using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekClub.Models;
using TekClub.Models.IRespositories;

namespace TekClub.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(IClubRepository clubRepository, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _clubRepository = clubRepository;
            _roleManager = roleManager;
            _userManager = userManager;
             
        }
       
        public IActionResult Index()
        {
            // total des clubs
            ViewBag.TotalClubs = _clubRepository.FindAll().Count();
            // je veux le club qui a le plus de membres
            var clubs = _clubRepository.FindAll();
            int max = 0;
            Club clubMax = null;
            foreach (var club in clubs)
            {
                if (club.Membres?.Count > max)
                {
                    max = club.Membres.Count;
                    clubMax = club;
                }
            }
            return View(clubMax);
        }

       
        public IActionResult CreateClub()
        {
          

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClub(Club club, IFormFile file)
        {
             if(ModelState.IsValid)
            {
               var usertemp = _userManager.FindByIdAsync(club.MembreAjouter.ToString()).Result;
                usertemp.Club = club;
                club.Membres.Add(usertemp);
                _clubRepository.Create(club,file);
                return RedirectToAction("Index");
            }
             return View();
        }

        public IActionResult UpdateClub(Guid id)
        {
             var club = _clubRepository.GetById(id);
             return View(club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateClub(Club club, IFormFile file)
        {
            if (ModelState.IsValid)
            {
             
                _clubRepository.Update(club,file);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteClub(Guid id)
        {
            var club = _clubRepository.GetById(id);
            bool supprimer =  _clubRepository.Delete(club);
            if(supprimer)
                return RedirectToAction("ListClub");
            return View();
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                
            }
            return View(name);
        }
        public IActionResult ListRole()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        public IActionResult ListClub()
        {
            var clubs = _clubRepository.FindAll();
            foreach (var club in clubs)
            {
               club.Membres = _userManager.Users.Where(u => u.Club.Id == club.Id).ToList();
            }
             
            return View(clubs);
        }
        public IActionResult ListUser()
        {
            var users = _userManager.Users.Include(user => user.Club);

            return View(users);
        }
        public IActionResult Accepter(Guid id)
        {          
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            user.Status = "Accepté";
            user.Président = true;
            _userManager.AddToRoleAsync(user, "Président").Wait();
             _userManager.UpdateAsync(user);
              return RedirectToAction("ListUser");
        }
        public IActionResult DeleteUser(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            _userManager.DeleteAsync(user);
            return RedirectToAction("ListUser");
        }

        public IActionResult DetailsClub(Guid id)
        {
            var pres = _userManager.Users.Where(u => u.Club.Id == id && u.Président == true).FirstOrDefault();
            ViewBag.President = pres;

            var club = _clubRepository.GetById(id);
            return View(club);
        }
        public IActionResult VoirClub(Guid id)
        {
            var club = _clubRepository.GetById(id);

            return View(club);

        }

        [AllowAnonymous]
        public IActionResult Clublist()
        {
            var clubs = _clubRepository.FindAll();
            return View(clubs);
        }
    }
}
