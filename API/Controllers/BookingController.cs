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
            /*if (!bookingDetails.Any())
            {
                return NotFound();
            }*/
            /*var employeeDetail = _employeeRepository.GetAll();
            var roomDetail = _roomRepository.GetAll();
            var results = _bookingRepository.GetAllBookingDetail(bookingDetails, employeeDetail, roomDetail);*/


            /* var bookingDetail = from b in bookingDetails
                                 join e in employeeDetail on b.EmployeeGuid equals e.Guid
                                 join r in roomDetail on b.RoomGuid equals r.Guid
                                 select new
                                 {
                                     b.Guid,
                                     e.NIK,
                                     BookedBy = e.FirstName + " " + e.LastName,
                                     r.Name,
                                     b.StartDate,
                                     b.EndDate,
                                     b.Status,
                                     b.Remarks
                                 };



         foreach (var booking in bookingDetail)
         {
             var result = new BookingDetailVM
             {
                 Guid = booking.Guid,
                 BookedNIK = booking.NIK,
                 Fullname = booking.BookedBy,
                 RoomName = booking.Name,
                 StartDate = booking.StartDate,
                 EndDate = booking.EndDate,
                 Status = booking.Status,
                 Remarks = booking.Remarks

             };

             results.Add(result);

         }*/




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

                /*var employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
                var room = _roomRepository.GetByGuid(booking.RoomGuid);
                var result = _bookingRepository.GetBookingDetailByGuid(booking, employee, room);*/

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
    }
}
