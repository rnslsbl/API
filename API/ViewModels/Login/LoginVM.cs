using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Login;
    public class LoginVM
    {
    //k3
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}

