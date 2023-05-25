using API.Contexts;
using API.Models;
using API.Contracts;
using API.Repositories;
using API.ViewModels.Bookings;
using Microsoft.AspNetCore.Http;
using API.ViewModels.Employees;
using API.ViewModels.Rooms;
using System;

namespace API.Repositories;
public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    
    public BookingRepository(BookingManagementDbContext context) : base(context) {
        
    }

    //get by guid
    public BookingDetailVM GetBookingDetailByGuid(Guid guid)
    {
        var booking = GetByGuid(guid);
        var employee = _context.Employees.Find(booking.EmployeeGuid);
        var room = _context.Rooms.Find(booking.RoomGuid);
       
        var bookingDetail = new BookingDetailVM
        {
            Guid = booking.Guid,
            BookedNIK = employee.NIK,
            Fullname = employee.FirstName + " " + employee.LastName,
            RoomName = room.Name,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks
        };

        return bookingDetail;
    }
    //get all
   
    public IEnumerable<BookingDetailVM> GetAllBookingDetail()
    {
        var bookings = GetAll();
        var employees = _context.Employees.ToList();
        var rooms = _context.Rooms.ToList();
        var results = new List<BookingDetailVM>();
        var bookingDetail = from b in bookings
                            join e in employees on b.EmployeeGuid equals e.Guid
                            join r in rooms on b.RoomGuid equals r.Guid
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

        }
        return results;
    }

    //K3
    private int CalculateBookingDuration(DateTime startDate, DateTime endDate)
    {
        int totalDays = 0; // Untuk menghitung hari
        DateTime currentDate = startDate.Date; // Perhitungan tergantung dari tanggal yang digunakan

        while (currentDate <= endDate.Date)
        {
            // Mengecek apakah currentDate adalah hari kerja (Selain sabtu dan minggu) 
            if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                // Jika currentDate hari kerja, totalDays bertambah
                totalDays++;
            }
            currentDate = currentDate.AddDays(1); // untuk maju ke tanggal berikutnya.
        }

        return totalDays;
    }

    //K3
    public IEnumerable<BookingDurationVM> GetBookingDuration()
    {
        var rooms = _context.Rooms.ToList();
        var bookings = GetAll();
        var bookingduration = bookings.Select(b => new BookingDurationVM
        {
            RoomName = rooms.FirstOrDefault(r => r.Guid == b.RoomGuid)?.Name, 
            DurationOfBooking = CalculateBookingDuration(b.StartDate, b.EndDate)
        });

        return bookingduration;
    }

}



/*
    public Booking Create(Booking booking)
    {
        try
        {
            _context.Set<Booking>().Add(booking);
            _context.SaveChanges();
            return booking;
        }
        catch
        {
            return new Booking();
        }
    }
    public bool Update(Booking booking)
    {
        try
        {
            _context.Set<Booking>().Update(booking);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Delete(Guid guid)
    {
        try
        {
            var booking = GetByGuid(guid);
            if (booking == null)
            {
                return false;
            }
            _context.Set<Booking>().Remove(booking);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<Booking> GetAll()
    {
        return _context.Set<Booking>().ToList();

    }

    public Booking? GetByGuid(Guid guid)
    {
        return _context.Set<Booking>().Find(guid);
    }*/

