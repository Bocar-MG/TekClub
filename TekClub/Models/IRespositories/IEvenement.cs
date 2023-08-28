namespace TekClub.Models.IRespositories
{
    public interface IEvenement
    {
        public void AddEvenement(Evenement evenement);
        public void UpdateEvenement(Evenement evenement);
        public void DeleteEvenement(Evenement evenement);
        public Evenement GetEvenement(Guid id);
        public IEnumerable<Evenement> GetAllEvenements();
    }
}
