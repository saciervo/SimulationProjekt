using SimulationProjektarbeit.Utility;

namespace SimulationProjektarbeit.Core
{
    internal class Server : SimulationsSystem
    {
public int Step(Warteschlange warteschlange)
{
// Wenn die Warteschlange keine Objekte enthält, können wir auch keine verarbeiten
if (warteschlange.AnzahlVideos == 0)
    return 0;

// Falls die Zufallszahl höher als die Wahrscheinlichkeit ist gibt es keine Ankunft
if (ThreadSafeRandom.Rand() > Wahrscheinlichkeit)
    return 0;

// Ankunft eines Videos auf dem Server aus der Warteschlage
warteschlange.AnzahlVideos--;
AnzahlVideos++;
return 1;
}
    }
}