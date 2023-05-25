using API.Models;
using API.ViewModels.Bookings;

namespace API.Contracts; 
    public interface IBookingRepository : IGenericRepository<Booking>
    {
    IEnumerable<BookingDetailVM> GetAllBookingDetail();
    BookingDetailVM GetBookingDetailByGuid(Guid guid);

    //IEnumerable<BookingDurationVM> GetBookingDuration();
}

