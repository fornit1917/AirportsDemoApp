using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services;

namespace AirportsDemo.App.Services.Impl
{
	public class AirlinesCache : IAirlinesCache
	{
		private ConcurrentDictionary<string, Airline> airlinesDictionary = new ConcurrentDictionary<string, Airline>();

		public Airline Get(string alias)
		{
			return airlinesDictionary.GetValueOrDefault(alias);
		}

		public void Set(Airline airline)
		{
			airlinesDictionary.TryAdd(airline.Alias, airline);
		}
	}
}
