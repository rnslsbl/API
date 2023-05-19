using API.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace API.Models;
//nama tabel taro diatas kelas
[Table("tb_m_employees")]
    public class Employee : BaseEntity
{
    [Column("nik", TypeName = "nchar(6)")]
    public string NIK { get; set; }

    [Column("first_name", TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [Column("last_name", TypeName = "nvarchar(100)")]
    //tanda tanya adalah boleh null
    public string? LastName { get; set; }

    [Column("birth_date")]
    public DateTime BirthDate { get; set; }

    [Column("gender")]
    public GenderLevel Gender { get; set; }

    [Column("hiring_date")]
    public DateTime HiringDate { get; set; }

    [Column("email", TypeName = "nvarchar(100)")]
    public string Email { get; set; }

    [Column("phone_number", TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; }

    //cardinality
    public Education? Education { get; set; }
    public Account? Account { get; set; }
    public ICollection<Booking>? Bookings { get; set; }
}

