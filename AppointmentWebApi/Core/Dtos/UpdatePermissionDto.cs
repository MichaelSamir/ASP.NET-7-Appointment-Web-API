using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApi.Core.Dtos
{
    public class UpdatePermissionDto
    {
        //[Required(ErrorMessage = "UserName is required")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }
    }
}
