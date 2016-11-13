using System.Collections.Generic;
using SimulationProjektarbeit.Core;

namespace SimulationProjektarbeit.Simulations
{
    internal class SimulationOhneUmgebung : SimulationBase
    {
        public SimulationOhneUmgebung(Umgebung umgebung, Warteschlange warteschlange, List<Server> servers) : base(umgebung)
        {
            SetWarteschlange(new Warteschlange { Erwartungswert = warteschlange.Erwartungswert });
            Servers.Clear();
            foreach (var server in servers)
            {
                AddServer(new Server { Erwartungswert = server.Erwartungswert });
            }
        }

        internal int SimulateOneStep()
        {
            Warteschlange.Step();
            foreach (var server in Servers)
            {
                server.Step(Warteschlange);
            }
            return Warteschlange.AnzahlVideos;
        }

        internal override void CreateExcel()
        {
            throw new System.NotImplementedException();
        }
    }
}