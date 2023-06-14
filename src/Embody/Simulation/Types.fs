namespace Embody.Simulation

open Embody.Domain
open Embody.Integration

/// Contains data necessary to run a simulation, such as initial conditions and integration settings.
type SimulationPreset<[<Measure>] 'l, [<Measure>] 't> = {
    Settings: IntegratorSettings<'l, 't>
    System: CelestialSystem<'l, 't>
    Integrator: Integrator<'l, 't>
}

/// Contains results of a simulation (integration steps). Also stores the simulation's preset.
type SimulationResult<[<Measure>] 'l, [<Measure>] 't> = {
    Preset: SimulationPreset<'l, 't>
    Steps: IntegratorStep<'l, 't> array
}

/// Simulation metric is a function that assigns a value to each simulation step.
type NumericalMetric<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'metric> =
    SimulationPreset<'l, 't> -> IntegratorStep<'l, 't>
        -> float<'metric>
