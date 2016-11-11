using SimulationProjektarbeit.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationProjektarbeit
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new SimConfig();
            Console.WriteLine("[Config]");

            Console.Write("Zeitschritt: ");
            var tokens = Console.ReadLine().Split('/');
            if (tokens.Length == 2)
                config.Zeitschritt = double.Parse(tokens[0]) / double.Parse(tokens[1]);
            else
                config.Zeitschritt = double.Parse(tokens[0]);
            
            var queue = new SimQueue();
            Console.WriteLine("[Warteschlange]");

            Console.Write("Erwartungswert: ");
            queue.Erwartungswert = double.Parse(Console.ReadLine());

            var server = new SimServer();
            Console.WriteLine("[Server]");

            Console.Write("Erwartungswert: ");
            server.Erwartungswert = double.Parse(Console.ReadLine());

            var simulation = new Simulation(config, queue, new List<SimServer> { server });
            simulation.Create();
        }
    }
}
