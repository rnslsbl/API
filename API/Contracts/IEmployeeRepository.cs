using API.Models;
using API.ViewModels.Employees;

namespace API.Contracts;
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    //k1
    IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
    MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

    //k2
    int CreateWithValidate(Employee employee);
}

