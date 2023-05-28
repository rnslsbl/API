using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.AccountRoles;
using API.ViewModels.Accounts;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : BaseController<AccountRole, AccountRoleVM>
{

    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper<AccountRole, AccountRoleVM> _mapper;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper) : base (accountRoleRepository, mapper)
    {
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;

    }

}

