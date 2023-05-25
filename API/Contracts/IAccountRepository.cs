using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Login;

namespace API.Contracts;

    public interface IAccountRepository : IGenericRepository<Account>
    {
    //k3
    AccountEmpVM Login(LoginVM loginVM);

}

