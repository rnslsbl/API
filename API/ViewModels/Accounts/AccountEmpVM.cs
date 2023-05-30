using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Accounts;
    public class AccountEmpVM
    {
    //k3
    [EmailAddress]
    public string Email { get; set; }

    [PasswordValidation(ErrorMessage = "Minimal 6 character, 1 uppercase, 1 lower case, 1 Symbol, 1 number")]
    public string Password { get; set; }
}

