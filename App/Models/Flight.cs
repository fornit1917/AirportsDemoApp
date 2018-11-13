using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportsDemo.App.Models
{
	public class Flight
	{
		public string Airline { get; set; }
		public string SrcAirport { get; set; }
		public string DestAirport { get; set; }

		public Flight(string airline, string srcAirport, string destAirport)
		{
			Airline = airline;
			SrcAirport = srcAirport;
			DestAirport = destAirport;
		}
	}
}
