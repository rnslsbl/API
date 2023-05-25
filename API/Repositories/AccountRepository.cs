using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Login;

namespace API.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    
    //k2
    public AccountRepository(BookingManagementDbContext context, IUniversityRepository universityRepository,
        IEmployeeRepository employeeRepository,
        IEducationRepository educationRepository) : base(context)
    {
        _universityRepository = universityRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
    }


    //k2
    private readonly IUniversityRepository _universityRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;


    // coba

    public int Register(RegisterVM registerVM)
    {
        try
        {
            var university = new University
            {
                Code = registerVM.Code,
                Name = registerVM.Name

            };
            _universityRepository.CreateWithValidate(university);

            var employee = new Employee
            {
                NIK = GenerateNIK(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            var result = _employeeRepository.CreateWithValidate(employee);

            if (result != 3)
            {
                return result;
            }

            var education = new Education
            {
                Guid = employee.Guid,
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.GPA,
                UniversityGuid = university.Guid
            };
            _educationRepository.Create(education);

            var account = new Account
            {
                Guid = employee.Guid,
                Password = registerVM.Password,
                IsDeleted = false,
                IsUsed = true,
                OTP = 0
            };

            Create(account);

            return 3;

        }
        catch
        {
            return 0;
        }

    }

    private string GenerateNIK()
    {
        var lastNik = _employeeRepository.GetAll().OrderByDescending(e => int.Parse(e.NIK)).FirstOrDefault();

        if (lastNik != null)
        {
            int lastNikNumber;
            if (int.TryParse(lastNik.NIK, out lastNikNumber))
            {
                return (lastNikNumber + 1).ToString();
            }
        }

        return "100000";
    }
    //end k2


    //k3
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

