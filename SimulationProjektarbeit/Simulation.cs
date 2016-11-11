using OfficeOpenXml;
using SimulationProjektarbeit.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationProjektarbeit
{
    class Simulation
    {
        private Random _rand = new Random();

        public SimConfig Config { get; private set; }

        public SimQueue Queue { get; private set; }
        public List<SimServer> Servers { get; private set; }

        public Simulation(SimConfig config, SimQueue queue, List<SimServer> servers)
        {
            Config = config;

            queue.Calculate(config);
            Queue = queue;

            foreach (var server in servers)
            {
                server.Calculate(config);
            }
            Servers = servers;
        }

        internal void Create()
        {
            using (var excel = new ExcelPackage())
            {
                var ws = excel.Workbook.Worksheets.Add("Simulation");

                ws.Cells[1, 1].Value = "Zeitpunkt";
                ws.Cells[1, 2].Value = "Neu in Warteschlange";
                ws.Cells[1, 3].Value = "Neu auf Server";
                ws.Cells[1, 4].Value = "Anzahl in Warteschlange";

                var steps = 1000 / Config.Zeitschritt;
                for (int i = 0; i < steps; i++)
                {
                    var row = i + 2;
                    ws.Cells[row, 1].Value = (0 + Config.Zeitschritt) * (i + 1);
                    ws.Cells[row, 2].Value = Queue.Step();
                    ws.Cells[row, 3].Value = Servers[0].Step(Queue);
                    ws.Cells[row, 4].Value = Queue.Size;
                }

                var fileName = new FileInfo($"{Path.GetTempFileName()}.xlsx");
                excel.SaveAs(fileName);

                Process.Start(fileName.FullName);
            }
        }
    }
}
