using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator.TrackingPositions
{
    public class BdlTrackingPosition : TrackingPositionBase<BdlTrackingMessage>
    {
        public List<TrackingBillet> _bundle = new List<TrackingBillet>();
        public int _maxBilletsInBundle;

        public BdlTrackingPosition(int maxBilletsInBundle)
        {
            PositionName = $"BDL";
            _maxBilletsInBundle = maxBilletsInBundle;
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            _billet!.Status = BilletStatus.BDL;
            _bundle.Add(billet);
        }

        public override void Release()
        {
            if (_billet != null && _bundle.Count() == _maxBilletsInBundle)
            {
                string bundleCode = GenerateBundleCode();
                TrackingBillet bundle = new TrackingBillet
                {
                    PlcSemiproductCode = bundleCode,
                    L1TrackingId = _billet.L1TrackingId,
                    Name = $"Bundle_{bundleCode}",
                    PrdType = ProductType.Bundle,
                    Weight = _bundle.Select(billet => billet.Weight).Sum()
                };

                Logger.Instance.Log($"A new bundle with name {bundle.Name} was discharged.");
                Logger.Instance.Log($"Weight: {bundle.Weight}");
                Logger.Instance.Log("It contains billets with heat codes:");
                foreach(TrackingBillet b  in _bundle)
                {
                    Logger.Instance.Log(b.HeatCode);
                }
            }

            base.Release();
        }

        private string GenerateBundleCode()
        {
            return $"BDL0{_bundle.Last().HeatCode.Replace("HEAT00", String.Empty)}0";
        }
    }
}
