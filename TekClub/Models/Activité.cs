using System.ComponentModel;

namespace TekClub.Models
{
    public class Activité
    {

        public Guid Id { get; set; }

        [DisplayName("Nom de l'activité")]
        public string Nom { get; set; } = string.Empty;

        [DisplayName("Type d'activité")]
        public string? TypeActivité { get; set; }

        public string? Description { get; set; }


        [DisplayName("Date de Debut")]
        public DateTime? DateDebut { get; set; }

        [DisplayName("Date de Fin")]
        public DateTime? DateFin { get; set; }

        public Club? Club { get; set; }

        




    }
}
