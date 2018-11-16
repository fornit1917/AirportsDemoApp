using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services.Impl
{
    public class RouteNode
    {
        private readonly RouteNode prevNode;
        public int Depth { get; private set; }
        public Flight Flight { get; private set; }

        public RouteNode(Flight flight, RouteNode prevNode) {
            this.prevNode = prevNode;
            Flight = flight;
            Depth = prevNode == null ? 1 : prevNode.Depth + 1;
        }

        public Flight[] GetFullRoute() {
            Flight[] result = new Flight[Depth];
            RouteNode node = this;
            for (int i = Depth - 1; i > -1; i--) {
                result[i] = node.Flight;
                node = node.prevNode;
            }
            return result;
        }
    }
}
