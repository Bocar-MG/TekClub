using System.ComponentModel;

namespace TekClub.Models
{
    public class Evenement
    {
        public Guid Id { get; set; }

        [DisplayName("Nom de l'evenement")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Type d'evénement")]
        public string TypeEvenement { get; set; } = string.Empty;

        public string? Descripton { get; set; }


        [DisplayName("Date de Début")]
        public DateTime DateDebut { get; set; }

        [DisplayName("Date de Fin")]
        public DateTime DateFin { get; set; }

        public Club? Club { get; set; }

    }
}
