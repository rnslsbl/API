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



        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Booking Tidak Ditemukan",
                });
            }
            var bookingConverteds = bookings.Select(_mapper.Map).ToList();
            return Ok(new ResponseVM<List<BookingVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Booking Berhasil Ditampilkan",
                Data = bookingConverteds
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID Booking Tidak Ditemukan",
                });
            }
            var bookingConverted = _mapper.Map(booking);
            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Booking By Id Berhasil Ditampilkan",
                Data = bookingConverted
            });
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var booking = _mapper.Map(bookingVM);
            var result = _bookingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data Booking Tidak Berhasil Ditambahkan",                    
                });
            }
            return Ok(new ResponseVM<Booking>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Booking Berhasil Ditambahkan",
                Data = result
            });

        }
        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var booking = _mapper.Map(bookingVM);
            var isUpdated = _bookingRepository.Update(booking);
            if (!isUpdated)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data Booking Tidak Berhasil Diperbarui",
                });
            }
            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Booking Berhasil Diperbarui",
                
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(new ResponseVM<BookingVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data Booking Gagal Dihapus",
                });
            }
            return Ok(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Booking Berhasil Dihapus",

            });
        }


    }
}
