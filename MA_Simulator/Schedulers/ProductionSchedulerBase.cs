using MA_Simulator.Models;

namespace MA_Simulator.Schedulers
{
    public class ProductionSchedulerBase
    {
        public List<TrackingBillet> ScheduledBillets { get; set; } = new List<TrackingBillet>();

        public ProductionSchedulerBase()
        {
            SeedScheduler();
        }

        private void SeedScheduler()
        {
            ScheduledBillets = new List<TrackingBillet>
            {
                new TrackingBillet{ L1TrackingId = 1, Name = "Billet 1", PlcSemiproductCode = "B1000", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 2, Name = "Billet 2", PlcSemiproductCode = "B1001", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 3, Name = "Billet 3", PlcSemiproductCode = "B1002", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 4, Name = "Billet 4", PlcSemiproductCode = "B1003", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 5, Name = "Billet 5", PlcSemiproductCode = "B1004", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 6, Name = "Billet 6", PlcSemiproductCode = "B1005", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 7, Name = "Billet 7", PlcSemiproductCode = "B1006", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 8, Name = "Billet 8", PlcSemiproductCode = "B1007", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 9, Name = "Billet 9", PlcSemiproductCode = "B1008", SemiproductNo = 1 },
                new TrackingBillet{ L1TrackingId = 10, Name = "Billet 10", PlcSemiproductCode = "B1009", SemiproductNo = 1 },
            };
        }
    }
}
