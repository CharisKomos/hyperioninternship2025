using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class ShTrackingPosition : TrackingPositionBase<ShTrackingMessage>
    {
        public double _croppedHeadLength { get; set; } = 0.0; // Units: m
        public double _croppedTailLength { get; set; } = 0.0; // Units: m

        public ShTrackingPosition(int positionNo)
        {
            PositionName = $"SH{positionNo}";
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);
        }

        public override void Release()
        {
            if (_billet != null)
            {
                _billet.Status = BilletStatus.ExitedSH;

                double croppedHead = _croppedHeadLength;
                double croppedTail = _croppedTailLength;

                // Update billet fields

                _billet.Length = _billet.Length - (croppedHead + croppedTail);

                Console.WriteLine($"[SH] Cropped Head = {croppedHead}mm, Cropped Tail = {croppedTail}mm, New Length = {_billet.Length:F0}mm");

                base.Release();
            }
        }

        public override void ConstructMesssage()
        {
            if(_billet != null)
            {
                _positionMessage = new ShTrackingMessage
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
