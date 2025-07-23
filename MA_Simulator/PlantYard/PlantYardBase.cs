
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
                new YardBillet(123001, "HEAT001", 12567, 120, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123002, "HEAT002", 13547, 110, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123003, "HEAT003", 12027, 125, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123004, "HEAT004", 10127, 140, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123005, "HEAT005", 12357, 150, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123006, "HEAT006", 11327, 128, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123007, "HEAT007", 12007, 117, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123008, "HEAT008", 12407, 127, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123009, "HEAT009", 13056, 115, BilletShape.Round, new List<ChemicalComposite>(), 30),
                new YardBillet(123010, "HEAT010", 11547, 139, BilletShape.Round, new List<ChemicalComposite>(), 30)
            };
        }
    }
}
