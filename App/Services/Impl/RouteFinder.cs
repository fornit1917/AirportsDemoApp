using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Services;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services.Impl
{
	public class RouteFinder : IRouteFinder
	{
		private IFlightsService flightsService;

		public RouteFinder(IFlightsService flightsService)
		{
			this.flightsService = flightsService;
		}

		public Task<Flight[]> FindRouteAsync(string srcAirport, string destAirport, int maxDepth = 10)
		{
			RouteFinderContext ctx = new RouteFinderContext(destAirport, maxDepth);
			RunFinderSubTaskAsync(srcAirport, null, ctx);
			return ctx.ResultTask;
		}

		private void RunFinderSubTaskAsync(string airport, RouteNode prevRouteNode, RouteFinderContext ctx)
		{
			if (ctx.IsAirportVisited(airport) || ctx.CancellationToken.IsCancellationRequested)
			{
				return;
			}

			ctx.MarkAirportAsVisited(airport);
			ctx.IncrementTasksCount();

			Task.Run(async () =>
			{
				try
				{
					if (ctx.CancellationToken.IsCancellationRequested)
					{
						return;
					}

					List<Flight> outgoingFlights = await flightsService.GetActiveOutgoingFlightsAsync(airport);
					if (outgoingFlights.Count == 0 || ctx.CancellationToken.IsCancellationRequested)
					{
						return;
					}

					Flight finishFlight = outgoingFlights.Find(flight => flight.DestAirport == ctx.destAirport);
					if (finishFlight != null)
					{
						RouteNode routeNode = new RouteNode(finishFlight, prevRouteNode);
						ctx.SetResult(routeNode.GetFullRoute());
					}
					else
					{
						int depth = prevRouteNode != null ? prevRouteNode.Depth + 1 : 0;
						if (depth < ctx.maxDepth)
						{
							foreach (var flight in outgoingFlights)
							{
								RouteNode nextRouteNode = new RouteNode(flight, prevRouteNode);
								RunFinderSubTaskAsync(flight.DestAirport, nextRouteNode, ctx);
							}
						}
					}
				}
				catch (Exception e)
				{
					ctx.SetException(e);
				}
				finally
				{
					if (ctx.DecrementTasksCount() == 0)
					{
						ctx.SetResult(Array.Empty<Flight>());
					}
				}
			});
		}
	}
}
