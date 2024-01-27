using AppointmentWebApi.Core.Dtos;

namespace AppointmentWebApi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<DataResultDto> SeedRolesAsync();
        Task<DataResultDto> RegisterAsync(RegisterDto registerDto);
        Task<DataResultDto> LoginAsync(LoginDto loginDto);
    }
}
