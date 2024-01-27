using Microsoft.AspNetCore.Identity;

namespace AppointmentWebApi.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string ServiceName { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Active { get; set; }
    }
}
