using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Bookings;
using API.ViewModels.Employees;
using API.ViewModels.Others;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoomController : BaseController<Room, RoomVM>
{

    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomVM> _mapper;

    public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper) : base (roomRepository, mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;

    }
    [HttpGet("AvailableRoom")]
    public IActionResult GetAvailableRoom()
    {
        try
        {
            var room = _roomRepository.GetAvailableRoom();
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomBookedTodayVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Available Room Tidak Ditemukan",
                });
            }

            return Ok(new ResponseVM<List<RoomBookedTodayVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Room By Id Berhasil Ditampilkan",
                Data = room.ToList()
            });
        }
        catch
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    //k1
    [HttpGet("CurrentlyUsedRooms")]
    public IActionResult GetCurrentlyUsedRooms()
    {
        var room = _roomRepository.GetCurrentlyUsedRooms();
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomUsedVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Ruangan yang Sedang Digunakan Tidak Ditemukan",
            });
        }

        return Ok(new ResponseVM<List<RoomUsedVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Ruangan yang Sedang Digunakan Berhasil Ditampilkan",
            Data = room.ToList()
        });
    }

    [HttpGet("CurrentlyUsedRoomsByDate")]
    public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
    {
        var room = _roomRepository.GetByDate(dateTime);
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomUsedVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Ruangan yang Sedang Digunakan Tidak Ditemukan",
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Ruangan yang Sedang Digunakan Berhasil Ditampilkan",
            Data = room
        });
    }
    private string GetRoomStatus(Booking booking, DateTime dateTime)
    {

        if (booking.StartDate <= dateTime && booking.EndDate >= dateTime)
        {
            return "Occupied";
        }
        else if (booking.StartDate > dateTime)
        {
            return "Booked";
        }
        else
        {
            return "Available";
        }
    }
    // End Kelompok 1
}

