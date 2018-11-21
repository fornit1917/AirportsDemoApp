using AirportsDemo.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportsDemo.App.Services
{
    public interface IFlightsApiClient
    {
        Task<Airline> GetAirlineAsync(string alias);
        Task<Flight[]> GetOutgoingFlightsAsync(string airportCode);
        Task<Airport[]> SearchAirports(string pattern);
    }
}
