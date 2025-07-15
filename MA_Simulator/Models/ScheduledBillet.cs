namespace MA_Simulator.Models
{
    public class ScheduledBillet
    {
        public int TrkId { get; set; } // This is the id of L1
        public string Name { get; set; } = String.Empty;
        public string HeatCode { get; set; } = String.Empty;

        public ScheduledBillet(string heatCode)
        {
            HeatCode = heatCode;
        }
    }
}
