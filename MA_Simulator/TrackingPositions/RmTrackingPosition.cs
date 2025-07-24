using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class RmTrackingPosition : TrackingPositionBase<RmTrackingMessage>
    {
        public double _decreaseFactorDimension { get; set; } = 1.0;

        public RmTrackingPosition(int positionNo, int port, int internalMessageId) : base(port, internalMessageId)
        {
            PositionName = $"RM{positionNo}";
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            billet.Status = BilletStatus.EnteringRM;
        }

        public override void Release()
        {
            if (_billet is TrackingBillet rollingBillet)
            {
                rollingBillet.Status = BilletStatus.ExitedRM;
                double originalDiameter = rollingBillet.Dimension;
                double originalLength = rollingBillet.Length;

                // Apply percentile reduction to diameter
                double newDiameter = originalDiameter * (1 - _decreaseFactorDimension);
                rollingBillet.Dimension = newDiameter;

                // Maintain constant volume for a cylinder: V = π * r² * h
                // So: newLength = originalVolume / (π * (newDiameter/2)²)
                double originalVolume = Math.PI * Math.Pow(originalDiameter / 2, 2) * originalLength;
                double newLength = originalVolume / (Math.PI * Math.Pow(newDiameter / 2, 2));
                rollingBillet.Length = newLength;

                base.Release();
            }
        }

        public override void ConstructMesssage()
        {
            if(_billet != null)
            {
                _positionMessage = new RmTrackingMessage
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
