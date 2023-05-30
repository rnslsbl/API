using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Login;

namespace API.Contracts;

    public interface IAccountRepository : IGenericRepository<Account>
    {
    //k2
    int Register(RegisterVM registerVM);
    //k3
    AccountEmpVM Login(LoginVM loginVM);
    //k5
    int UpdateOTP(Guid? employeeId);
    //k6
    public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);

    IEnumerable<string> GetRoles(Guid guid);

    
}

