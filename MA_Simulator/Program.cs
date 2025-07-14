using Microsoft.Extensions.Configuration;
using MA_Simulator.Configuration;
using MA_Simulator.SimulationEngines;
using MA_Simulator.Schedulers;

// Load configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Load section from configuration
var settingsSimulation = config.GetSection("Settings").Get<SimulatorSettings>();

// Initialize scheduler
var productionScheduler = new LongProductionScheduler();

// Initialize custom simulation engine
var engine = new CustomSimulationEngine(settingsSimulation, productionScheduler);

// Start simulation engine
engine.Start();

Console.WriteLine("Simulator running. Press [Enter] to exit...");
Console.ReadLine();

engine.Stop();
