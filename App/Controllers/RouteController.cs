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
        private IFlightsService flightsService;

        public RouteController(IRouteFinder routeFinder, IFlightsService flightsService) {
            this.routeFinder = routeFinder;
            this.flightsService = flightsService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<Flight[]>> Search(string srcAirport, string destAirport) {
            ValidationResult airportValidationResult = await flightsService.ValidateAirportCodeAsync(srcAirport);
            if (!airportValidationResult.IsValid) {
                return GetInvalidAirportErrorResponse(airportValidationResult);
            }

            airportValidationResult = await flightsService.ValidateAirportCodeAsync(destAirport);
            if (!airportValidationResult.IsValid) {
                return GetInvalidAirportErrorResponse(airportValidationResult);
            }

            Flight[] route = await routeFinder.FindRouteAsync(srcAirport, destAirport);
            return Ok(route);
        }

        private ActionResult GetInvalidAirportErrorResponse(ValidationResult validationResult) {
            if (validationResult.ErrorType == ValidationErrorType.NotFound) {
                return NotFound(new { Message = validationResult.ErrorMessage });
            }
            return BadRequest(new { Message = validationResult.ErrorMessage });
        }
    }
}
