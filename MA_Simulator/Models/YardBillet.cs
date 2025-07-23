using MA_Simulator.Enums;

namespace MA_Simulator.Models
{
    public class YardBillet
    {
        public int L1Id { get; set; }
        public string HeatCode { get; set; } = String.Empty;
        public double Length { get; set; }
        public double Weight { get; set; }
        public double Dimension { get; set; }
        public BilletShape Shape { get; set; }
        public double Temperature { get; set; }
        public List<ChemicalComposite> ChemicalComposition { get; set; } = new List<ChemicalComposite>();

        public YardBillet(int l1Id, string heatCode, double length, double weight, double dim, BilletShape shape, List<ChemicalComposite> chemComp, double temperature)
        {
            L1Id = l1Id;
            HeatCode = heatCode;
            Length = length;
            Weight = weight;
            Dimension = dim;
            Shape = shape;
            Temperature = temperature;
            ChemicalComposition = chemComp;
        }
    }
}
