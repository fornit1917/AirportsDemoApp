using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Services;
using AirportsDemo.App.Models;
using System.Threading;

namespace AirportsDemo.App.Services.Impl
{
    public class RouteFinder : IRouteFinder
    {
        private IFlightsService flightsService;
        private RouteFinderConfig config;

        public RouteFinder(IFlightsService flightsService, RouteFinderConfig config) {
            this.flightsService = flightsService;
            this.config = config;
        }

        public async Task<Flight[]> FindRouteAsync(string srcAirport, string destAirport, CancellationToken ct) {
            var visitedAirports = new HashSet<string> { srcAirport };
            var queue = new Queue<RouteNode>();
            queue.Enqueue(new RouteNode(srcAirport, null, null));

            while (!ct.IsCancellationRequested && queue.Count > 0) {
                List<RouteNode> parents = BatchDequeue(queue, config.MaxDegreeOfParallelism);

                List<Flight>[] childrenData = await Task.WhenAll(
                    parents.Select(item => flightsService.GetActiveOutgoingFlightsAsync(item.Airport))
                );

                if (ct.IsCancellationRequested) {
                    break;
                }

                for (int i = 0; i < childrenData.Length; i++) {
                    RouteNode parent = parents[i];
                    List<Flight> outgoingFlights = childrenData[i];
                    foreach (var flight in outgoingFlights) {
                        if (flight.DestAirport == destAirport) {
                            return new RouteNode(flight.DestAirport, flight, parent).GetFullRoute();
                        }

                        if (visitedAirports.Contains(flight.DestAirport)) {
                            continue;
                        }

                        visitedAirports.Add(flight.DestAirport);

                        if (parent.Depth + 1 < config.MaxRouteDepth) {
                            queue.Enqueue(new RouteNode(flight.DestAirport, flight, parent));
                        }
                    }
                }
            }

            if (ct.IsCancellationRequested) {
                ct.ThrowIfCancellationRequested();
            }

            return Array.Empty<Flight>();
        }

        private List<RouteNode> BatchDequeue(Queue<RouteNode> q, int batchSize) {
            var items = new List<RouteNode>();
            for (int i = 0; i < batchSize; i++) {
                if (q.TryDequeue(out RouteNode item)) {
                    items.Add(item);
                } else {
                    break;
                }
            }
            return items;
        }
    }
}
