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
public class EducationController : BaseController<Education, EducationVM>
{

    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<Education, EducationVM> _mapper;

    public EducationController(IEducationRepository educationRepository, IMapper<Education, EducationVM> mapper) : base (educationRepository, mapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;

    }

   
}

