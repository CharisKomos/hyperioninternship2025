using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class CoolingTrackingPosition : TrackingPositionBase<CoolingTrackingMessage>
    {
        public double _temperatureDropPerc { get; set; } = 0.0;
        public BilletStatus _billetEnteringStatus;

        public CoolingTrackingPosition(string positionName, BilletStatus billetStatus)
        {
            PositionName = $"{positionName}";
            _billetEnteringStatus = billetStatus;
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            _billet!.Status = BilletStatus.EnteredCC;
            Console.WriteLine($"The billet {_billet.PlcSemiproductCode} has status {Enum.GetName(_billet.Status)} at {PositionName}");
        }

        public override void Release()
        {
            if (_billet != null)
                _billet.Temperature *= _temperatureDropPerc;

            base.Release();
        }
    }
}
