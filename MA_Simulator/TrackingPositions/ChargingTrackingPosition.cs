using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class ChargingTrackingPosition : TrackingPositionBase<ChargingTrackingMessage>
    {
        private List<ScheduledBillet> _allScheduledBillets;

        public ChargingTrackingPosition(int port, int internalMessageId, List<ScheduledBillet> scheduledBillets) : base(port, internalMessageId)
        {
            PositionName = "CHG";
            _allScheduledBillets = scheduledBillets;
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            // Billet is charged
            _billet!.Status = BilletStatus.Charged;
            Console.WriteLine($"The billet {_billet.PlcSemiproductCode} has status {_billet.Status}");

            // Validation
            var inProcessBillets = _allScheduledBillets
                   .Where(sb => sb.JobStatus == JobStatus.InProcess)
                   .ToList();
            bool isValid = inProcessBillets.Any(sb => sb.HeatCode == billet.HeatCode);

            if (isValid)
            {
                _billet!.Status = BilletStatus.Consumed;
                _billet.ChargedTime = DateTime.Now;

                // Introduce a measuring deviation .5%
                _billet.WeightMeasured = _billet.Weight*1.005;
            }
            else
            {
                _billet!.Status = BilletStatus.Rejected;               
                return;
            }
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
            if (_billet?.Status == BilletStatus.Rejected)
            {              
                Release();  // This clears CHG position
                return;
            }

            ConstructMesssage();
            base.Process();
        }
    }
}
