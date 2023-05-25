using API.Models;
using API.ViewModels.Rooms;

namespace API.Contracts;
public interface IRoomRepository : IGenericRepository<Room>
{
  
   // RoomBookedTodayVM GetRoomByGuid(Guid guid);
    IEnumerable<RoomBookedTodayVM> GetAvailableRoom();

}

