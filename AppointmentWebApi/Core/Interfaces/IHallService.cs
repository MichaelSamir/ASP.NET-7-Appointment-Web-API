using AppointmentWebApi.Core.Dtos;

namespace AppointmentWebApi.Core.Interfaces
{
    public interface IHallService
    {
        Task<DataResultDto> SearchHallByIdAndDateTimeAsync(long hallId, DateTime reservationDateTime);
        Task<DataResultDto> SearchHallsByDateTimeAsync(DateTime reservationDateTime);
    }
}
