using System;
using System.Collections.Generic;
using System.Text;
using AirportsDemo.App.Models;

namespace AirportsDemo.Tests.Mocks
{
    class FlightsFixtures
    {
        private Dictionary<string, Flight[]> data;

        public FlightsFixtures() {
            data = new Dictionary<string, Flight[]>();

            data.Add("SVO", new Flight[] {
                new Flight("UJ", "SVO", "KBP"),
                new Flight("UJ", "SVO", "ODS"),
                new Flight("INAKTIVE_AIRLINE", "SVO", "VOZ"),
            });

            data.Add("KBP", new Flight[] {
                new Flight("UJ", "KBP", "SVO"),
                new Flight("UJ", "KBP", "SIP"),
                new Flight("UJ", "KBP", "ODS"),
                new Flight("UJ", "KBP", "IST"),
                new Flight("UJ", "KBP", "GYD"),
                new Flight("UJ", "KBP", "DOK"),
                new Flight("UJ", "KBP", "AID"),
                new Flight("INAKTIVE_AIRLINE", "SVO", "VOZ"),
            });

            data.Add("SIP", new Flight[] {
                new Flight("UJ", "SIP", "KBP"),
            });

            data.Add("ODS", new Flight[] {
                new Flight("FZ", "ODS", "DWC"),
                new Flight("UJ", "ODS", "KBP"),
                new Flight("UJ", "ODS", "SVO"),
            });

            data.Add("CYCLE_1", new Flight[1] { new Flight("UJ", "CYCLE_1", "CYCLE_2") });
            data.Add("CYCLE_2", new Flight[1] { new Flight("UJ", "CYCLE_2", "CYCLE_3") });
            data.Add("CYCLE_3", new Flight[1] { new Flight("UJ", "CYCLE_3", "CYCLE_1") });

            data.Add("LINE_1", new Flight[1] { new Flight("UJ", "LINE_1", "LINE_2") });
            data.Add("LINE_2", new Flight[1] { new Flight("UJ", "LINE_2", "LINE_3") });
            data.Add("LINE_3", new Flight[1] { new Flight("UJ", "LINE_3", "LINE_4") });

            data.Add("HAS_ERROR_CHILD", new Flight[1] { new Flight("UJ", "HAS_ERROR_CHILD", "THROW_ERROR") });
        }

        public Flight[] GetOutgoingFlights(string airportCode) {
            Flight[] flights;
            bool exists = data.TryGetValue(airportCode, out flights);
            return exists ? flights : Array.Empty<Flight>();
        }
    }
}
