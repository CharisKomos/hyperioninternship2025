using MA_Simulator.Enums;
using MA_Simulator.Models;
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
        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            //  RHF ENTRY EVENT
            billet.Status = BilletStatus.EnteringRHF;

        }
        public override void Release()
        {
            if (_billet != null)
            {
                _billet.Status = BilletStatus.ExitedRHF;
                _billet.Temperature = 900 + new Random().NextDouble() * 100;

                Console.WriteLine($"[ RHF EXIT] Billet {_billet.TrkId} exited RHF with Temp = {_billet.Temperature:F1}°C");
            }

            base.Release();
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

        private bool _entryLogged = false;
        private bool _exitLogged = false;

        public override void Process()
        {

        ConstructMesssage();
            base.Process();
        }

    }
}
