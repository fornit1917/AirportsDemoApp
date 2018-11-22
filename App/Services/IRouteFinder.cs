using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services
{
    public interface IRouteFinder
    {
        Task<Flight[]> FindRouteAsync(string srcAirport, string destAirport, CancellationToken ct);
    }
}
