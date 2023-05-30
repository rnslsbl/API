using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Employees;
    public class MasterEmployeeVM
    {
    //K1
    public Guid? Guid { get; set; }
    public string NIK { get; set; }

    public string FullName { get; set; }

    public DateTime BirthDate { get; set; }

    public GenderLevel Gender { get; set; }

    public DateTime HiringDate { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone] //anotasi
    public string PhoneNumber { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float GPA { get; set; }
    public string UniversityName { get; set; }
}
