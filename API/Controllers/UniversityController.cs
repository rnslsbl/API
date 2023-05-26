using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Educations;
using API.ViewModels.Others;
using API.ViewModels.Rooms;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
    {

    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<University, UniversityVM> _mapper;
    private readonly IMapper<Education, EducationVM> _educationMapper;

    public UniversityController(IUniversityRepository universityRepository, IEducationRepository educationRepository, IMapper<University, UniversityVM> mapper, IMapper<Education, EducationVM> educationMapper )
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _mapper = mapper;
        _educationMapper = educationMapper;
    }

    [HttpGet("WithEducation")]
    public IActionResult GetAllWithEducation()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound(new ResponseVM<UniversityEducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data University Tidak Ditemukan",
            });
        }

        var results = new List<UniversityEducationVM>();
        foreach (var university in universities)
        {
            var education = _educationRepository.GetByUniversityId(university.Guid);
            //var educationMapped = education.Select(EducationVM.ToVM);
            var educationMapped = education.Select(_educationMapper.Map);

            var result = new UniversityEducationVM
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name,
                Educations = educationMapped
            };

            results.Add(result);
        }

        return Ok(new ResponseVM<IEnumerable<UniversityEducationVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Room Berhasil Ditampilkan",
            Data = results
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data University Tidak Ditemukan",
            });
        }
        var data = universities.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<UniversityVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data University Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null)
        {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID University Tidak Ditemukan",
            });
        }
        var data = _mapper.Map(university);
        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "University By Id Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(UniversityVM universityVM)
    {
        var universityConverted = _mapper.Map(universityVM);
        var result = _universityRepository.Create(universityConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data University Tidak Berhasil Ditambahkan",
            });
        }

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data University Berhasil Ditambahkan",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(UniversityVM universityVM)
    {
        var universityConverted = _mapper.Map(universityVM);
        var isUpdated = _universityRepository.Update(universityConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data University Tidak Berhasil Diperbarui",
            });
        }
       
        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data University Berhasil Diperbarui",

        });
    }
    [HttpGet("ByName/{name}")]
    public IActionResult GetByName(string name)
    {
        var university = _universityRepository.GetByName(name);
        if (!university.Any())
        {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Nama University Tidak Ditemukan",
            });
        }
        var data = university.Select(_mapper.Map);
        return Ok(new ResponseVM<IEnumerable<UniversityVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "University By Name Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _universityRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data University Gagal Dihapus",
            });
        }
        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data University Berhasil Dihapus",

        });
    }
}

