using API.Models;

namespace API.Contracts;
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    Employee GetEmployeeId(Guid bookingGuid);
}

