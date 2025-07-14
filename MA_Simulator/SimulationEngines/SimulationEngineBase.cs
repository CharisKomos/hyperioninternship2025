using System.Timers;
using MA_Simulator.Configuration;

namespace MA_Simulator.SimulationEngines
{
    public class SimulationEngineBase
    {
        private readonly System.Timers.Timer? _timer;
        private readonly object _lock = new();
        private bool _isRunning;

        protected SimulationEngineBase(SimulatorSettings? settings)
        {
            if (settings != null)
            {
                _timer = new System.Timers.Timer(settings.Interval);
                _timer.Elapsed += OnStepInternal;
            }
        }

        public void Start()
        {
            _timer?.Start();
            Console.WriteLine("Simulation engine started.");
        }

        public void Stop()
        {
            _timer?.Stop();
            Console.WriteLine("Simulation engine stopped.");
        }

        private async void OnStepInternal(object? sender, ElapsedEventArgs e)
        {
            if (_isRunning) return;

            lock (_lock)
            {
                if (_isRunning) return;
                _isRunning = true;
            }

            try
            {
                await Step();
            }
            finally
            {
                _isRunning = false;
            }
        }

        protected virtual Task Step()
        {
            return Task.CompletedTask;
        }
    }
}
