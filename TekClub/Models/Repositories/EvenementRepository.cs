using Microsoft.EntityFrameworkCore;
using TekClub.Models.Data;
using TekClub.Models.IRespositories;

namespace TekClub.Models.Repositories
{
    public class EvenementRepository : IEvenement
    {
        private readonly ClubDbContext _clubDbContext;
        public EvenementRepository(ClubDbContext clubDbContext)
        {
            _clubDbContext = clubDbContext;
           
        }
        public void AddEvenement(Evenement evenement)
        {
             _clubDbContext.Evenements.Add(evenement);
            _clubDbContext.SaveChanges();
        }

        public void DeleteEvenement(Evenement evenement)
        {
            var evenementToDelete = _clubDbContext.Evenements.Find(evenement.Id);
            _clubDbContext.Evenements.Remove(evenementToDelete);
            _clubDbContext.SaveChanges();
        }

        public IEnumerable<Evenement> GetAllEvenements()
        {
            return _clubDbContext.Evenements.Include(u => u.Club).ToList();
        }

        public Evenement GetEvenement(Guid id)
        {
            return _clubDbContext.Evenements.Find(id);
        }

        public void UpdateEvenement(Evenement evenement)
        {
            _clubDbContext.Evenements.Update(evenement);
            _clubDbContext.SaveChanges();
        }
    }
}
