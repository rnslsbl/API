using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Employees;
using API.ViewModels.Login;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;
using Microsoft.Identity.Client;
using System.Net.Mail;
using System.Net;
using API.ViewModels.Others;
using API.ViewModels.Bookings;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController<Account, AccountVM>
{

    private readonly IAccountRepository _accountRepository;
    private readonly IMapper<Account, AccountVM> _mapper;
    private readonly IEmployeeRepository _employeeRepository; //k3 & k6
    private readonly IMapper<Account, ChangePasswordVM> _changePasswordMapper; //k6
    private readonly IMapper<Employee, EmployeeVM> _emailMapper; //k6
    private readonly IEmailService _emailService;

    public AccountController(IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper, IEmployeeRepository employeeRepository, IMapper<Account, ChangePasswordVM> changePasswordMapper, IMapper<Employee, EmployeeVM> emailMapper, IEmailService emailService) : base (accountRepository, mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
        _changePasswordMapper = changePasswordMapper;
        _emailMapper = emailMapper;
        _emailService = emailService;
    }

    //k2
    [HttpPost("Register")]

    public IActionResult Register(RegisterVM registerVM)
    {

        var result = _accountRepository.Register(registerVM);
        switch (result)
        {
            case 0:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Registration Failed",
                });
            case 1:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email Already Exist",
                });
            case 2:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Phone Number Already Exist",
                });
            case 3:
                return Ok(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Proses Register Berhasil",
                    
                });
        }

        return Ok();

    }

    //end k2


    //k3
    [HttpPost("login")]

    public IActionResult Login(LoginVM loginVM)
    {
        var account = _accountRepository.Login(loginVM);

        if (account == null)
        {
            return NotFound(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Akun Tidak Ditemukan",
            });
        }
        /*var validatePassword = Hashing.ValidatePassword(loginVM.Password, account.Password);
            if (validatePassword is false)*/
        if (!Hashing.ValidatePassword(loginVM.Password, account.Password))
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password is Invalid",
            });
        }

        return Ok(new ResponseVM<LoginVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Berhasil Login",
            
        });

    }
    //end k3

    //k5
    [HttpPost("ForgotPassword" + "{email}")]
    public IActionResult UpdateResetPass(String email)
    {

        var getGuid = _employeeRepository.FindGuidByEmail(email);
        if (getGuid == null)
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Akun Tidak Ditemukan",
            });
        }

        var isUpdated = _accountRepository.UpdateOTP(getGuid);

        switch (isUpdated)
        {
            case 0:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Failed Update OTP"
                });
            default:
                var hasil = new AccountResetPasswordVM
                {
                    Email = email,
                    OTP = isUpdated
                };

                _emailService.SetEmail(email)
                        .SetSubject("Forget Password")
                        .SetHtmlMessage($"Your OTP is {isUpdated}")
                        .SendEmailAsync();
                

                return Ok(new ResponseVM<AccountResetPasswordVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Reset Password Berhasil",
                    Data = hasil

                });
        }
        }
        //end k5

        //k6
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            // Cek apakah email dan OTP valid
            var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
            var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
            switch (changePass)
            {
                case 0:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Change Password Failed",
                    });
                case 1:
                    return Ok(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Password Change Succesful",
                    });
                case 2:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Invalid OTP",
                    });
                case 3:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP Has Already Been Used",
                    });
            /*case 4:
                return BadRequest("OTP expired");*/
            case 5:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Cek..",
                    });

            }
            return null;

        }
        //end k6

      
}

