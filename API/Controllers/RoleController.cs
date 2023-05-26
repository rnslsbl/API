using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Employees;
using API.ViewModels.Others;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{

    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<Role, RoleVM> _mapper;

    public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var roles = _roleRepository.GetAll();
        if (!roles.Any())
        {
            return NotFound(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Role Tidak Ditemukan",
            });
        }

        var data = roles.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<RoleVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Role Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID Role Tidak Ditemukan",
            });
        }
        var data = _mapper.Map(role);
        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Role By Id Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(RoleVM roleVM)
    {
        var roleConverted = _mapper.Map(roleVM);
        var result = _roleRepository.Create(roleConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Role Tidak Berhasil Ditambahkan",
            });
        }
    
        return Ok(new ResponseVM<Role>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Role Berhasil Ditambahkan",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(RoleVM roleVM)
    {
        var roleConverted = _mapper.Map(roleVM);
        var isUpdated = _roleRepository.Update(roleConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Role Tidak Berhasil Diperbarui",
            });
        }

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Role Berhasil Diperbarui",

        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Role Gagal Dihapus",
            });
        }

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Role Berhasil Dihapus",

        });
    }
}

