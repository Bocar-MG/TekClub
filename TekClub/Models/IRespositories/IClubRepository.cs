namespace TekClub.Models.IRespositories
{
    public interface IClubRepository
    {
        // Create the CRUD methods
        // Create
        public Club Create(Club club, IFormFile formFile);
        // Read
        public IEnumerable<Club> FindAll();
        // Update
        public Club Update(Club club, IFormFile formFile);
        // Delete
        public bool Delete(Club club);

        public Club GetById(Guid id);

        public IEnumerable<Club> GetClubWithActivités();

        public IEnumerable<Club> GetClubWithMembers();


       
      
       
           

    }
}
