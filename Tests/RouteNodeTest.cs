using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services.Impl;

namespace AirportsDemo.Tests
{
    [TestClass]
    public class RouteNodeTest
    {
        [TestMethod]
        public void TestGetOneFlightRoute() {
            Flight flight = new Flight("airline", "src", "dest");
            RouteNode first = new RouteNode("src", null, null);
            RouteNode second = new RouteNode("dest", new Flight("airline", "src", "dest"), first);

            Flight[] route = second.GetFullRoute();
            Assert.IsNotNull(route);
            Assert.AreEqual(1, route.Length);
            Assert.AreEqual("airline", route[0].Airline);
            Assert.AreEqual("src", route[0].SrcAirport);
            Assert.AreEqual("dest", route[0].DestAirport);
        }

        [TestMethod]
        public void TestGetLongRout() {
            Flight[] flights = new Flight[3] {
                new Flight("airline_1", "airport_1", "airport_2"),
                new Flight("airline_2", "airport_2", "airport_3"),
                new Flight("airline_3", "airport_3", "airport_4"),
            };
            RouteNode node = new RouteNode("airport_1", null, null);
            foreach (var flight in flights) {
                node = new RouteNode(flight.DestAirport, flight, node);
            }

            Flight[] route = node.GetFullRoute();
            Assert.IsNotNull(route);
            Assert.AreEqual(3, route.Length);
            for (int i = 0; i < route.Length; i++) {
                Assert.AreEqual(flights[i].Airline, route[i].Airline);
                Assert.AreEqual(flights[i].SrcAirport, route[i].SrcAirport);
                Assert.AreEqual(flights[i].DestAirport, route[i].DestAirport);
            }
        }
    }
}
