using AppointmentWebApi.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApi.Core.Dtos
{
    public class AppointmentDto
    {
        public long HallId { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }
        public DateTime ReservationFromDateTime { get; set; }
        public DateTime ReservationToDateTime { get; set; }
        public bool Approved { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
