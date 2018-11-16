using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirportsDemo.App.Models;

namespace AirportsDemo.App.Services.Impl
{
    public class RouteFinderContext
    {
        public readonly string destAirport;
        public readonly int maxDepth;
        public Task<Flight[]> ResultTask => resultTaskSource.Task;
        public CancellationToken CancellationToken => cts.Token;

        private int count;
        private readonly ConcurrentDictionary<string, bool> visitedAirports;
        private readonly TaskCompletionSource<Flight[]> resultTaskSource;
        private readonly CancellationTokenSource cts;

        public RouteFinderContext(string destAirport, int maxDepth) {
            count = 0;
            resultTaskSource = new TaskCompletionSource<Flight[]>();
            visitedAirports = new ConcurrentDictionary<string, bool>();
            cts = new CancellationTokenSource();
            this.destAirport = destAirport;
            this.maxDepth = maxDepth;
        }

        public int IncrementTasksCount() {
            return Interlocked.Increment(ref count);
        }

        public int DecrementTasksCount() {
            return Interlocked.Decrement(ref count);
        }

        public void MarkAirportAsVisited(string airport) {
            visitedAirports.TryAdd(airport, true);
        }

        public bool IsAirportVisited(string airport) {
            return visitedAirports.ContainsKey(airport);
        }

        public void SetResult(Flight[] result) {
            if (!cts.Token.IsCancellationRequested) {
                cts.Cancel();
                resultTaskSource.SetResult(result);
            }
        }

        public void SetException(Exception e) {
            if (!cts.Token.IsCancellationRequested) {
                cts.Cancel();
                resultTaskSource.SetException(e);
            }
        }
    }
}
