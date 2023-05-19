using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;
    public abstract class BaseEntity
//bikin abstract class biar kelasnya ga bisa di instansi hanya untuk property
{
    [Key]
    //tambah constraint
    [Column("guid")]
    public Guid Guid { get; set; }

    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }
}

