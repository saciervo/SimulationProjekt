using SimulationProjektarbeit.Utility;

namespace SimulationProjektarbeit.Core
{
    internal class Warteschlange : SimulationsSystem
    {
        public int Step()
        {
            // Falls die Zufallszahl höher als die Wahrscheinlichkeit ist gibt es keine Ankunft
            if (ThreadSafeRandom.Rand() > Wahrscheinlichkeit)
                return 0;

            // Ankunft eines Videos in der Warteschlange
            AnzahlVideos++;
            return 1;
        }
    }
}