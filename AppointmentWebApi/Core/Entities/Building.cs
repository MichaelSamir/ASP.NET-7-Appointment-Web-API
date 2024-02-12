using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApi.Core.Entities
{
    public class Building
    {
        public long Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public int NoOfFloors { get; set; }
        public int NoOfHalls { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
