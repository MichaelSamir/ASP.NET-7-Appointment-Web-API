using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Entities;
using System.Dynamic;

namespace AppointmentWebApi.Core.Interfaces
{
    public interface IHallService
    {
        Task<DataResultDto> GetHallAsync(HallDto hallInfo);
        Task<DataResultDto> SearchHallByIdAndDateTimeAsync(SearchHallDto searchHallInfo);
        Task<DataResultDto> SearchHallsByDateTimeAsync(SearchHallDto searchHallInfo);
    }
}
