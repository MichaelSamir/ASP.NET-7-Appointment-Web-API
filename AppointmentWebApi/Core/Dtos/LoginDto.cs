using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApi.Core.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Mobile is required")]
        //public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
