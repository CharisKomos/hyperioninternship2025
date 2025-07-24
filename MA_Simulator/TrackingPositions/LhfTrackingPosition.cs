using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class LhfTrackingPosition : TrackingPositionBase<LhfTrackingMessage>
    {
        public LhfTrackingPosition()
        {
            PositionName = $"LHF";
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            _billet.Status = BilletStatus.LHF;
        }

        public override void Release()
        {
            if (_billet != null)
                _billet.PrdType = ProductType.Coil;

            base.Release();
        }
    }
}
