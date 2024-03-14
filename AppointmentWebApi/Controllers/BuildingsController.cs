using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Interfaces;
using AppointmentWebApi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpPost]
        [Route("GetActiveBuildings")]
        public async Task<IActionResult> GetActiveBuildings()
        {
            var res = await _buildingService.GetActiveBuildingsAsync();
            return Ok(res);
        }
    }
}
