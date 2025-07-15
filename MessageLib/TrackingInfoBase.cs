namespace MA_MessageLib
{
    public class TrackingInfoBase
    {
        public int L1TrackingId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string PlcSemiproductCode { get; set; } = String.Empty; // L2 Tracking Id
        public int SemiproductNo { get; set; } = 1;
    }
}
