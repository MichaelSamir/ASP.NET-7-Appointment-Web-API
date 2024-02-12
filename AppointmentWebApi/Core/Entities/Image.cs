namespace AppointmentWebApi.Core.Entities
{
    public class Image
    {
        public long Id { get; set; }
        public string LinkedId { get; set; }
        public string Path { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool Active { get; set; }
    }
}
