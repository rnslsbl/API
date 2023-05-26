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
public class AccountRoleController : ControllerBase
{

    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper<AccountRole, AccountRoleVM> _mapper;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
    {
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accountRoles = _accountRoleRepository.GetAll();
        if (!accountRoles.Any())
        {
            return NotFound(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Akun Role Tidak Ditemukan",
            });
        }
        var data = accountRoles.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<AccountRoleVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Akun Role Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null)
        {
            return NotFound(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID Account Role Tidak Ditemukan",
            });
        }
        var data = _mapper.Map(accountRole);
        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Account Role by Id Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(AccountRoleVM accountRoleVM)
    {
        var accountRoleConverted = _mapper.Map(accountRoleVM);
        var result = _accountRoleRepository.Create(accountRoleConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Akun Role Baru Tidak Berhasil Ditambahkan",
            });
        }

        return Ok(new ResponseVM<AccountRole>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Akun Role Baru Berhasil Ditambahkan",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(AccountRoleVM accountRoleVM)
    {
        var accountRoleConverted = _mapper.Map(accountRoleVM);
        var isUpdated = _accountRoleRepository.Update(accountRoleConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Akun Role Tidak Berhasil Diperbarui",
            });
        }

        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Akun Role Berhasil Diperbarui",

        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRoleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Akun Role Gagal Dihapus",
            });
        }

        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Akun Role Berhasil Dihapus",

        });
    }
}

