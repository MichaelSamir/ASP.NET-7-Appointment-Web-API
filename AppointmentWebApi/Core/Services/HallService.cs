using AppointmentWebApi.Core.DbContext;
using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Entities;
using AppointmentWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<DataResultDto> SearchHallByIdAndDateTimeAsync(long hallId, DateTime reservationDateTime)
        {
            var hallInDb = await _context.Halls.FindAsync(hallId);

            DataResultDto resultDto = new DataResultDto();

            if (hallInDb == null)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.NotFound).ToString();
                resultDto.IsSucceeded = true;
                resultDto.Result = "Hall Not Found";
                return resultDto;
            }

            if(reservationDateTime == DateTime.MinValue)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = true;
                resultDto.Result = "Reservation DateTime not valid";
                return resultDto;
            }

            var appointment = _context.Appointments
                .Where(a => (a.ReservationFromDateTime == reservationDateTime
                         || (a.ReservationFromDateTime < reservationDateTime && reservationDateTime < a.ReservationFromDateTime))
                         && a.Active == true
                         && a.HallId == hallId);

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

        public async Task<DataResultDto> SearchHallsByDateTimeAsync(DateTime reservationDateTime)
        {
            DataResultDto resultDto = new DataResultDto();

            if (reservationDateTime == DateTime.MinValue)
            {
                resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                resultDto.IsSucceeded = true;
                resultDto.Result = "Reservation DateTime not valid";
                return resultDto;
            }

            var appointments = await _context.Appointments
                .Where(a => (a.ReservationFromDateTime == reservationDateTime
                         || (a.ReservationFromDateTime < reservationDateTime && reservationDateTime < a.ReservationFromDateTime))
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
