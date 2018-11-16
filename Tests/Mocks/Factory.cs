using System;
using System.Collections.Generic;
using System.Text;
using AirportsDemo.App.Services;
using AirportsDemo.App.Services.Impl;

namespace AirportsDemo.Tests.Mocks
{
	class Factory
	{
		public static FlightsService CreateMockedFlightsService()
		{
			IFlightsApiClient flightsApiClient = new FlightsApiClientMock();
			IAirlinesCache airlinesCache = new AirlinesCache();
			return new FlightsService(flightsApiClient, airlinesCache);
		}

		public static RouteFinder CreateMockedRouteFinder()
		{
			return new RouteFinder(CreateMockedFlightsService());
		}
	}
}
