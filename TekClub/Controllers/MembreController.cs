using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TekClub.Models;
using TekClub.Models.IRespositories;

namespace TekClub.Controllers
{
    
    public class MembreController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IActivité _activitéRepository;
        private readonly IEvenement _evenementRepository;
        public MembreController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,IActivité activité, IEvenement evenement)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _activitéRepository = activité;
            _evenementRepository = evenement;



        }
        public IActionResult Index()
        {
            return View();
        }

      
        public async Task<IActionResult> RejointClub(Guid Id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            user.clubTemp = Id.ToString();
            user.Status = "Souhaite rejoindre votre club";
            await _userManager.UpdateAsync(user);
            ViewBag.Message = "Votre demande a été envoyée";
           

            return View();

        }
        [Authorize(Roles = "Membre")]
        public  IActionResult ConsulterActivité()
        {
          
            var activités = _activitéRepository.GetAllActivités();


            return View(activités);
        }

        [Authorize(Roles = "Membre")]
        public IActionResult ConsulterEvenement()
        {
             var evenements = _evenementRepository.GetAllEvenements();
            return View(evenements);
        }

        [Authorize(Roles = "Membre")]
        public IActionResult Discussion()
        {
            return View();
        }
    }

  
}
