using MA_Simulator.Enums;

namespace MA_Simulator.Models
{
    public class Job : EntityId
    {
        public string Customer { get; set; }
        public List<Heat> Heats { get; set; }
        public int NoHeats => Heats.Count;
        public DateTime StatusChanged { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Scheduled;

        public Job(int id, string customer, List<Heat> heatList, DateTime statusChange, JobStatus status)
        {
            Id = id;
            Customer = customer;
            Heats = heatList;
            StatusChanged = statusChange;
            Status = status;
        }
    }
}