using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.Schedulers;
using MA_Simulator.TrackingPositions;
using MA_Simulator.PlantYard;

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
        private readonly RmTrackingPosition _rm2TrkPosition;
        #endregion

        #region Constructor
        public ProductionPlantArrangment(ProductionSchedulerBase productionScheduler, PlantYardBase plantYard)
        {
            // Production scheduler provides production plan data - What to load after
            _productionScheduler = productionScheduler;
            _plantYard = plantYard;

            _chgTrkPosition = new ChargingTrackingPosition((int)ServicePortEnum.ChargingServicePort, 1, _productionScheduler.ScheduledBillets);
            _rhfTrkPosition = new RhfTrackingPosition((int)ServicePortEnum.ReheatingFurnaceServicePort, 1);
            _rm1TrkPosition = new RmTrackingPosition(1, (int)ServicePortEnum.RmServicePort, 1);
            _rm2TrkPosition = new RmTrackingPosition(2, (int)ServicePortEnum.RmServicePort, 2);
        }
        #endregion

        #region Public Methods
        public async Task HandleTimeStep()
        {
            await LogPositionsState();

            // LAST POSITION NEEDS TO BE LIKE THIS
            // RM2
            if (_rm2TrkPosition.CanRelease())
            {
                _rm2TrkPosition.Release();
            }
            _rm2TrkPosition.Process();

            // RM1
            if (_rm2TrkPosition.CanAccept() && _rm1TrkPosition.CanRelease())
            {
                _rm1TrkPosition._billet!.Length *= 1.2;

                _rm2TrkPosition.Accept(_rm1TrkPosition.BilletInPosition()!);
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
        private Task<TrackingBillet?> GetNextBillet()
        {
            TrackingBillet? nextBilletToLoad = null;

            // If there any yard billets left
            if (_plantYard.AvailableBilletsInYard.Any())
            {
                // Get the first one from the stack
                YardBillet? yardBillet = _plantYard.AvailableBilletsInYard.First();
                nextBilletToLoad = CreateTrackingBilletFromScheduled(yardBillet);

                // Remove it from the stack
                _plantYard.AvailableBilletsInYard.Remove(yardBillet);
            }

            return Task.FromResult(nextBilletToLoad);
        }

        private TrackingBillet? CreateTrackingBilletFromScheduled(YardBillet yardBillet)
        {
            // Convert the billet from the yard to tracking billet for the simulation
            // TODO: Fill the yardTrackingBillet with properties from the yardBillet
            if (yardBillet == null)
                return null;

            // Create new TrackingBillet object
            TrackingBillet yardTrackingBillet = new TrackingBillet();

            //  Explicit mapping of each property
            yardTrackingBillet.TrkId = yardBillet.L1Id;
            yardTrackingBillet.HeatCode = yardBillet.HeatCode;
            yardTrackingBillet.Length = yardBillet.Length;
            yardTrackingBillet.Height = yardBillet.Dimension;
            yardTrackingBillet.Shape = yardBillet.Shape;
            yardTrackingBillet.ChemicalComposition = yardBillet.ChemicalComposition;

            return yardTrackingBillet;
        }

        private Task LogPositionsState()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"{_chgTrkPosition.PositionName} : [{_chgTrkPosition.BilletInPosition()?.HeatCode}] " +
                $"- Status: {_chgTrkPosition.BilletInPosition()?.Status} " +
                $"- ChargedAt: {_chgTrkPosition.BilletInPosition()?.ChargedTime?.ToString("HH:mm:ss") ?? "N/A"}" +
                $"- Weight={_chgTrkPosition.BilletInPosition()?.WeightMeasured.ToString("F2") ?? "N/A"} kg");  
            Console.WriteLine($"{_rhfTrkPosition.PositionName} : [{_rhfTrkPosition.BilletInPosition()?.HeatCode}]");
            Console.WriteLine($"{_rm1TrkPosition.PositionName} : [{_rm1TrkPosition.BilletInPosition()?.HeatCode}]");
            Console.WriteLine($"{_rm2TrkPosition.PositionName} : [{_rm2TrkPosition.BilletInPosition()?.HeatCode}]");
            Console.WriteLine("-----------------");
            Console.WriteLine();

            return Task.CompletedTask;
        }
        #endregion
    }
}
