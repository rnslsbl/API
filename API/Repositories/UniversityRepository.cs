using API.Contexts;
using API.Models;
using API.Contracts;

namespace API.Repositories; 
    public class UniversityRepository : GenericRepository<University>, IUniversityRepository
{
      public UniversityRepository(BookingManagementDbContext context) : base(context) { }

    public IEnumerable<University> GetByName(string name)
    {
        return _context.Set<University>().Where(u => u.Name.Contains(name));
    }

    /* public UniversityRepository(BookingManagementDbContext context) : base(context) { }
        {
            _context = context;
        }

        public University Create (University university)
        {
            try
            {
                _context.Set<University>().Add(university);
                _context.SaveChanges();
                return university;
            }
            catch
            {
                return new University();
            }
        }
        public bool Update (University university)
        {
            try
            {
                _context.Set<University>().Update(university);
                _context.SaveChanges();
                return true;
            } 
            catch
            {
                return false;
            }
        }
        public bool Delete(Guid guid)
        {
            try
            {
                var university = GetByGuid(guid);
                if (university == null)
                {
                    return false;
                }
                _context.Set<University>().Remove(university);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList();

        }

        public University? GetByGuid (Guid guid)
        {
            return _context.Set<University>().Find(guid);
        }*/
}
