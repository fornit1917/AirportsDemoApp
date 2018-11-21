using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services
{
    public interface IFlightsService
    {
        Task<List<Flight>> GetActiveOutgoingFlightsAsync(string airportCode);
        Task<ValidationResult> ValidateAirportCodeAsync(string airportCode);
    }
}
