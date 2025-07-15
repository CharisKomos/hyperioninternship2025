namespace MA_Simulator.Enums
{
    public enum JobStatus
    {
        Scheduled = 0, // Default-First status
        InProcess = 1, // When the first billet of the job has entered the charging station
        Completed = 2, // When the last billet of the job has exited the finishing station
    }
}