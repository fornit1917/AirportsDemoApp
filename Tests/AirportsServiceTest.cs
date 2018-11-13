using AirportsDemo.App.Services.Impl;
using AirportsDemo.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportsDemo.Tests
{
	[TestClass]
	public class AirportsServiceTest
	{
		private AirportsService airportsService = new AirportsService(new FlightsApiClientMock());

		[TestMethod]
		public void TestGetAirportWhenSearchResultIsEmpty()
		{
			Assert.IsNull(airportsService.GetAirportAsync("EMPTY").Result);
		}

		[TestMethod]
		public void TestGetAirportByCorrectCode()
		{
			var airport = airportsService.GetAirportAsync("CODE").Result;
			Assert.IsNotNull(airport);
			Assert.AreEqual("CODE", airport.Alias);
		}

		[TestMethod]
		public void TestGetAirportWhenQueryIsCityName()
		{
			Assert.IsNull(airportsService.GetAirportAsync("CITY_NAME").Result);
		}
	}
}
