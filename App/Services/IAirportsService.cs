using AirportsDemo.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportsDemo.App.Services
{
	public interface IAirportsService
	{
		Task<Airport> GetAirportAsync(string code);
	}
}
