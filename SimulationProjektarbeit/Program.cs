using SimulationProjektarbeit.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationProjektarbeit.Simulations;

namespace SimulationProjektarbeit
{
    class Program
    {
        static void Main(string[] args)
        {
            var umgebung = new Umgebung();
            Console.WriteLine("[Umgebung]");

            Console.Write("Zeitschritt: ");
            umgebung.Zeitschritt = ParseMoeglicheDivision(Console.ReadLine());
            Console.Write("Anzahl Server: ");
            umgebung.AnzahlServer = ParseInt(Console.ReadLine());

            var simulation = new EinPfadSimulation(umgebung);

            Console.WriteLine("[Warteschlange]");
            var warteschlange = new Warteschlange();

            Console.Write("Erwartungswert: ");
            warteschlange.Erwartungswert = ParseMoeglicheDivision(Console.ReadLine());
            simulation.SetWarteschlange(warteschlange);

            Console.WriteLine("[Server]");
            for (int i = 0; i < umgebung.AnzahlServer; i++)
            {
                var server = new Server();
                Console.Write($"Erwartungswert {i+1}. Server: ");
                server.Erwartungswert = ParseMoeglicheDivision(Console.ReadLine());
                simulation.AddServer(server);
            }
            
            simulation.CreateExcel();
        }

        private static double ParseDouble(string input)
        {
            // Falls der Wert nicht in ein double umgewandelt werden kann, wird der default Wert, also 0, zurückgegeben
            double result;

            double.TryParse(input, out result);
            return result;
        }

        private static int ParseInt(string input)
        {
            // Falls der Wert nicht in ein integer umgewandelt werden kann, wird der default Wert, also 0, zurückgegeben
            int result;

            int.TryParse(input, out result);
            return result;
        }

        private static double ParseMoeglicheDivision(string input)
        {
            // Wenn der Input ein trailing slash '/' enthält, ist es ein Bruch
            var tokens = input.Split('/');

            // Ein Token -> Keine Division
            if (tokens.Length == 1)
                return ParseDouble(tokens[0]); // Ein Token -> Keine Division

            // Mehrere Tokens -> Die Zahlen hintereinander dividieren
            var result = ParseDouble(tokens[0]);
            for (int i = 1; i < tokens.Length; i++)
            {
                result /= ParseDouble(tokens[i]);
            }
            return result;
        }
    }
}
