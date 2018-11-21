using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services;

namespace AirportsDemo.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private IRouteFinder routeFinder;

        public RouteController(IRouteFinder routeFinder) {
            this.routeFinder = routeFinder;
        }

        [HttpGet("search")]
        public async Task<ActionResult<Flight[]>> Search(string srcAirport, string destAirport) {
            Flight[] route = await routeFinder.FindRouteAsync(srcAirport, destAirport);
            return Ok(route);
        }
    }
}
