using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationProjektarbeit.Utility
{
    /// <summary>
    /// Liefert  unabhängig von Threads (Pseudo-)Zufallszahlen 
    /// Adaptiert von: http://stackoverflow.com/a/19271062
    /// </summary>
    internal static class ThreadSafeRandom
    {
        private static int _seed = Environment.TickCount;
        static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        public static double Rand()
        {
            return Random.Value.NextDouble();
        }
    }
}
