// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TekClub.Models;

namespace TekClub.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
       private readonly IWebHostEnvironment _env;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

       

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string Nom { get; set; }
            public string Prenom { get; set; }
            public int Age { get; set; }
            public string Specialité { get; set; }


            [Display(Name = "Photo de profile")]
            public string ImagePic { get; set; }
            public string Classe { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var Nom = user.Nom;
            var Prenom = user.Prenom;
            var Age = user.Age;
            var Specialité = user.Specialité;
            var Classe = user.Classe;
            var ImagePic = user.ImagePic;
            
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
                , Nom = Nom, Prenom = Prenom, Age = Age, Specialité = Specialité, Classe = Classe, ImagePic = ImagePic
            };
        }

        public SelectList Specialités { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
           
            IEnumerable<string> liste = new List<string> { "Classe préparatoire(CPI)", "TIC", "Genie Logiciel", "DMWM", "Data Science", "IoT", "Sécurité et Systémes Informatiques et Réseaux" };

            Specialités = new SelectList(liste.ToList());
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
             // i want to update the user's information
             user.Nom = Input.Nom;
            user.Prenom = Input.Prenom;
            user.Age = Input.Age;
            user.Specialité = Input.Specialité;
            user.Classe = Input.Classe;

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                var NomFichier = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var CheminStockageImage = Path.Combine(_env.WebRootPath, "ProfilePic", NomFichier);

                using (var stream = new FileStream(CheminStockageImage, FileMode.Create))
                {
                    file.CopyTo(stream);

                }
                user.ImagePic = NomFichier;
            }







            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update user's information.";
                return RedirectToPage();
            }


            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                

                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
