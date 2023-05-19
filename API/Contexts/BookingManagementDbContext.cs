using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;
public class BookingManagementDbContext : DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options)
    {

    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Employee>().HasIndex(e => new
        {
            e.NIK,
            e.Email,
            e.PhoneNumber
        }).IsUnique();


        //Relasi one to Many Education ke University (FK UNIVERSITY ID)
        builder.Entity<Education>()
            .HasOne(u => u.University)
            .WithMany(e => e.Educations)
            .HasForeignKey(e => e.UniversityGuid);

        //relasi one to one Education ke Employee (FK ID)
        builder.Entity<Education>()
            .HasOne(emp => emp.Employee)
            .WithOne(e => e.Education)
            .HasForeignKey<Education>(e => e.Guid);

        //ACCOUNT (DONE)

        //relasi one to one Account dan Employee (FK ID)
        builder.Entity<Account>()
            .HasOne(emp => emp.Employee)
            .WithOne(a => a.Account)
            .HasForeignKey<Account>(a => a.Guid);


        // ACCOUNT ROLE (DONE)

        //relasi one to many Account dan AccountRole (FK ACCOUNTGUID)
        builder.Entity<AccountRole>()
            .HasOne(a => a.Account)
            .WithMany(ac => ac.AccountRoles)
            .HasForeignKey(ac => ac.AccountGuid);

        //relasi one to many Account dan AccountRole (FK ROLEGUID)
        builder.Entity<AccountRole>()
            .HasOne(r => r.Role)
            .WithMany(ac => ac.AccountRoles)
            .HasForeignKey(ac => ac.RoleGuid);


        //BOOKING (DONE)

        //relasi one to many Room ke Booking 1 room ke banyak booking
        builder.Entity<Booking>()
            .HasOne(r => r.Room)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.RoomGuid);

        //relasi one to many booking- 1employee (FK EMPLOYEE ID)
        builder.Entity<Booking>()
            .HasOne(em => em.Employee)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.EmployeeGuid);



    }
}

