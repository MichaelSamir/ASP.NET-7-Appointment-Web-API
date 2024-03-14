using AppointmentWebApi.Core.Dtos;
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
        [Route("GetHall")]
        public async Task<IActionResult> GetHall([FromBody] HallDto hallInfo)
        {
            var res = await _hallService.GetHallAsync(hallInfo);
            return Ok(res);
        }

        [HttpPost]
        [Route("SearchHallByIdAndDateTime")]
        public async Task<IActionResult> SearchHallByNameAndDateTime([FromBody] SearchHallDto searchHallInfo) 
        {
            var res = await _hallService.SearchHallByIdAndDateTimeAsync(searchHallInfo);
            return Ok(res);
        }

        [HttpPost]
        [Route("SearchHallsByDateTime")]
        public async Task<IActionResult> SearchHallsByDateTime([FromBody] SearchHallDto searchHallInfo)
        {
            var res = await _hallService.SearchHallsByDateTimeAsync(searchHallInfo);
            return Ok(res);
        }
    }
}
