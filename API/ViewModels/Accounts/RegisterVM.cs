using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Accounts;
    public class RegisterVM
    {
    //k2
    [Required(ErrorMessage ="First Name is Required ")]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public GenderLevel Gender { get; set; }

    public DateTime HiringDate { get; set; }

    [EmailAddress]
    [NIKEmailPhoneValidation(nameof(Email))]
    public string Email { get; set; }

    [Phone]
    [NIKEmailPhoneValidation(nameof(PhoneNumber))]
    public string PhoneNumber { get; set; }

    public string Major { get; set; }

    public string Degree { get; set; }

    [Range(0, 4, ErrorMessage = "Input a Valid Number")]
    public float Gpa { get; set; }

    public string UniversityCode { get; set; }

    public string UniversityName { get; set; }

    [PasswordValidation(ErrorMessage = "Minimal 6 character, 1 uppercase, 1 lower case, 1 Symbol, 1 number")]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}

