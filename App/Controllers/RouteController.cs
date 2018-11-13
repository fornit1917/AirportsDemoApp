using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RouteController : ControllerBase
	{
		[HttpGet("search")]
		public Task<Flight[]> Search(string srcAirport, string destAirport)
		{
			var testData = new Flight[2] { new Flight("Airline_1", srcAirport, "SomeAirport"), new Flight("Airline_2", "SomeAirport", destAirport) };
			return Task.FromResult(testData);
		}
	}
}
