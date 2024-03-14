using AppointmentWebApi.Core.DbContext;
using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Entities;
using AppointmentWebApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace AppointmentWebApi.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<DataResultDto> CreateAppointmentAsync(AppointmentDto appointmentDto)
        {
            DataResultDto dataResultDto;
            if (appointmentDto == null)
            {
                dataResultDto = new DataResultDto()
                {
                    StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                    IsSucceeded = false,
                    HasErrors = true,
                    Result = "null appointmentDto",
                    Errors = new List<Error> { new Error { ErrorMessage = "null appointmentDto" } }
                };

                return dataResultDto;
            }

            if (appointmentDto.HallId <= 0
                || appointmentDto.ReservationFromDateTime <= DateTime.MinValue
                || appointmentDto.ReservationToDateTime <= DateTime.MinValue
                || appointmentDto.ReservationFromDateTime < DateTime.Now
                || appointmentDto.ReservationToDateTime < DateTime.Now)
            {
                dataResultDto = new DataResultDto()
                {
                    StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                    IsSucceeded = false,
                    HasErrors = true,
                    Result = "invalid appointmentDto data",
                    Errors = new List<Error> { new Error { ErrorMessage = "invalid appointmentDto data" } }
                };

                return dataResultDto;
            }

            var hallInDb = await _context.Halls.Where(h => h.Active == true && h.Id == appointmentDto.HallId).ToListAsync();

            if (hallInDb == null || hallInDb.Count <= 0)
            {
                dataResultDto = new DataResultDto()
                {
                    StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
                    IsSucceeded = false,
                    HasErrors = true,
                    Result = "invalid appointmentDto HallId",
                    Errors = new List<Error> { new Error { ErrorMessage = "invalid appointmentDto HallId" } }
                };

                return dataResultDto;
            }

          var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(currentUserId) || string.IsNullOrWhiteSpace(currentUserId))
            {
                dataResultDto = new DataResultDto()
                {
                    StatusCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                    IsSucceeded = false,
                    HasErrors = true,
                    Result = "invalid user id",
                    Errors = new List<Error> { new Error { ErrorMessage = "invalid user id" } }
                };

                return dataResultDto;
            }

            Appointment appointment = new Appointment {
                HallId = appointmentDto.HallId,
                UserId = currentUserId,
                ReservationFromDateTime = appointmentDto.ReservationFromDateTime,
                ReservationToDateTime = appointmentDto.ReservationToDateTime,
                Created = DateTime.Now,
                Active = true,
                Approved = true
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            dataResultDto = new DataResultDto()
            {
                StatusCode = ((int)HttpStatusCode.Created).ToString(),
                IsSucceeded = true,
                HasErrors = false,
                Result = appointment
            };

            return dataResultDto;
        }
    }
}
