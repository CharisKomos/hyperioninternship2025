using MA_Simulator.Configuration;
using MA_Simulator.PlantYard;
using MA_Simulator.Schedulers;

namespace MA_Simulator.SimulationEngines
{
    public class CustomSimulationEngine : SimulationEngineBase
    {
        private ProductionPlantArrangment _productionArrangmentObj;

        public CustomSimulationEngine(SimulatorSettings? settings, ProductionSchedulerBase _productionScheduler)
            : base(settings)
        {
            _productionArrangmentObj = new ProductionPlantArrangment(_productionScheduler, new PlantYardBase());
        }

        protected override async Task Step()
        {
            await _productionArrangmentObj.HandleTimeStep();
        }
    }
}
