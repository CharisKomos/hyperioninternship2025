using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class RhfTrackingPosition : TrackingPositionBase<RhfTrackingMessage>
    {

        public RhfTrackingPosition(int port, int internalMessageId) : base(port, internalMessageId)
        {
            PositionName = "RHF";
        }

        public override void ConstructMesssage()
        {
            if (_billet != null)
            {
                _positionMessage = new RhfTrackingMessage
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
