using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Bookings;
using API.ViewModels.Others;
using API.ViewModels.Roles;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{

    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomVM> _mapper;

    public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var rooms = _roomRepository.GetAll();
        if (!rooms.Any())
        {
            return NotFound(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Room Tidak Ditemukan",
            });
        }

        var data = rooms.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<RoomVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Room Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _roomRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID Room Tidak Ditemukan",
            });
        }

        var data = _mapper.Map(room);
        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Room By Id Berhasil Ditampilkan",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(RoomVM roomVM)
    {
        var roomConverted = _mapper.Map(roomVM);
        var result = _roomRepository.Create(roomConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Room Tidak Berhasil Ditambahkan",
            });
        }

        return Ok(new ResponseVM<Room>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Room Berhasil Ditambahkan",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(RoomVM roomVM)
    {
        var roomConverted = _mapper.Map(roomVM);
        var isUpdated = _roomRepository.Update(roomConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Room Tidak Berhasil Diperbarui",
            });
        }
    
        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Room Berhasil Diperbarui",

        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roomRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data Room Gagal Dihapus",
            });
        }

        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Room Berhasil Dihapus",

        });
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

