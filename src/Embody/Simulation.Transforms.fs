namespace Embody.Simulation

open Embody.Domain

module Transforms =
    
    open Base

    module private Helpers =

        let inline findBodyIndex
            (bodyName: string)
            (bodies: CelestialBody<'l, 't, 'm> seq)
            : int
            =
            bodies |> Seq.findIndex (fun body -> body.Name = bodyName)

    open Helpers


    /// Apply a metric to the results of a simulation.
    let inline applyMetric
        (metric: NumericalMetric<'l, 't, 'm, 'metric>)
        (simulationResult: SimulationResult<'l, 't, 'm>)
        =
        simulationResult.Steps
        |> Array.map (metric simulationResult.Preset)


    let timeSteps
        (simulationResult: SimulationResult<'l, 't, 'm>)
        =
        simulationResult.Steps
        |> Array.map (fun step -> step.T)


    let forPositionOf
        (bodyName: string)
        (predicate)
        (simulationResult: SimulationResult<'l, 't, 'm>)
        =
        let bodies = simulationResult.Preset.System.Bodies
        let bodyIndex = bodies |> findBodyIndex bodyName

        simulationResult.Steps
        |> Array.map (fun step -> predicate step.R.[bodyIndex])


    let for2PositionsOf
        (firstBodyName: string)
        (secondBodyName: string)
        (predicate)
        (simulationResult: SimulationResult<'l, 't, 'm>)
        =
        let bodies = simulationResult.Preset.System.Bodies
        let firstBodyIndex = bodies |> findBodyIndex firstBodyName
        let secondBodyIndex = bodies |> findBodyIndex secondBodyName

        simulationResult.Steps
        |> Array.map (fun step -> predicate step.R.[firstBodyIndex] step.R.[secondBodyIndex])
