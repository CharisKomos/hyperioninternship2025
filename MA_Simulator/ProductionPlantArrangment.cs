using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.Schedulers;
using MA_Simulator.TrackingPositions;

namespace MA_Simulator
{
    public class ProductionPlantArrangment
    {
        #region Fields/Properties
        private readonly ProductionSchedulerBase _productionScheduler;
        private readonly ChargingTrackingPosition _chgTrkPosition = new ChargingTrackingPosition((int)ServicePortEnum.ChargingServicePort, 1);
        private readonly RhfTrackingPosition _rhfTrkPosition = new RhfTrackingPosition((int)ServicePortEnum.ReheatingFurnaceServicePort, 1);
        private readonly RmTrackingPosition _rm1TrkPosition = new RmTrackingPosition(1, (int)ServicePortEnum.RmServicePort, 1);
        private readonly RmTrackingPosition _rm2TrkPosition = new RmTrackingPosition(2, (int)ServicePortEnum.RmServicePort, 2);
        #endregion

        #region Constructor
        public ProductionPlantArrangment(ProductionSchedulerBase productionScheduler)
        {
            // Production scheduler provides production plan data - What to load after
            _productionScheduler = productionScheduler;
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

            // Load scheduled billet
            if (_chgTrkPosition.CanAccept())
            {
                TrackingBillet? scheduledBillet = await GetNextBillet();
                if (scheduledBillet != null)
                {
                    _chgTrkPosition.Accept(scheduledBillet);
                }
            }
        }
        #endregion

        #region Private Methods
        private Task<TrackingBillet?> GetNextBillet()
        {
            TrackingBillet? nextBilletToLoad = null;

            // If there any scheduled billets left
            if (_productionScheduler.ScheduledBillets.Any())
            {
                // Get the first one from the stack
                ScheduledBillet? scheduledBillet = _productionScheduler.ScheduledBillets.First();
                nextBilletToLoad = CreateTrackingBilletFromScheduled(scheduledBillet);

                // Remove it from the stack
                _productionScheduler.ScheduledBillets.Remove(scheduledBillet);
            }

            return Task.FromResult(nextBilletToLoad);
        }

        private TrackingBillet? CreateTrackingBilletFromScheduled(ScheduledBillet scheduledBillet)
        {
            // TODO: Create a new TrackingBillet from ScheduledBillet
            throw new NotImplementedException();
        }

        private Task LogPositionsState()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"{_chgTrkPosition.PositionName} : [{_chgTrkPosition.BilletInPosition()?.Name}]");
            Console.WriteLine($"{_rhfTrkPosition.PositionName} : [{_rhfTrkPosition.BilletInPosition()?.Name}]");
            Console.WriteLine($"{_rm1TrkPosition.PositionName} : [{_rm1TrkPosition.BilletInPosition()?.Name}]");
            Console.WriteLine($"{_rm2TrkPosition.PositionName} : [{_rm2TrkPosition.BilletInPosition()?.Name}]");
            Console.WriteLine("-----------------");
            Console.WriteLine();

            return Task.CompletedTask;
        }
        #endregion
    }
}
