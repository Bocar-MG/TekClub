using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TekClub.Models.Data;
using TekClub.Models.IRespositories;

namespace TekClub.Models.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly ClubDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

       public ClubRepository(ClubDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public Club Create(Club club, IFormFile formFile)
        {
            if (formFile != null)
            {
                var NomFichier = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

                var CheminStockageImage = Path.Combine(_env.WebRootPath, "ClubImage", NomFichier);

                using (var stream = new FileStream(CheminStockageImage, FileMode.Create))
                {
                    formFile.CopyTo(stream);

                }
                club.ImageUrl = NomFichier;

            }
           
            _dbContext.Clubs.Add(club);
            _dbContext.SaveChanges();
            return club;
        }

       public  bool Delete(Club club)
        {
           _dbContext?.Clubs.Remove(club);
            _dbContext?.SaveChanges();
            return true;

        }

        public IEnumerable<Club> FindAll()
        {
            var clubs = _dbContext.Clubs.Include(c => c.Membres).ToList();
            return clubs;

        }

        public Club GetById(Guid id)
        {
           var club = _dbContext.Clubs.Find(id);
            return club;
        }

       

        public IEnumerable<Club> GetClubWithActivités()
        {
            var clubs = _dbContext.Clubs.Include(u => u.Activités).ToList();
            return clubs;
        }

        public IEnumerable<Club> GetClubWithMembers()
        {
            return _dbContext.Clubs.Include(u => u.Membres);
        }

        public  Club Update(Club club, IFormFile formFile)
        {

            if (formFile != null)
            {
                var NomFichier = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

                var CheminStockageImage = Path.Combine(_env.WebRootPath, "ClubImage", NomFichier);

                using (var stream = new FileStream(CheminStockageImage, FileMode.Create))
                {
                    formFile.CopyTo(stream);


                }
             
                club.ImageUrl = NomFichier;
                _dbContext.Clubs.Update(club);
                _dbContext.SaveChanges();

            }
            else
            {
                _dbContext.Clubs.Update(club);
                _dbContext.SaveChanges();
            }
            return club;
        }
    }
}
