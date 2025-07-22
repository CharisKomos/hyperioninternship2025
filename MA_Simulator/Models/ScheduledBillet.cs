using MA_Simulator.Enums;

namespace MA_Simulator.Models
{
    public class ScheduledBillet
    {
        public int TrkId { get; set; } // This is the id of L1
        public string Name { get; set; } = String.Empty;
        public string HeatCode { get; set; } = String.Empty;
        public JobStatus JobStatus { get; set; } = JobStatus.Scheduled;

        public ScheduledBillet(string heatCode, JobStatus jobStatus)
        {
            HeatCode = heatCode;
            JobStatus = jobStatus;
        }
    }
}
