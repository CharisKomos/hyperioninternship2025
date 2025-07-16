using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class ChargingTrackingPosition : TrackingPositionBase<ChargingTrackingMessage>
    {
        private List<ScheduledBillet> _scheduledBillet;

        public ChargingTrackingPosition(int port, int internalMessageId, List<ScheduledBillet> scheduledBillets) : base(port, internalMessageId)
        {
            PositionName = "CHG";
            _scheduledBillet = scheduledBillets;
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            // TODO: Add logic to decide if the incoming billet from the yard can be consumed or not
            // Hint: Use the _scheduledBilletList
        }

        public override void ConstructMesssage()
        {
            if(_billet != null)
            {
                _positionMessage = new ChargingTrackingMessage
                {
                    Name = _billet.Name,
                    L1TrackingId = _billet.L1TrackingId,
                    PlcSemiproductCode = _billet.PlcSemiproductCode,
                    SemiproductNo = _billet.SemiproductNo
                };
            }
        }

        public override void Process()
        {
            ConstructMesssage();
            base.Process();
        }
    }
}
