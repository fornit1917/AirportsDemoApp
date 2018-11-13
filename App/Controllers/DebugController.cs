using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
		private IFlightsApiClient flightsApiClient;

		public DebugController(IFlightsApiClient flightsApiClient)
		{
			this.flightsApiClient = flightsApiClient;
		}

		[HttpGet("airports")]
		public async Task<ActionResult<Airport[]>> Airports(string query)
		{
			try
			{
				return await flightsApiClient.SearchAirportsAsync(query);
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("airline")]
		public async Task<ActionResult<Airline>> Airline(string alias)
		{
			try
			{
				Airline airline = await flightsApiClient.GetAirlineAsync(alias);
				if (airline == null)
				{
					return NotFound("Airline not found");
				}
				return Ok(airline);
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("flights")]
		public async Task<ActionResult<Flight[]>> Flights(string airportCode)
		{
			try
			{
				return await flightsApiClient.GetOutgoingFlightsAsync(airportCode);
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

    }
}