using System;
using System.Collections.Generic;
using System.Text;
using AirportsDemo.App.Models;

namespace AirportsDemo.Tests.Mocks
{
    class AirlinesFixtures
    {
        private Dictionary<string, Airline> data;

        public AirlinesFixtures() {
            data = new Dictionary<string, Airline>();
            data.Add("UJ", new Airline() { Active = true, Alias = "UJ", Name = "AlMasria Universal Airlines" });
            data.Add("FZ", new Airline() { Active = true, Alias = "FZ", Name = "Fly Dubai" });
            data.Add("INACTIVE_AIRLINE", new Airline() { Active = false, Alias = "INACTIVE_AIRLINE", Name = "Inactive airline" });
        }

        public Airline GetAirline(string alias) {
            Airline airline;
            return data.TryGetValue(alias, out airline) ? airline : null;
        }
    }
}
