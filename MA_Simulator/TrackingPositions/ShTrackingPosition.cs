using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class ShTrackingPosition : TrackingPositionBase<ShTrackingMessage>
    {
        public double _croppedHeadLength { get; set; } = 0.0; // Units: m
        public double _croppedTailLength { get; set; } = 0.0; // Units: m

        public ShTrackingPosition(int positionNo, int port, int internalMessageId) : base(port, internalMessageId)
        {
            PositionName = $"SH{positionNo}";
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);
        }

        public override void Release()
        {
            // TODO: Handle the billet exiting event
            
            // TODO: Make head and tail cuts on the billet. Change the value of the TotalCroppedTail and TotalCroppedHead from the billet accordingly.
                // Hint: The cropped length can be a percentage of the total length of the billet
            // TODO: Update the length of the billet after the cropped cuts

            base.Release();
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
