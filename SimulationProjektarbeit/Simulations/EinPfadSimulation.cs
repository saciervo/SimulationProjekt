using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;
using SimulationProjektarbeit.Core;

namespace SimulationProjektarbeit.Simulations
{
    /// <summary>
    /// Erstellt eine Simulation mit nur einem Durchlauf (Pfad). Dient zum besseren Verständnis der Logik und des Ablaufs
    /// </summary>
    internal class EinPfadSimulation : SimulationBase
    {
        public EinPfadSimulation(Umgebung umgebung) : base(umgebung) { }

        public EinPfadSimulation(Umgebung umgebung, Warteschlange warteschlange, List<Server> servers) : base(umgebung)
        {
            Warteschlange = warteschlange;
            Servers = servers;
        }

        internal override void CreateExcel()
        {
            // Benutze die EPPlus library um ein Excel zu erstellen
            using (var excelPackage = new ExcelPackage())
            {
                var ws = excelPackage.Workbook.Worksheets.Add("Simulation");

                // Überschriften
                ws.Cells[1, 1].Value = "Zeitpunkt";
                ws.Cells[1, 2].Value = "Neu in Warteschlange";
                for (int s = 0; s < Servers.Count; s++)
                {
                    ws.Cells[1, s + 3].Value = $"Neu auf {s + 1}. Server";
                }
                ws.Cells[1, Servers.Count + 3].Value = "Anzahl in Warteschlange";

                var steps = Umgebung.AnzahlZeitschritte * Umgebung.Zeitschritt;
                for (int i = 0; i < steps; i++)
                {
                    var row = i + 2;
                    ws.Cells[row, 1].Value = (0 + Umgebung.Zeitschritt) * (i + 1);
                    ws.Cells[row, 2].Value = Warteschlange.Step();
                    for (int s = 0; s < Servers.Count; s++)
                    {
                        ws.Cells[row, s + 3].Value = Servers[s].Step(Warteschlange);
                    }
                    ws.Cells[row, Servers.Count + 3].Value = Warteschlange.AnzahlVideos;
                }

                // Die Spaltengrössen auf den Inhalt anpassen
                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                for (int s = 0; s < Servers.Count; s++)
                {
                    ws.Column(s + 3).AutoFit();
                }
                ws.Column(Servers.Count + 3).AutoFit();

                // Dateinamen generieren und Excel an diesem Ort erstellen
                var excelFile = GetFilePath();
                excelPackage.SaveAs(excelFile);

                // Nach dem Erstellen des Excel files soll dieses gleich im Excel angezeigt werden
                Process.Start(excelFile.FullName);
            }
        }
    }
}
