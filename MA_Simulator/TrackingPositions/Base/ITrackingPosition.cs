using MA_Simulator.Models;

namespace MA_Simulator.TrackingPositions.Base
{
    public interface ITrackingPosition
    {
        public void Accept(TrackingBillet position);
        public void Release();
        public TrackingBillet? BilletInPosition();
        public bool CanAccept();
        public bool CanRelease();
        public Task SendMessageAsync();
        public void ConstructMesssage();
        public void Process();
    }
}
