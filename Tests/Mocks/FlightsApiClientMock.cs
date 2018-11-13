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

		public Task<Airport[]> SearchAirportsAsync(string query)
		{
			if (query == "EMPTY")
			{
				return Task.FromResult(Array.Empty<Airport>());
			}
			if (query == "CITY_NAME")
			{
				return Task.FromResult(new Airport[1] { new Airport() { Alias = $"NOT_{query}", City = query, Country = "Some Country", Name = $"Airport {query}" } });
			}

			return Task.FromResult(new Airport[2]
			{
				new Airport() { Alias=query, City = "Some City", Country = "Some Country", Name = $"Airport {query}" },
				new Airport() { Alias=$"NOT_{query}", City = "Some City", Country = "Some Country", Name = "Some airport" },
			});
		}
	}
}
