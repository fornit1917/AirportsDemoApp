using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportsDemo.App
{
    public class RouteFinderConfig
    {
        public int MaxRouteDepth { get; set; }
        public int MaxDegreeOfParallelism { get; set; }
        public int RetryRequestCount { get; set; }
    }
}
