﻿using API.Contexts;
using API.Contracts;
using API.Models;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Login;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Azure.Core;
using API.ViewModels.Bookings;

namespace API.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    
    //k2
    public AccountRepository(BookingManagementDbContext context, 
        IUniversityRepository universityRepository,
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

    public int Register(RegisterVM registerVM)
    {
        try
        {
            var university = new University
            {
                Code = registerVM.UniversityCode,
                Name = registerVM.UniversityName

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
            var result = _employeeRepository.Create(employee);

            var education = new Education
            {
                Guid = employee.Guid,
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.Gpa,
                UniversityGuid = university.Guid
            };
            _educationRepository.Create(education);

            /*var hashPassword = Hashing.HashPassword(registerVM.Password);*/
            var account = new Account
            {
                Guid = employee.Guid,
                Password = Hashing.HashPassword(registerVM.Password),        
                IsDeleted = false,
                IsUsed = true,
                OTP = 0
            };

            Create(account);
            //Template new register nya user
            var accountRole = new AccountRole
            {
                RoleGuid = Guid.Parse("42e77495-645e-4095-586f-08db60bf14ae"),
                AccountGuid = employee.Guid
            };
            //injeksi
            _context.AccountRoles.Add(accountRole);
            _context.SaveChanges();

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
    //end k3


    //k5
    public int UpdateOTP(Guid? employeeId)
    {
        var account = new Account();
        account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        //Generate OTP
        Random rnd = new Random();
        var getOtp = rnd.Next(100000, 999999);
        account.OTP = getOtp;

        //Add 5 minutes to expired time
        account.ExpiredTime = DateTime.Now.AddMinutes(5);
        account.IsUsed = false;
        try
        {
            var check = Update(account);


            if (!check)
            {
                return 0;
            }
            return getOtp;
        }
        catch
        {
            return 0;
        }
    }
        //end k5

        //k6

        public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM)
    {
        var account = new Account();
        account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        if (account == null || account.OTP != changePasswordVM.OTP)
        {
            return 2;
        }
        // Cek apakah OTP sudah digunakan
        if (account.IsUsed)
        {
            return 3;
        }
        // Cek apakah OTP sudah expired
        if (account.ExpiredTime < DateTime.Now)
        {
            return 4;
        }
        // Cek apakah NewPassword dan ConfirmPassword sesuai
        if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
        {
            return 5;
        }
        // Update password
        account.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
        account.IsUsed = true;
        try
        {
            var updatePassword = Update(account);
            if (!updatePassword)
            {
                return 0;
            }
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public IEnumerable<string> GetRoles(Guid guid)
    {
        //join account role dan role
        var getAccount = GetByGuid(guid);
        if (getAccount == null) return Enumerable.Empty<string>();
        var getAccountRoles = from accountRoles in _context.AccountRoles
                              join roles in _context.Roles on accountRoles.RoleGuid equals roles.Guid
                               where accountRoles.AccountGuid == guid
                              select roles.Name;

        return getAccountRoles.ToList();

        

}




    }

    //end k6




