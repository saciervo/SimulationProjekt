using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SimulationProjektarbeit.Core
{
    class SimParameter
    {
        protected readonly Random Rand = new Random();

        public double Erwartungswert { get; set; }
        public double Wahrscheinlichkeit { get; set; }
        public int Size { get; set; }

        public void Calculate(SimConfig config)
        {
            Wahrscheinlichkeit = 1 - Math.Exp(config.Zeitschritt * -1 / Erwartungswert);
        }
    }

    class SimQueue : SimParameter
    {
        public int Step()
        {
            if (Wahrscheinlichkeit >= Rand.NextDouble())
            {
                Size++;
                return 1;
            }

            return 0;
        }
    }

    class SimServer : SimParameter
    {
        public int Step(SimQueue queue)
        {
            if (queue.Size > 0)
            {
                if (Wahrscheinlichkeit >= Rand.NextDouble())
                {
                    queue.Size--;
                    Size++;
                    return 1;
                }
            }

            return 0;
        }
    }
}
