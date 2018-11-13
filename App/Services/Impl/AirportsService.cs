using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services.Impl
{
	public class AirportsService : IAirportsService
	{
		private IFlightsApiClient apiClient;

		public AirportsService(IFlightsApiClient apiClient)
		{
			this.apiClient = apiClient;
		}

		public async Task<Airport> GetAirportAsync(string alias)
		{
			if (alias.Length < 3)
			{
				return null;
			}

			Airport[] airports = await apiClient.SearchAirportsAsync(alias);
			foreach (var airport in airports)
			{
				if (airport.Alias == alias)
				{
					return airport;
				}
			}

			return null;
		}
	}
}
