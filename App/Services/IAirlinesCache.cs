using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services
{
    public interface IAirlinesCache
    {
        Airline Get(string alias);
        void Set(Airline airline);
    }
}
