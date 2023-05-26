namespace API.ViewModels.Accounts;
    
//k6
public class ChangePasswordVM
    {
    public string Email { get; set; }
    public int OTP { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}

