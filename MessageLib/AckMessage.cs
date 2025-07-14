namespace MA_MessageLib
{
    public class AckMessage
    {
        public int Id { get; set; }
        public string Description => $"Acknowledgement message from [{ServiceName}].";
        public string ServiceName { get; set; } = String.Empty;
    }
}
