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
        private AirlinesFixtures airlines = new AirlinesFixtures();
        private FlightsFixtures flights = new FlightsFixtures();

        public Task<Airline> GetAirlineAsync(string alias) {
            return Task.FromResult(airlines.GetAirline(alias));
        }

        public Task<Flight[]> GetOutgoingFlightsAsync(string airportCode) {
            if (airportCode == "THROW_ERROR") {
                throw new Exception("Service unavailable");
            }
            return Task.FromResult(flights.GetOutgoingFlights(airportCode));
        }

        public Task<Airport[]> SearchAirports(string pattern) {
            if (pattern == "EMPT") {
                return Task.FromResult(Array.Empty<Airport>());
            }

            return Task.FromResult(new Airport[] {
                new Airport() { Alias = pattern, City = "Some ciry" },
                new Airport() { Alias = "NOT_" + pattern, City = pattern + " City" },
            });
        }
    }
}
