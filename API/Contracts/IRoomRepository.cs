using API.Models;
using API.ViewModels.Rooms;

namespace API.Contracts;
public interface IRoomRepository : IGenericRepository<Room>
{
 
    IEnumerable<RoomBookedTodayVM> GetAvailableRoom();
    //k1
    IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
    IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();
    
}

