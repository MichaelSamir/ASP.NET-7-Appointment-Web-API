using AppointmentWebApi.Core.DbContext;
using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AppointmentWebApi.Core.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly ApplicationDbContext _context;

        public BuildingService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<DataResultDto> GetActiveBuildingsAsync()
        {
            var activeBuildings = await _context.Buildings.Where(b=>b.Active == true).ToListAsync();

            DataResultDto resultDto = new DataResultDto();
            resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
            resultDto.IsSucceeded = true;
            resultDto.Result = activeBuildings;

            return resultDto;
        }
    }
}
