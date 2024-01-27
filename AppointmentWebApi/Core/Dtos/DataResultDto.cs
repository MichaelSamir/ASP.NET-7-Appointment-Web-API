namespace AppointmentWebApi.Core.Dtos
{
    public class DataResultDto
    {
        public string StatusCode { get; set; }
        public object Result { get; set; }
        public bool IsSucceeded { get; set; }
        public bool HasErrors { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
    }
}
