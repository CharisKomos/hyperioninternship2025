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

            // Validation
            // Hint: Use .Where(condition to find the JobStatus.InProcess)
            var inProcessBillets = _allScheduledBillets
                   .Where(sb => sb.JobStatus == JobStatus.InProcess)
                   .ToList();
            bool isValid = inProcessBillets.Any(sb => sb.HeatCode == billet.HeatCode);

            if (isValid)
            {
                _billet!.Status = TrkStatus.Consumed;
                _billet.ChargedTime = DateTime.Now;
                _billet.WeightMeasured = 1250 + new Random().NextDouble() * 100;
            }
            else
            {
                _billet!.Status = TrkStatus.Rejected;               
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
            if (_billet?.Status == TrkStatus.Rejected)
            {              
                Release();  // This clears CHG position
                return;
            }
            ConstructMesssage();
            base.Process();
        }
    }
}
