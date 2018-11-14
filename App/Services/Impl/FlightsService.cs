﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services;

namespace AirportsDemo.App.Services.Impl
{
	public class FlightsService : IFlightsService
	{
		private IFlightsApiClient apiClient;
		private IAirlinesCache airlinesCache;

		public FlightsService(IFlightsApiClient apiClient, IAirlinesCache airlinesCache)
		{
			this.apiClient = apiClient;
			this.airlinesCache = airlinesCache;
		}

		public async Task<List<Flight>> GetActiveOutgoingFlightsAsync(string airportCode)
		{
			Flight[] allFlights = await apiClient.GetOutgoingFlightsAsync(airportCode);
			List<Flight> result = new List<Flight>();
			foreach (var flight in allFlights)
			{
				bool isActive = await IsAirlineActive(flight.Airline);
				if (isActive)
				{
					result.Add(flight);
				}
			}
			return result;
		}

		private async Task<bool> IsAirlineActive(string airlineCode)
		{
			Airline airline = airlinesCache.Get(airlineCode);
			if (airline == null)
			{
				airline = await apiClient.GetAirlineAsync(airlineCode);
				if (airline != null)
				{
					airlinesCache.Set(airline);
				}
			}
			return airline != null ? airline.Active : false;
		}
	}
}
