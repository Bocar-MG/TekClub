using Microsoft.EntityFrameworkCore;
using TekClub.Models.Data;
using TekClub.Models.IRespositories;

namespace TekClub.Models.Repositories
{
    public class ActivitéRepository : IActivité
    {
        private readonly ClubDbContext _clubDbContext;
       
        public ActivitéRepository(ClubDbContext clubDbContext)
        {
            _clubDbContext = clubDbContext;
          
        }
        public void AddActivité(Activité activité)
        {
            _clubDbContext.Activités.Add(activité);
            _clubDbContext.SaveChanges();
        }

        public void DeleteActivité(Activité activité)
        {
            var activitéToDelete = _clubDbContext.Activités.Find(activité.Id);
            _clubDbContext.Activités.Remove(activitéToDelete);
            _clubDbContext.SaveChanges();

        }

        public Activité GetActivité(Guid id)
        {
            
            
            return _clubDbContext.Activités.Find(id);
        }

        public IEnumerable<Activité> GetAllActivités()
        {
             return _clubDbContext.Activités.Include(u => u.Club).ToList();
        }

        public void UpdateActivité(Activité activité)
        {
            _clubDbContext.Activités.Update(activité);
            _clubDbContext.SaveChanges();
        }
    }
}
