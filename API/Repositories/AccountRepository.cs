using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Login;

namespace API.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    //k3

    public AccountRepository(BookingManagementDbContext context) : base(context)
    {
       
    }

    public AccountEmpVM Login(LoginVM loginVM)
    {
        var account = GetAll();
        var employee = _context.Employees.ToList();
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new AccountEmpVM
                    {
                        Email = emp.Email,
                        Password = acc.Password

                    };
        return query.FirstOrDefault();
    }

    /*
    GAPERLU
        public Account Create(Account account)
            {
                try
                {
                    _context.Set<Account>().Add(account);
                    _context.SaveChanges();
                    return account;
                }
                catch
                {
                    return new Account();
                }
            }
            public bool Update(Account account)
            {
                try
                {
                    _context.Set<Account>().Update(account);
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
                    var account = GetByGuid(guid);
                    if (account == null)
                    {
                        return false;
                    }
                    _context.Set<Account>().Remove(account);
                    _context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public IEnumerable<Account> GetAll()
            {
                return _context.Set<Account>().ToList();

            }

            public Account? GetByGuid(Guid guid)
            {
                return _context.Set<Account>().Find(guid);
            }*/
}

