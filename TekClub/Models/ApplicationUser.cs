using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekClub.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Classe { get; set; } = string.Empty;
        public string Specialité { get; set; } = string.Empty;

        public bool Président { get; set; }

        public string? ImagePic { get; set; } = string.Empty;
        public string? clubTemp { get; set; } = string.Empty;
        public string? Status { get; set; }
        public Club? Club { get; set; }
    }
}
