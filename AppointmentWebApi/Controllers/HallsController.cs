using AppointmentWebApi.Core.Interfaces;
using AppointmentWebApi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController : ControllerBase
    {
        IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpPost]
        [Route("SearchHallByIdAndDateTime")]
        public async Task<IActionResult> SearchHallByNameAndDateTime(long hallId, DateTime reservationDateTime) 
        {
            var res = await _hallService.SearchHallByIdAndDateTimeAsync(hallId, reservationDateTime);
            return Ok(res);
        }

        [HttpPost]
        [Route("SearchHallsByDateTime")]
        public async Task<IActionResult> SearchHallsByDateTime([FromBody] DateTime reservationDateTime)
        {
            var res = await _hallService.SearchHallsByDateTimeAsync(reservationDateTime);
            return Ok(res);
        }
    }
}
