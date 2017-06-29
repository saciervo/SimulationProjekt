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
    internal class MultiPfadSimulation : SimulationBase
    {
        public int AnzahlPfade { get; set; }

        public MultiPfadSimulation(Umgebung umgebung) : base(umgebung) { }

        public MultiPfadSimulation(Umgebung umgebung, int anzahlPfade) : this(umgebung)
        {
            AnzahlPfade = anzahlPfade;
        }

        internal override void CreateExcel()
        {
            // Für jeden Pfad muss eine eigene Simulation erstellt werden
            var simulations = new List<SimulationOhneUmgebung>();
            for (int i = 0; i < AnzahlPfade; i++)
            {
                simulations.Add(new SimulationOhneUmgebung(Umgebung, Warteschlange, Servers));
            }

            // Benutze die EPPlus library um ein Excel zu erstellen
            using (var excelPackage = new ExcelPackage())
            {
                var ws = excelPackage.Workbook.Worksheets.Add("Simulation");

                // Überschriften
                ws.Cells[1, 1].Value = "Zeit/Pfad";
                for (int p = 0; p < simulations.Count; p++)
                {
                    ws.Cells[1, p + 2].Value = p + 1;
                }

var steps = Umgebung.AnzahlZeitschritte * Umgebung.Zeitschritt;
for (int i = 0; i < steps; i++)
{
    var row = i + 2; // Neue Zeile
    ws.Cells[row, 1].Value = (0 + Umgebung.Zeitschritt) * (i + 1); // Spalte: Aktueller Zeitpunkt
                    
    for (int p = 0; p < simulations.Count; p++)
    {
        var simulation = simulations[p];
        ws.Cells[row, p + 2].Value = simulation.SimulateOneStep(); // Spalte: Aktueller Pfad
    }
}

                // Dateinamen generieren und Excel an diesem Ort erstellen
                var excelFile = GetFilePath();
                excelPackage.SaveAs(excelFile);

                // Nach dem Erstellen des Excel files soll dieses gleich im Excel angezeigt werden
                Process.Start(excelFile.FullName);
            }
        }
    }
}
