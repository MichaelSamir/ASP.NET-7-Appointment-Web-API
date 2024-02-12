using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApi.Core.Entities
{
    public class Appointment
    {
        public long Id { get; set; }
        public Hall Hall { get; set; }
        public long HallId { get; set; }
        public User User { get; set; }

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
