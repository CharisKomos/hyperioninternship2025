using MA_MessageLib;
using MA_Simulator.Enums; // for BilletShape
using System.Collections.Generic;

namespace MA_Simulator.Models
{
    public class TrackingBillet : TrackingInfoBase
    {
        public string HeatCode { get; set; } = string.Empty;

        public double Length { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double WeightMeasured { get; set; } = 0.0;

        // New fields from YardBillet
        public BilletShape Shape { get; set; }
        public List<ChemicalComposite> ChemicalComposition { get; set; } = new();
        public BilletStatus Status { get; set; } = BilletStatus.InYard;
        public DateTime? ChargedTime { get; set; } = null;
        public double Temperature { get; set; }
        public double TotalCroppedHead { get; set; } = 0.0;
        public double TotalCroppedTail { get; set; } = 0.0;
    }
}
