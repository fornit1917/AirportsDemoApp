using System;
using System.Collections.Generic;
using System.Text;
using AirportsDemo.App.Models;

namespace AirportsDemo.Tests.Mocks
{
	class FlightsFixtures
	{
		private Dictionary<string, Flight[]> data;

		public FlightsFixtures()
		{
			data = new Dictionary<string, Flight[]>();

			data.Add("SVO", new Flight[] {
				new Flight("UJ", "SVO", "KBP"),
				new Flight("UJ", "SVO", "ODS"),
				new Flight("INAKTIVE_AIRLINE", "SVO", "VOZ"),
			});
		}

		public Flight[] GetOutgoingFlights(string airportCode)
		{
			Flight[] flights;
			bool exists = data.TryGetValue(airportCode, out flights);
			return exists ? flights : Array.Empty<Flight>();
		}
	}
}
