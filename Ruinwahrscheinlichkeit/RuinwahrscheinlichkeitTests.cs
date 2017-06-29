using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ruinwahrscheinlichkeit
{
    [TestClass]
    public class RuinwahrscheinlichkeitTests
    {
        // (Konstante) Parameter
        public const int RuinGrenze = 0;
        public const int Einsatz = 1;
        public const double GewinnWahrscheinlichkeit = 0.48648d;

        // Zufallszahlgenerator
        private static readonly Random Zufall = new Random();

        private static string SimuliereRuinpfade(int anzahlSimulationen, int anfangsKapital = 40, int zielKapital = 80)
        {
            var ruinPfade = 0;
            for (int i = 0; i < anzahlSimulationen; i++)
            {
                var kapital = SimuliereKapitalpfad(anfangsKapital, zielKapital);
                if (kapital <= RuinGrenze)
                {
                    // Kapital ist aufgebraucht, die Simulation endete also im Ruin
                    ruinPfade++;
                }
            }

            // Die Ruinwahrscheinlichkeit ist gleich der Anzahl Ruinpfade geteilt durch die Anzahl Simulationen
            var ruinWahrscheinlichkeit = ruinPfade / (double) anzahlSimulationen;

            // Rückgabewert als Zahl mit 5 Nachkommastellen formatieren
            return ruinWahrscheinlichkeit.ToString("0.00000");
        }

        public static int SimuliereKapitalpfad(int anfangsKapital, int zielKapital)
        {
            var kapital = anfangsKapital;
            while (kapital > RuinGrenze && kapital < zielKapital)
            {
                var zufallsZahl = Zufall.NextDouble();
                if (zufallsZahl >= GewinnWahrscheinlichkeit)
                {
                    kapital -= Einsatz;
                }
                else
                {
                    kapital += Einsatz;
                }
            }

            return kapital;
        }

        [TestMethod]
        public void Aufgabe1()
        {
            Console.WriteLine("Ruinwahrscheinlichkeit mit unterschiedlich vielen Simulationspfaden");
            Console.WriteLine($"R_1000 = {SimuliereRuinpfade(1000)}");
            Console.WriteLine($"R_5000 = {SimuliereRuinpfade(5000)}");
            Console.WriteLine($"R_10000 = {SimuliereRuinpfade(10000)}");
            Console.WriteLine($"R_50000 = {SimuliereRuinpfade(50000)}");
            Console.WriteLine($"R_100000 = {SimuliereRuinpfade(100000)}");
        }

        [TestMethod]
        public void Aufgabe2()
        {
            Console.WriteLine("100 Schätzungen der Ruinwahrscheinlichkeit mit je 10000 Simulationspfade");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"R^{i}_10000 = {SimuliereRuinpfade(10000)}");
            }
        }

        [TestMethod]
        public void Aufgabe3()
        {
            Console.WriteLine("Ruinwahrscheinlichkeit in Abhängikeit zum Anfangskapital (0-80)");
            for (int i = 0; i <= 80; i++)
            {
                Console.WriteLine($"R_10000({i}) = {SimuliereRuinpfade(10000, anfangsKapital: i)}");
            }
        }
    }
}
