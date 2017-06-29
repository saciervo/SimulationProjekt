using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruinwahrscheinlichkeit
{
    public static class Global
    {
        public const int RuinGrenze = 0;
        public const int Einsatz = 1;
        public const double GewinnWahrscheinlichkeit = 0.48648d;

        private static readonly Random Zufall = new Random();

        private static int SimuliereRuinPfade(int anzahlSimulationen)
        {
            var ruinPfade = 0;
            for (int i = 0; i < anzahlSimulationen; i++)
            {
                var kapital = SimuliereKapitalpfad();
                if (kapital <= RuinGrenze)
                {
                    // Kapital ist aufgebraucht, die Simulation endete also im Ruin
                    ruinPfade++;
                }
            }
            return ruinPfade;
        }

        public static int SimuliereKapitalpfad()
        {
            var kapital = 40;
            var zielKapital = 80;

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
    }
}
