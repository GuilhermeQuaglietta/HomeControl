using System.ComponentModel.DataAnnotations;

namespace HomeControl.AccessControl.WebApi.Requests.Login
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
