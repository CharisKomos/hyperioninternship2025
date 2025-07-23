
using MA_Simulator.Enums;
using MA_Simulator.Models;

namespace MA_Simulator.PlantYard
{
    public class PlantYardBase
    {
        public List<YardBillet> AvailableBilletsInYard = new List<YardBillet>();

        public PlantYardBase()
        {
            SeedPlantYardWithBillets();
        }

        private void SeedPlantYardWithBillets()
        {
            AvailableBilletsInYard = new List<YardBillet>
            {
                new YardBillet(123001, "HEAT001", 12567, 1250 + new Random().NextDouble() * 100, 120, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123002, "HEAT002", 13547, 1250 + new Random().NextDouble() * 100, 110, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123003, "HEAT003", 12027, 1250 + new Random().NextDouble() * 100, 125, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123004, "HEAT004", 10127, 1250 + new Random().NextDouble() * 100, 140, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123005, "HEAT005", 12357, 1250 + new Random().NextDouble() * 100, 150, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123006, "HEAT006", 11327, 1250 + new Random().NextDouble() * 100, 128, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123007, "HEAT007", 12007, 1250 + new Random().NextDouble() * 100, 117, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123008, "HEAT008", 12407, 1250 + new Random().NextDouble() * 100, 127, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123009, "HEAT009", 13056, 1250 + new Random().NextDouble() * 100, 115, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123010, "HEAT010", 11547, 1250 + new Random().NextDouble() * 100, 139, BilletShape.Round, new List<ChemicalComposite>(), 30)
            };
        }
    }
}
