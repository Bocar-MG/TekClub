using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekClub.Models
{
    public class Club
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public string TypeClub { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;


        [NotMapped]
        [DisplayName("Président")]
        public Guid MembreAjouter { get; set; }
        public ICollection<ApplicationUser>? Membres { get; set; } = new Collection<ApplicationUser>();

        public ICollection<Evenement>? Evenements { get; set; } =  new Collection<Evenement>();


        public ICollection<Activité>? Activités { get; set; } = new Collection<Activité>();

        [DisplayName("Date de création")]
        public DateTime DateCreation { get; set; } = DateTime.Now;


        
    }
}
