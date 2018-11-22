using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services.Impl
{
    public class RouteNode
    {
        public string Airport { get; private set; }
        public int Depth { get; private set; }

        private readonly Flight flight;
        private readonly RouteNode prevNode;

        public RouteNode(string airport, Flight flight, RouteNode prevNode) {
            this.prevNode = prevNode;
            this.flight = flight;
            Airport = airport;
            Depth = prevNode == null ? 0 : prevNode.Depth + 1;
        }

        public Flight[] GetFullRoute() {
            if (Depth == 0) {
                return Array.Empty<Flight>();
            }

            Flight[] result = new Flight[Depth];
            RouteNode node = this;
            for (int i = Depth; i > 0; i--) {
                result[i - 1] = node.flight;
                node = node.prevNode;
            }
            return result;
        }
    }
}
