using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TekClub.Models
{
    public class Club
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public string TypeClub { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;


        public ICollection<ApplicationUser>? Membres { get; set; }

        [DisplayName("Date de création")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        
    }
}
