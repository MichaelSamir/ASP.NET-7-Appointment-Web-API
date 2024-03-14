using AppointmentWebApi.Core.Dtos;

namespace AppointmentWebApi.Core.Interfaces
{
    public interface IBuildingService
    {
        Task<DataResultDto> GetActiveBuildingsAsync();
    }
}
