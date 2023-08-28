using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekClub.Models;
using TekClub.Models.IRespositories;

namespace TekClub.Controllers
{

    [Authorize(Roles = "Président")]
    public class PresidentController : Controller
    {
        private readonly IActivité _activitéRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IEvenement _evenementRepository;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public PresidentController(IActivité activitéRepository, IClubRepository clubRepository, UserManager<ApplicationUser> userManager, IEvenement evenementRepository)
        {
            _activitéRepository = activitéRepository;
            _clubRepository = clubRepository;
            _userManager = userManager;
            _evenementRepository = evenementRepository;
           
        }
      
        public IActionResult Index()
        {
            var users = _userManager.Users.Include(u => u.Club);
            var userId = _userManager.GetUserAsync(User).Result.Id;
            foreach(var user in users)
            {
                if(user.Id == userId)
                   ViewBag.UserClub = user.Club?.Id;
            }
          
            var clubs = _clubRepository.FindAll();
            return View(clubs);
        }
        

        public IActionResult CreateActivité()
          {
            var clubs = _clubRepository.GetClubWithMembers();
            var userswithClub = _userManager.Users.Include(x => x.Club);
            ViewBag.Clubs = clubs;
            ViewBag.Users = userswithClub;

            return View();
          }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult CreateActivité(Activité activité, string club)
        {
            if (ModelState.IsValid)
            {
                var clubA = _clubRepository.GetById(Guid.Parse(club));
                clubA.Activités?.Add(activité);
                _activitéRepository.AddActivité(activité);
                return RedirectToAction("ListeActivité");
            }
            return View();
         }
       
        public IActionResult ListeActivité()
        {
            var users = _userManager.Users.Include(u => u.Club);
            ViewBag.UserList = users;
            var activites = _activitéRepository.GetAllActivités();
            return View(activites);

        }
       

        public IActionResult EditActivité(Guid id)
        {
            var activité = _activitéRepository.GetActivité(id);
            return View(activité);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditActivité(Activité activité)
        {
            if (ModelState.IsValid)
            {

                _activitéRepository.UpdateActivité(activité);
                return RedirectToAction("ListeActivité");
            }
            return View();
        }
       

        public IActionResult DeleteActivité(Guid id)
        {
            var activité = _activitéRepository.GetActivité(id);
            _activitéRepository.DeleteActivité(activité);
            return RedirectToAction("ListeActivité");
        }

       
        public IActionResult DetailsActivité(Guid id)
        {
            var activité = _activitéRepository.GetActivité(id);
            return View(activité);


        }
       
        public IActionResult CreateEvenement()
        {
            var clubs = _clubRepository.GetClubWithMembers();
            var userswithClub = _userManager.Users.Include(x => x.Club);
            ViewBag.Clubs = clubs;
            ViewBag.Users = userswithClub;
            return View();
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEvenement(Evenement evenement, string club)
        {
            if (ModelState.IsValid)
            {
                var clubA = _clubRepository.GetById(Guid.Parse(club));
                clubA.Evenements?.Add(evenement);
                _evenementRepository.AddEvenement(evenement);
                return RedirectToAction("Index");
            }
            return View();
        }
       

        public IActionResult DeleteEvenement(Guid id)
        {
            var evenement = _evenementRepository.GetEvenement(id);
            _evenementRepository.DeleteEvenement(evenement);
            return RedirectToAction("ListeEvenement");
        }
       
        public IActionResult DetailsEvenement(Guid id)
        {
            var evenement = _evenementRepository.GetEvenement(id);
            return View(evenement);


        }
     
        public IActionResult ListeEvenement()
        {
            var users = _userManager.Users.Include(u => u.Club);
            ViewBag.UserList = users;
            var evenements = _evenementRepository.GetAllEvenements();
            return View(evenements);

        }
       
        public IActionResult EditEvenement(Guid id)
        {
            var evenement = _evenementRepository.GetEvenement(id);
            return View(evenement);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEvenement(Evenement evenement)
        {
            if (ModelState.IsValid)
            {

                _evenementRepository.UpdateEvenement(evenement);
                return RedirectToAction("ListeEvenement");
            }
            return View();
        }



        public async Task<IActionResult> listMembres()
        {
            var user = await _userManager.GetUserAsync(User);
             var users = _userManager.Users.Include(u => u.Club);
             var ClubId = Guid.Empty;
             foreach(var u in users)
            {
                    if(u.Id == user.Id)
                    {
                        ClubId  = u.Club.Id;
                    }
             }
             ViewBag.ClubId = ClubId.ToString();
             return View(users);
        }

        public IActionResult Accepter(Guid id)
        {
            
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            
            if(user.clubTemp != null)
            {
                var club = _clubRepository.GetById(Guid.Parse(user.clubTemp));
                club.Membres?.Add(user);
                user.Status = "Accépté";
                
                _userManager.AddToRoleAsync(user, "Membre").Wait();
                _userManager.UpdateAsync(user);
            }
           
           
            return RedirectToAction("ListMembres");
        }

        public async Task<IActionResult> Retirer(Guid id)
        {
            var user =  await _userManager.FindByIdAsync(id.ToString());
            var clubs = _clubRepository.GetClubWithMembers();
           
            if(user.clubTemp != null)
            {
                foreach(var club in clubs)
                {
                    if(club.Membres != null && club.Membres.Contains(user))
                    {
                        user.clubTemp = null;
                        user.Status = null;
                      
                    }
                   
                }
                await _userManager.RemoveFromRoleAsync(user, "Membre");
                await _userManager.UpdateAsync(user);
                return RedirectToAction("ListMembres");
            }
            


            return View();
        }

        public IActionResult Discussion()
        {
            return View();
        }


    }
}
