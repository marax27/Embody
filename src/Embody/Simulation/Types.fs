namespace Embody.Simulation

open Embody.Domain
open Embody.Integration

/// Contains data necessary to run a simulation, such as initial conditions and integration settings.
type SimulationPreset<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm> = {
    Settings: IntegratorSettings<'l, 't, 'm>
    System: CelestialSystem<'l, 't, 'm>
    Integrator: Integrator<'l, 't, 'm>
}

/// Contains results of a simulation (integration steps). Also stores the simulation's preset.
type SimulationResult<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm> = {
    Preset: SimulationPreset<'l, 't, 'm>
    Steps: IntegratorStep<'l, 't> array
}

/// Simulation metric is a function that assigns a value to each simulation step.
type NumericalMetric<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm, [<Measure>] 'metric> =
    SimulationPreset<'l, 't, 'm> -> IntegratorStep<'l, 't>
        -> float<'metric>
