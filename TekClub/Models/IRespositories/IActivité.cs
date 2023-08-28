namespace TekClub.Models.IRespositories
{
    public interface IActivité
    {
        public void AddActivité(Activité activité);
        public void UpdateActivité(Activité activité);
        public void DeleteActivité(Activité activité);
        public Activité GetActivité(Guid id);
        public IEnumerable<Activité> GetAllActivités();
    }
}
