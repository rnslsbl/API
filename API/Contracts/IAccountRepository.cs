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

}

