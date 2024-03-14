using AppointmentWebApi.Core.Dtos;

namespace AppointmentWebApi.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<DataResultDto> CreateAppointmentAsync(AppointmentDto appointmentDto);
    }
}
