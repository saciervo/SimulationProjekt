using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationProjektarbeit.Core
{
    /// <summary>
    /// Basis-Klasse für die Systeme in der Simulation (Warteschlange und Server)
    /// </summary>
    internal class SimulationsSystem
    {
        public double Erwartungswert { get; set; }

        /// <summary>
        /// Anzahl der Videos die momentan im System sind
        /// </summary>
        public int AnzahlVideos { get; set; }

        /// <summary>
        /// Die Wahrscheinlichkeit, dass bei einem einzelnen Zeitschritt ein neues Video im System landet
        /// </summary>
        public double Wahrscheinlichkeit { get; private set; }

        public void Calculate(Umgebung umgebung)
        {
            // Die Wahrscheinlichkeit ist exponentiell verteilt
            Wahrscheinlichkeit = 1 - Math.Exp(umgebung.Zeitschritt * -1 / Erwartungswert);
        }
    }
}
