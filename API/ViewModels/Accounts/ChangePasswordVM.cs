using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Accounts;
    
//k6
public class ChangePasswordVM
    {
    [EmailAddress]
    [NIKEmailPhoneValidation(nameof(Email))]
    public string Email { get; set; }

    public int OTP { get; set; }

    [PasswordValidation(ErrorMessage = "Minimal 6 character, 1 uppercase, 1 lower case, 1 Symbol, 1 number")]
    public string NewPassword { get; set; }
    
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}

