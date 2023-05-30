using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Bookings;
using API.ViewModels.Educations;
using API.ViewModels.Employees;
using API.ViewModels.Rooms;
using API.ViewModels.Universities;
using API.ViewModels.Others;
using System.Linq.Expressions;
using System.Net;
using API.ViewModels.AccountRoles;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{

    [ApiController]
    [Route("RestAPI/[controller]")]
    public class BookingController : BaseController<Booking, BookingVM>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingController(IBookingRepository bokingRepository, IMapper<Booking, BookingVM> mapper, IEmployeeRepository employeeRepository, IRoomRepository roomRepository): base (bokingRepository, mapper)
        {
            _bookingRepository = bokingRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                var bookingDetails = _bookingRepository.GetAllBookingDetail();

                return Ok(new ResponseVM<IEnumerable<BookingDetailVM>> {
                    Code = StatusCodes.Status200OK,
                    Status =HttpStatusCode.OK.ToString(),
                    Message = "Detail Booking Berhasil Ditampilkan",
                    Data = bookingDetails
                });

            }
            catch
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Detail Booking Tidak Ditemukan",
                });
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

                    return NotFound(new ResponseVM<BookingDetailVM>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Booking Tidak Ditemukan",
                    });
                }

                return Ok(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Detail Booking Berhasil Ditampilkan",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //K3
        [HttpGet("BookingLength")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound(new ResponseVM<BookingDurationVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Booking Tidak Ditemukan",
                });
            }
            return Ok(new ResponseVM<IEnumerable<BookingDurationVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Durasi Booking Berhasil Ditampilkan",
                Data = bookingLengths
            });
        }





    }
}
