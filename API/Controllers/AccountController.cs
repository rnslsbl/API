using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Login;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{

    private readonly IAccountRepository _accountRepository;
    private readonly IMapper<Account, AccountVM> _mapper;
    private readonly IEmployeeRepository _employeeRepository; //k3

    public AccountController(IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper, IEmployeeRepository employeeRepository)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }
    //k2
    [HttpPost("Register")]

    public IActionResult Register(RegisterVM registerVM)
    {

        var result = _accountRepository.Register(registerVM);
        switch (result)
        {
            case 0:
                return BadRequest("Registration failed");
            case 1:
                return BadRequest("Email already exists");
            case 2:
                return BadRequest("Phone number already exists");
            case 3:
                return Ok("Registration success");
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
            return NotFound("Account not found");
        }

        if (account.Password != loginVM.Password)
        {
            return BadRequest("Password is invalid");
        }

        return Ok();

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return NotFound();
        }
        var data = accounts.Select(_mapper.Map).ToList();
        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(account);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);
        var result = _accountRepository.Create(accountConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);
        var isUpdated = _accountRepository.Update(accountConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

