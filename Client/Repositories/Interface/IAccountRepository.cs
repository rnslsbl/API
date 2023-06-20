using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IAccountRepository : IRepository<Account, string>
    {
        public Task<ResponseViewModel<string>> Loginn(LoginVM entity);
        public Task<ResponseMessageVM> Registerr(RegisterVM entity);
    }
}
