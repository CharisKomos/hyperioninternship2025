using MA_Simulator.Models;

namespace MA_Simulator.Schedulers
{
    public class ProductionSchedulerBase
    {
        public List<ScheduledBillet> ScheduledBillets { get; set; } = new List<ScheduledBillet>();
        public List<Job> ScheduledJobs { get; set; } = new List<Job>();

        public ProductionSchedulerBase()
        {
            SeedScheduler();
        }

        private void SeedScheduler()
        {
        }
    }
}
