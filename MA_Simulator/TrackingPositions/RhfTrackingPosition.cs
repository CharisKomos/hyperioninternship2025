using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class RhfTrackingPosition : TrackingPositionBase<RhfTrackingMessage>
    {
        public RhfTrackingPosition()
        {
            PositionName = "RHF";
        }
        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            //  RHF ENTRY EVENT
            if (_billet != null)
                _billet.Status = BilletStatus.EnteringRHF;

        }
        public override void Release()
        {
            if (_billet != null)
            {
                _billet.Status = BilletStatus.ExitedRHF;
                _billet.Temperature = 900 + new Random().NextDouble() * 100;

                Console.WriteLine($"[RHF EXIT] Billet {_billet.PlcSemiproductCode} exited RHF with Temp = {_billet.Temperature:F1}°C");
            }

            base.Release();
        }
    }
}
