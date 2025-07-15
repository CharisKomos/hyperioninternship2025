namespace MA_Simulator.Models
{
    public class ChemicalComposite
    {
        public string Name { get; set; } = "Composite Name";
        public double PercInComposition { get; set; }

        public ChemicalComposite(string name, double perc)
        {
            Name = name;
            PercInComposition = perc;
        }
    }
}
