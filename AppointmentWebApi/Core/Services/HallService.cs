using AppointmentWebApi.Core.DbContext;
using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Entities;
using AppointmentWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Dynamic;
using System.Net;

namespace AppointmentWebApi.Core.Services
{
    public class HallService : IHallService
    {
        private readonly ApplicationDbContext _context;

        public HallService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataResultDto> GetHallAsync(HallDto hallInfo)
        {
            DataResultDto resultDto = new DataResultDto();

            if (hallInfo == null)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid hall Info";
                return resultDto;
            }

            if (hallInfo.BuildingId <= 0)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid BuildingId";
                return resultDto;
            }

            if (hallInfo.FloorNumber <= 0)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid FloorNumber";
                return resultDto;
            }

            var availableHalls = await _context.Halls.Where(h =>
            h.BuildingId == hallInfo.BuildingId && h.FloorNumber == hallInfo.FloorNumber && h.Active == true).ToListAsync();

            resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
            resultDto.IsSucceeded = true;
            resultDto.Result = availableHalls;

            return resultDto;
        }

        public async Task<DataResultDto> SearchHallByIdAndDateTimeAsync(SearchHallDto searchHallInfo)
        {
            DataResultDto resultDto = new DataResultDto();

            if (searchHallInfo == null)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid searchHallInfo";
                return resultDto;
            }

            if (searchHallInfo.HallId <= 0)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid HallId";
                return resultDto;
            }

            if (searchHallInfo.ReservationDateTime <= DateTime.MinValue || searchHallInfo.ReservationDateTime <= DateTime.Now)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid ReservationDateTime";
                return resultDto;
            }

            var hallInDb = await _context.Halls.FindAsync(searchHallInfo.HallId);

            if (hallInDb == null)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.NotFound).ToString();
                resultDto.IsSucceeded = true;
                resultDto.Result = "Hall Not Found";
                return resultDto;
            }

            var appointment = _context.Appointments
                .Where(a => (a.ReservationFromDateTime == searchHallInfo.ReservationDateTime
                         || (a.ReservationFromDateTime < searchHallInfo.ReservationDateTime && searchHallInfo.ReservationDateTime < a.ReservationFromDateTime))
                         && a.Active == true
                         && a.HallId == searchHallInfo.HallId);

            if (appointment == null || appointment.Count() <= 0)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
                resultDto.IsSucceeded = true;
                resultDto.Result = "Hall available";

                _context.Dispose();

                return resultDto;
            }

            resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
            resultDto.IsSucceeded = true;
            resultDto.Result = "Hall Not available";

            _context.Dispose();

            return resultDto;
        }

        public async Task<DataResultDto> SearchHallsByDateTimeAsync(SearchHallDto searchHallInfo)
        {
            DataResultDto resultDto = new DataResultDto();
            if (searchHallInfo == null)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid searchHallInfo";
                return resultDto;
            }

            if (searchHallInfo.ReservationDateTime <= DateTime.MinValue || searchHallInfo.ReservationDateTime <= DateTime.Now)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = false;
                resultDto.HasErrors = true;
                resultDto.Result = "Invalid ReservationDateTime";
                return resultDto;
            }

            var appointments = await _context.Appointments
                .Where(a => (a.ReservationFromDateTime == searchHallInfo.ReservationDateTime
                         || (a.ReservationFromDateTime < searchHallInfo.ReservationDateTime && searchHallInfo.ReservationDateTime < a.ReservationFromDateTime))
                         && a.Active == true).ToListAsync();


            if (appointments == null || appointments.Count <= 0)
            {
                var halls = await _context.Halls.Where(h => h.Active == true).ToListAsync();

                resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
                resultDto.IsSucceeded = true;
                resultDto.Result = halls;

                _context.Dispose();

                return resultDto;
            }

            List<long> reservedHallsIds = new List<long>();

            foreach (Appointment appointment in appointments)
            {
                reservedHallsIds.Add(appointment.HallId);
            }

            var availableHalls = await _context.Halls.Where(h => !reservedHallsIds.Contains(h.Id) && h.Active == true).ToListAsync();

            resultDto = new DataResultDto();
            resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
            resultDto.IsSucceeded = true;
            resultDto.Result = availableHalls;

            _context.Dispose();

            return resultDto;
        }
    }
}
