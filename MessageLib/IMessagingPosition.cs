namespace MA_MessageLib
{
    public interface IMessagingPosition<TPositionMessage>
    {
        TPositionMessage PositionMessage { get; }
    }
}
