using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services;

namespace AirportsDemo.Tests.Mocks
{
	class FlightsApiClientMock : IFlightsApiClient
	{
		public Task<Airline> GetAirlineAsync(string alias)
		{
			throw new NotImplementedException();
		}

		public Task<Flight[]> GetOutgoingFlightsAsync(string airportCode)
		{
			throw new NotImplementedException();
		}
	}
}
