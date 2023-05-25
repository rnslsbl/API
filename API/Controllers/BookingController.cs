using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Bookings;
using API.ViewModels.Educations;
using API.ViewModels.Employees;
using API.ViewModels.Rooms;
using API.ViewModels.Universities;
using System.Linq.Expressions;

namespace API.Controllers
{

    [ApiController]
    [Route("RestAPI/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingController(IBookingRepository bokingRepository, IMapper<Booking, BookingVM> mapper, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bokingRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
        }

        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                var bookingDetails = _bookingRepository.GetAllBookingDetail();

                return Ok(bookingDetails);

            }
            catch
            {
                return Ok("error");
            }
             }

        [HttpGet("BookingDetail/{guid}")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            try
            {
                var booking = _bookingRepository.GetBookingDetailByGuid(guid);
                if (booking is null)
                {

                    return NotFound();
                }

                return Ok(booking);
            }
            catch
            {
                return Ok("error");
            }
        }

         [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound();
            }
            var bookingConverteds = bookings.Select(_mapper.Map).ToList();
            return Ok(bookingConverteds);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound();
            }
            var bookingConverted = _mapper.Map(booking);
            return Ok(bookingConverted);
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var booking = _mapper.Map(bookingVM);
            var result = _bookingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var booking = _mapper.Map(bookingVM);
            var isUpdated = _bookingRepository.Update(booking);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }

        //K3
        [HttpGet("bookinglength")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound();
            }

            return Ok(bookingLengths);
        }
    }
}
