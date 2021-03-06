﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AirportsDemo.App.Models;
using AirportsDemo.App.Services;
using AirportsDemo.App.Services.Impl;
using AirportsDemo.Tests.Mocks;
using System.Threading;

namespace AirportsDemo.Tests
{
    [TestClass]
    public class RouteFinderTest
    {
        private RouteFinder routeFinder = Factory.CreateMockedRouteFinder(8);

        [TestMethod]
        public void TestFindRouteForIncorrectAirports() {
            Flight[] route = routeFinder.FindRouteAsync("INCORRECT_AIRPORT_1", "INCORRECT_AIRPORT_2", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);
        }

        [TestMethod]
        public void TestFindRouteWhenAirlineIsInactive() {
            Flight[] route = routeFinder.FindRouteAsync("SVO", "VOZ", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);
        }

        [TestMethod]
        public void TestFindOneStepRoute() {
            Flight[] route = routeFinder.FindRouteAsync("SVO", "KBP", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(1, route.Length);
            Assert.AreEqual("SVO", route[0].SrcAirport);
            Assert.AreEqual("KBP", route[0].DestAirport);
            Assert.AreEqual("UJ", route[0].Airline);
        }

        [TestMethod]
        public void TestFindRouteInOneLine() {
            Flight[] route = routeFinder.FindRouteAsync("LINE_1", "LINE_3", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(2, route.Length);
            Assert.AreEqual("LINE_1", route[0].SrcAirport);
            Assert.AreEqual("LINE_2", route[0].DestAirport);
            Assert.AreEqual("LINE_2", route[1].SrcAirport);
            Assert.AreEqual("LINE_3", route[1].DestAirport);
        }

        [TestMethod]
        public void TestFindUnavailableRouteInCycle() {
            Flight[] route = routeFinder.FindRouteAsync("CYCLE_1", "VOZ", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);
        }

        [TestMethod]
        public void TestFindRouteWhenMaxDepthReached() {
            RouteFinder limitedRouteFinder = Factory.CreateMockedRouteFinder(2);
            Flight[] route = limitedRouteFinder.FindRouteAsync("LINE_1", "LINE_4", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(0, route.Length);

            route = routeFinder.FindRouteAsync("LINE_1", "LINE_4", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreNotEqual(0, route.Length);
        }

        [TestMethod]
        public void TestFindRouteFromSVOToDWC() {
            Flight[] route = routeFinder.FindRouteAsync("SVO", "DWC", CancellationToken.None).Result;
            Assert.IsNotNull(route);
            Assert.AreEqual(2, route.Length);
            Assert.AreEqual("SVO", route[0].SrcAirport);
            Assert.AreEqual("ODS", route[0].DestAirport);
            Assert.AreEqual("ODS", route[1].SrcAirport);
            Assert.AreEqual("DWC", route[1].DestAirport);
        }

        [TestMethod]
        public async Task TestFindRouteWhenServiceThrowsException() {
            bool isException = false;
            try {
                Flight[] route = await routeFinder.FindRouteAsync("HAS_ERROR_CHILD", "VOZ", CancellationToken.None);
            } catch (Exception e) {
                isException = true;
            }

            Assert.IsTrue(isException);
        }

    }
}
