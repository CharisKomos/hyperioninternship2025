using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.Schedulers;
using MA_Simulator.TrackingPositions;
using MA_Simulator.PlantYard;
using MA_Simulator.TrackingPositions.Models;

namespace MA_Simulator
{
    public class ProductionPlantArrangment
    {
        #region Fields/Properties
        private readonly ProductionSchedulerBase _productionScheduler;
        private readonly PlantYardBase _plantYard;
        private readonly ChargingTrackingPosition _chgTrkPosition;
        private readonly RhfTrackingPosition _rhfTrkPosition;
        private readonly RmTrackingPosition _rm1TrkPosition;
        private readonly ShTrackingPosition _sh1TrkPosition;
        private readonly RmTrackingPosition _rm2TrkPosition;
        private readonly RmTrackingPosition _rm3TrkPosition;
        private readonly ShTrackingPosition _sh2TrkPosition;
        private readonly RmTrackingPosition _rm4TrkPosition;
        private readonly ShTrackingPosition _sh3TrkPosition;
        private readonly LhfTrackingPosition _lhfTrkPosition;
        private readonly CoolingTrackingPosition _ccTrkPosition;
        private readonly CoolingTrackingPosition _cbTrkPosition;
        #endregion

        #region Constructor
        public ProductionPlantArrangment(ProductionSchedulerBase productionScheduler, PlantYardBase plantYard)
        {
            // Production scheduler provides production plan data - What to load after
            _productionScheduler = productionScheduler;
            _plantYard = plantYard;

            _chgTrkPosition = new ChargingTrackingPosition(_productionScheduler.ScheduledBillets);
            _rhfTrkPosition = new RhfTrackingPosition();
            _rm1TrkPosition = new RmTrackingPosition(1);
            _sh1TrkPosition = new ShTrackingPosition(1);
            _rm2TrkPosition = new RmTrackingPosition(2);
            _rm3TrkPosition = new RmTrackingPosition(3);
            _sh2TrkPosition = new ShTrackingPosition(2);
            _rm4TrkPosition = new RmTrackingPosition(4);
            _sh3TrkPosition = new ShTrackingPosition(3);
            _lhfTrkPosition = new LhfTrackingPosition(1);
            _ccTrkPosition = new CoolingTrackingPosition("Cooling Conveyor", BilletStatus.EnteredCC);

            ConfigureSetupParameters();
            Logger.Instance.Log("========================= NEW SIMULATION STARTED =========================");
        }
        #endregion

        #region Public Methods
        public async Task HandleTimeStep()
        {
            await LogPositionsState();

            // LAST POSITION NEEDS TO BE LIKE THIS
            // CC 
            if (_ccTrkPosition.CanRelease())
            {
                _ccTrkPosition.Release();
            }
            _ccTrkPosition.Process();
            // Lhf
            if (_ccTrkPosition.CanAccept() && _lhfTrkPosition.CanRelease())
            {
                _ccTrkPosition.Accept(_lhfTrkPosition.BilletInPosition()!);
                _lhfTrkPosition.Release();
            }
            _lhfTrkPosition.Process();
            // Sh3
            if (_lhfTrkPosition.CanAccept() && _sh3TrkPosition.CanRelease())
            {
                _lhfTrkPosition.Accept(_sh3TrkPosition.BilletInPosition()!);
                _sh3TrkPosition.Release();
            }
            _sh3TrkPosition.Process();
            // RM4
            if (_sh3TrkPosition.CanAccept() && _rm4TrkPosition.CanRelease())
            {
                _sh3TrkPosition.Accept(_rm4TrkPosition.BilletInPosition()!);
                _rm4TrkPosition.Release();
            }
            _rm4TrkPosition.Process();
            // Sh2
            if (_rm4TrkPosition.CanAccept() && _sh2TrkPosition.CanRelease())
            {
                _rm4TrkPosition.Accept(_sh2TrkPosition.BilletInPosition()!);
                _sh2TrkPosition.Release();
            }
            _sh2TrkPosition.Process();
            // RM3
            if (_sh2TrkPosition.CanAccept() && _rm3TrkPosition.CanRelease())
            {
                _sh2TrkPosition.Accept(_rm3TrkPosition.BilletInPosition()!);
                _rm3TrkPosition.Release();
            }
            _rm3TrkPosition.Process();

            // RM2
            if (_rm3TrkPosition.CanAccept() && _rm2TrkPosition.CanRelease())
            {
                _rm3TrkPosition.Accept(_rm2TrkPosition.BilletInPosition()!);
                _rm2TrkPosition.Release();
            }
            _rm2TrkPosition.Process();

            // SH1
            if (_rm2TrkPosition.CanAccept() && _sh1TrkPosition.CanRelease())
            {
                _rm2TrkPosition.Accept(_sh1TrkPosition.BilletInPosition()!);
                _sh1TrkPosition.Release();
            }
            _sh1TrkPosition.Process();

            // RM1
            if (_sh1TrkPosition.CanAccept() && _rm1TrkPosition.CanRelease())
            {
                _sh1TrkPosition.Accept(_rm1TrkPosition.BilletInPosition()!);
                _rm1TrkPosition.Release();
            }
            _rm1TrkPosition.Process();

            // Reheating Furnace Station
            if (_rm1TrkPosition.CanAccept() && _rhfTrkPosition.CanRelease())
            {
                _rm1TrkPosition.Accept(_rhfTrkPosition.BilletInPosition()!);
                _rhfTrkPosition.Release();
            }
            _rhfTrkPosition.Process();

            // Charging Station
            if (_rhfTrkPosition.CanAccept() && _chgTrkPosition.CanRelease())
            {
                _rhfTrkPosition.Accept(_chgTrkPosition.BilletInPosition()!);                
                _chgTrkPosition.Release();
            }
            _chgTrkPosition.Process();

            // Load yard billet
            if (_chgTrkPosition.CanAccept())
            {
                TrackingBillet? yardBillet = await GetNextBillet();
                if (yardBillet != null)
                {
                    _chgTrkPosition.Accept(yardBillet);
                }
            }
        }
        #endregion

        #region Private Methods
        private void ConfigureSetupParameters()
        {
            _rm1TrkPosition._decreaseFactorDimension = 0.05;
            _sh1TrkPosition._croppedHeadLength = 100;
            _sh1TrkPosition._croppedTailLength = 100;
            _rm2TrkPosition._decreaseFactorDimension = 0.05;
            _sh2TrkPosition._croppedHeadLength = 100;
            _sh2TrkPosition._croppedTailLength = 100;
            _rm3TrkPosition._decreaseFactorDimension = 0.05;
            _rm4TrkPosition._decreaseFactorDimension = 0.05;
            _sh3TrkPosition._croppedHeadLength = 100;
            _sh3TrkPosition._croppedTailLength = 100;


        }

        private Task<TrackingBillet?> GetNextBillet()
        {
            TrackingBillet? nextBilletToLoad = null;

            // If there any yard billets left
            if (_plantYard.AvailableBilletsInYard.Any())
            {
                // Get the first one from the stack
                YardBillet? yardBillet = _plantYard.AvailableBilletsInYard.First();
                
                if (_productionScheduler.ScheduledBillets.Any(sb => sb.HeatCode.Equals(yardBillet.HeatCode, StringComparison.InvariantCultureIgnoreCase)))
                    nextBilletToLoad = CreateTrackingBilletFromScheduled(yardBillet);

                // Remove it from the stack
                _plantYard.AvailableBilletsInYard.Remove(yardBillet);
            }

            return Task.FromResult(nextBilletToLoad);
        }

        private TrackingBillet? CreateTrackingBilletFromScheduled(YardBillet yardBillet)
        {
            // Convert the billet from the yard to tracking billet for the simulation
            if (yardBillet == null)
                return null;

            // Create new TrackingBillet object
            TrackingBillet yardTrackingBillet = new TrackingBillet();

            yardTrackingBillet.PlcSemiproductCode = $"{yardBillet.HeatCode}_{yardBillet.L1Id}";
            yardTrackingBillet.L1TrackingId = yardBillet.L1Id;
            yardTrackingBillet.HeatCode = yardBillet.HeatCode;
            yardTrackingBillet.Length = yardBillet.Length;
            yardTrackingBillet.Weight = yardBillet.Weight;
            yardTrackingBillet.Dimension = yardBillet.Dimension;
            yardTrackingBillet.Shape = yardBillet.Shape;
            yardTrackingBillet.ChemicalComposition = yardBillet.ChemicalComposition;
            yardTrackingBillet.Temperature = yardBillet.Temperature;

            return yardTrackingBillet;
        }

        private Task LogPositionsState()
        {
            Logger.Instance.Log("---------------------------------------------------");
            Logger.Instance.Log($"{_chgTrkPosition.PositionName} : [{_chgTrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_chgTrkPosition.BilletInPosition()?.Status} " +
                $"- Weight={_chgTrkPosition.BilletInPosition()?.WeightMeasured.ToString("F2") ?? "N/A"} kg");
            Logger.Instance.Log($"{_rhfTrkPosition.PositionName} : [{_rhfTrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_rhfTrkPosition.BilletInPosition()?.Status} " +
                $"- Temp={_rhfTrkPosition.BilletInPosition()?.Temperature.ToString("F1") ?? "N/A"}°C");
            Logger.Instance.Log($"{_rm1TrkPosition.PositionName} : [{_rm1TrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_rm1TrkPosition.BilletInPosition()?.Status} " +
                $"Dim={_rm1TrkPosition.BilletInPosition()?.Dimension:F2}mm, Len={_rm1TrkPosition.BilletInPosition()?.Length:F0}mm");
            Logger.Instance.Log($"{_sh1TrkPosition.PositionName} : [{_sh1TrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_sh1TrkPosition.BilletInPosition()?.Status} " +
                $"Len={_sh1TrkPosition.BilletInPosition()?.Length:F0}mm");
            Logger.Instance.Log($"{_rm2TrkPosition.PositionName} : [{_rm2TrkPosition.BilletInPosition()?.HeatCode}] "+
                $"- Status: {_rm2TrkPosition.BilletInPosition()?.Status} " +
                $"Dim={_rm2TrkPosition.BilletInPosition()?.Dimension:F2}mm, Len={_rm2TrkPosition.BilletInPosition()?.Length:F0}mm");
            Logger.Instance.Log($"{_rm3TrkPosition.PositionName} : [{_rm3TrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_rm3TrkPosition.BilletInPosition()?.Status} " +
                $"Dim={_rm3TrkPosition.BilletInPosition()?.Dimension:F2}mm, Len={_rm3TrkPosition.BilletInPosition()?.Length:F0}mm");
            Logger.Instance.Log($"{_sh2TrkPosition.PositionName} : [{_sh2TrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_sh2TrkPosition.BilletInPosition()?.Status} " +
                $"Len={_sh2TrkPosition.BilletInPosition()?.Length:F0}mm");
            Logger.Instance.Log($"{_rm4TrkPosition.PositionName} : [{_rm4TrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_rm4TrkPosition.BilletInPosition()?.Status} " +
                 $"Dim={_rm4TrkPosition.BilletInPosition()?.Dimension:F2}mm, Len={_rm4TrkPosition.BilletInPosition()?.Length:F0}mm");
            Logger.Instance.Log($"{_sh3TrkPosition.PositionName} : [{_sh3TrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_sh3TrkPosition.BilletInPosition()?.Status} " +
                $"Len={_sh3TrkPosition.BilletInPosition()?.Length:F0}mm");
            Logger.Instance.Log($"{_lhfTrkPosition.PositionName} : [{_lhfTrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_lhfTrkPosition.BilletInPosition()?.Status} ");
            Logger.Instance.Log($"{_ccTrkPosition.PositionName} : [{_ccTrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_ccTrkPosition.BilletInPosition()?.Status} ");
            Logger.Instance.Log("---------------------------------------------------");

            return Task.CompletedTask;
        }
        #endregion
    }
}
