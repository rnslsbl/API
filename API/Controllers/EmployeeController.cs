using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Bookings;
using API.ViewModels.Educations;
using API.ViewModels.Employees;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Employee, EmployeeVM> _mapper;


    public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeeVM> mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;

    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return NotFound(new ResponseVM<MasterEmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Employee Tidak Ditemukan",
            });
        }
            var data = employees.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<EmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Employee Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID Employee Tidak Ditemukan",
            });
        }

        var data = _mapper.Map(employee);
        return Ok(new ResponseVM<Employee>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee By Id Berhasil Ditampilkan",
            Data = employee
        });
    }

    [HttpPost]
    public IActionResult Create(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var result = _employeeRepository.Create(employeeConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Employee Tidak Berhasil Ditambahkan",
            });
        }

        return Ok(new ResponseVM<Employee>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Employee Berhasil Ditambahkan",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var isUpdated = _employeeRepository.Update(employeeConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Employee Tidak Berhasil Diperbarui",
            });
        }

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Employee Berhasil Diperbarui",

        });
    }


    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Employee Gagal Dihapus",
            });
        }

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Employee Berhasil Dihapus",

        });
    }
    //k1
    [HttpGet("GetAllMasterEmployee")]
    public IActionResult GetAllMasterEmployee()
    {
        var masterEmployees = _employeeRepository.GetAllMasterEmployee();
        if (!masterEmployees.Any())
        {
            return NotFound(new ResponseVM<MasterEmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Employee Tidak Ditemukan",
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterEmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Master Employee Berhasil Ditampilkan",
            Data = masterEmployees
        });
    }

    [HttpGet("GetMasterEmployeeByGuid")]
    public IActionResult GetMasterEmployeeByGuid(Guid guid)
    {
        var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
        if (masterEmployees is null)
        {
            return NotFound(new ResponseVM<MasterEmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Employee Master Tidak Ditemukan",
            });
        }

        return Ok(new ResponseVM<MasterEmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Master Employee Berhasil Ditampilkan",
            Data = masterEmployees
        });
    }

    // End Kel 1
}

