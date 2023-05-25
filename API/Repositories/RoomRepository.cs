using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.ViewModels.Educations;
using API.ViewModels.Employees;
using API.ViewModels.Universities;
using System.Globalization;
using API.Repositories;

namespace API.Repositories;
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly IBookingRepository _contextBooking;
        public RoomRepository(BookingManagementDbContext context, IBookingRepository booking) : base (context)
        {
            _contextBooking = booking;
        }

/*    public RoomBookedTodayVM GetRoomByGuid(Guid bookingGuid)
    {
        var entity = _context.Set<RoomBookedTodayVM>().Find(bookingGuid);
        _context.ChangeTracker.Clear();
        return entity;
    }*/

    public IEnumerable<RoomBookedTodayVM> GetAvailableRoom()
        {
            try
            {
                //get all data from booking and rooms
                var booking = _contextBooking.GetAll();
                var rooms = GetAll();

                var startToday = DateTime.Today;
                var endToday = DateTime.Today.AddHours(23).AddMinutes(59);

                var roomUse = rooms.Join(booking, Room => Room.Guid, booking => booking.RoomGuid, (Room, booking) => new { Room, booking })
                        .Select(joinResult => new {
                            joinResult.Room.Name,
                            joinResult.Room.Floor,
                            joinResult.Room.Capacity,
                            joinResult.booking.StartDate,
                            joinResult.booking.EndDate
                        }
                 );

                var roomUseTodays = new List<RoomBookedTodayVM>();
           

            foreach (var room in roomUse)
            {
                if ((room.StartDate < startToday && room.EndDate < startToday) || (room.StartDate > startToday && room.EndDate > endToday))
                {
                    var roomDay = new RoomBookedTodayVM
                    {
                        RoomName = room.Name,
                        Floor = room.Floor,
                        Capacity = room.Capacity
                    };
                    roomUseTodays.Add(roomDay);
                }
            }
            return roomUseTodays;
            }

            catch
            {
                return null;

            }
        }
    }
