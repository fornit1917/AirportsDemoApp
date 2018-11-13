using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportsDemo.App.Models
{
	public class Flight
	{
		public string Airline { get; private set; }
		public string SrcAirport { get; private set; }
		public string DestAirport { get; private set; }

		public Flight(string airline, string srcAirport, string destAirport)
		{
			Airline = airline;
			SrcAirport = srcAirport;
			DestAirport = destAirport;
		}
	}
}
