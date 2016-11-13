using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationProjektarbeit.Core;

namespace SimulationProjektarbeit.Simulations
{
    /// <summary>
    /// Basis-Klasse für Simulationen
    /// </summary>
    internal abstract class SimulationBase
    {
        public Umgebung Umgebung { get; }

        protected Warteschlange Warteschlange;
        protected List<Server> Servers = new List<Server>();

        protected SimulationBase(Umgebung umgebung)
        {
            Umgebung = umgebung;
        }

        /// <summary>
        /// Die Warteschlange der Simulation festlegen
        /// </summary>
        /// <param name="warteschlange"></param>
        public void SetWarteschlange(Warteschlange warteschlange)
        {
            warteschlange.Calculate(Umgebung);
            Warteschlange = warteschlange;
            Console.Write($" => Wahrscheinlichkeit = {warteschlange.Wahrscheinlichkeit:P}\n");
        }

        /// <summary>
        /// Der Simulation einen Server hinzufügen
        /// </summary>
        /// <param name="server"></param>
        public void AddServer(Server server)
        {
            server.Calculate(Umgebung);
            Servers.Add(server);
            Console.Write($" => Wahrscheinlichkeit = {server.Wahrscheinlichkeit:P}\n");
        }

        /// <summary>
        /// Dateiname und Pfad erzeugen
        /// </summary>
        /// <returns></returns>
        protected FileInfo GetFilePath()
        {
            var tempFile = new FileInfo(Path.GetTempFileName());
            var fileName = $"1_Warteschlange_1_Server_{tempFile.Name.Replace(".tmp", ".xlsx")}";
            return new FileInfo(tempFile.FullName.Replace(tempFile.Name, fileName)); ;
        }

        /// <summary>
        /// Jede Simulation soll ein Excel erzeugen.
        /// </summary>
        internal abstract void CreateExcel();
    }
}
