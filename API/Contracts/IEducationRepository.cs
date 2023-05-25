using API.Models;

namespace API.Contracts;
    public interface IEducationRepository : IGenericRepository<Education>
    {

    IEnumerable<Education> GetByUniversityId (Guid universityId);
   
    //k2
    Education GetByEmployeeId(Guid employeeId);
}

