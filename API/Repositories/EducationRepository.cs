using API.Contexts;
using API.Models;
using API.Contracts;

namespace API.Repositories;
public class EducationRepository : GenericRepository<Education>, IEducationRepository
{
   
    public EducationRepository(BookingManagementDbContext context) : base(context) { }

    public IEnumerable<Education> GetByUniversityId(Guid universityId)
    {

        return _context.Set<Education>().Where(e => e.UniversityGuid == universityId);

    }











    /*public Education Create(Education education)
    {
        try
        {
            _context.Set<Education>().Add(education);
            _context.SaveChanges();
            return education;
        }
        catch
        {
            return new Education();
        }
    }
    public bool Update(Education education)
    {
        try
        {
            _context.Set<Education>().Update(education);
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
            var education = GetByGuid(guid);
            if (education == null)
            {
                return false;
            }
            _context.Set<Education>().Remove(education);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<Education> GetAll()
    {
        return _context.Set<Education>().ToList();

    }

    public Education? GetByGuid(Guid guid)
    {
        return _context.Set<Education>().Find(guid);
    }*/
}

