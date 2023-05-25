using API.Contexts;
using API.Models;
using API.Contracts;

namespace API.Repositories;
public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    //private readonly BookingManagementDbContext _context;
    public EmployeeRepository(BookingManagementDbContext context) : base(context) { }



}
