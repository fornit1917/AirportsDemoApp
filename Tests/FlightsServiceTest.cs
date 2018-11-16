using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services;
using AirportsDemo.App.Services.Impl;
using AirportsDemo.Tests.Mocks;

namespace AirportsDemo.Tests
{
	[TestClass]
	public class FlightsServiceTest
	{
		private FlightsService flightsService = Factory.CreateMockedFlightsService();

		[TestMethod]
		public void TestGetActiveOutgoingFlightsForIncorrectAirport()
		{
			List<Flight> flights = flightsService.GetActiveOutgoingFlightsAsync("INCORRECT_CODE").Result;
			Assert.IsNotNull(flights);
			Assert.AreEqual(0, flights.Count);
		}

		[TestMethod]
		public void TestGetActiveOutgoingFlights()
		{
			List<Flight> flights = flightsService.GetActiveOutgoingFlightsAsync("SVO").Result;
			Assert.IsNotNull(flights);
			Assert.AreEqual(2, flights.Count);
			Assert.AreEqual("UJ", flights[0].Airline);
			Assert.AreEqual("UJ", flights[1].Airline);
		}
	}
}
