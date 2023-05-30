using API.Utility;

namespace API.ViewModels.Accounts;
    public class AccountVM
    {
    public Guid? Guid { get; set; }

    [PasswordValidation(ErrorMessage = "Minimal 6 character, 1 uppercase, 1 lower case, 1 Symbol, 1 number")]
    public string Password { get; set; }
    
    public bool IsDeleted { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }
}

