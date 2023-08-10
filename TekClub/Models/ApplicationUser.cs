using Microsoft.AspNetCore.Identity;

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
        public string? Status { get; set; }
        public Club? Club { get; set; }
    }
}
