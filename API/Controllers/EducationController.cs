using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Bookings;
using API.ViewModels.Educations;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{

    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<Education, EducationVM> _mapper;

    public EducationController(IEducationRepository educationRepository, IMapper<Education, EducationVM> mapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var educations = _educationRepository.GetAll();
        if (!educations.Any())
        {
            return NotFound(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Education Tidak Ditemukan",
            });
        }
        var data = educations.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<EducationVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Education Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var education = _educationRepository.GetByGuid(guid);
        if (education is null)
        {
            return NotFound(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID Education Tidak Ditemukan",
            });
        }
        var data = _mapper.Map(education);
        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Education By Id Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(EducationVM educationVM)
    {
        var educationConverted = _mapper.Map(educationVM);
        var result = _educationRepository.Create(educationConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Education Tidak Berhasil Ditambahkan",
            });
        }

        return Ok(new ResponseVM<Education>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Education Berhasil Ditambahkan",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(EducationVM educationVM)
    {
        var educationConverted = _mapper.Map(educationVM);
        var isUpdated = _educationRepository.Update(educationConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Education Tidak Berhasil Diperbarui",
            });
        }

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Education Berhasil Diperbarui",

        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _educationRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Education Gagal Dihapus",
            });
        }

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Education Berhasil Dihapus",

        });
    }
}

