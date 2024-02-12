using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApi.Core.Entities
{
    public class Hall
    {
        public long Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public int FloorNumber { get; set; }
        public Building Building { get; set; }
        public long BuildingId { get; set; }
        public int NoOfChairs { get; set; }
        public bool HasAirConditioner { get; set; }
        public bool HasSoundSystem { get; set; }
        public bool HasScreen { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime ReservationFromTime { get; set; }
        public DateTime ReservationToTime { get; set; }
        
        [StringLength(100)]
        public string ReservationDays { get; set; }
    }
}
