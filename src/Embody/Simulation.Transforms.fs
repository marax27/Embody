namespace Embody.Simulation

module Transforms =
    
    open Base

    /// Apply a metric to the results of a simulation.
     let applyMetric
        (metric: NumericalMetric<'l, 't, 'm, 'metric>)
        (simulationResult: SimulationResult<'l, 't, 'm>)
        =
        simulationResult.Steps
        |> Array.map (metric simulationResult.Preset)
