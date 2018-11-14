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
		private AirlinesFixtures airlines = new AirlinesFixtures();
		private FlightsFixtures flights = new FlightsFixtures();

		public Task<Airline> GetAirlineAsync(string alias)
		{
			return Task.FromResult(airlines.GetAirline(alias));
		}

		public Task<Flight[]> GetOutgoingFlightsAsync(string airportCode)
		{
			return Task.FromResult(flights.GetOutgoingFlights(airportCode));
		}
	}
}
