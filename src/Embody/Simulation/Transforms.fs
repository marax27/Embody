namespace Embody.Simulation


/// A module that contains transformations: functions that can be applied to a simulation result.
module Transforms =

    open Embody.Domain

    module private Helpers =

        let inline findBodyIndex
            (bodyName: string)
            (bodies: CelestialBody<'l, 't> seq)
            : int
            =
            bodies |> Seq.findIndex (fun body -> body.Name = bodyName)

    open Helpers

    /// Apply a metric to the results of a simulation.
    let inline applyMetric
        (metric: NumericalMetric<'l, 't, 'metric>)
        (simulationResult: SimulationResult<'l, 't>)
        =
        simulationResult.Steps
        |> Array.map (metric simulationResult.Preset)


    /// Extract all time steps. It might be helpful when gathering data for a graph.
    let timeSteps
        (simulationResult: SimulationResult<'l, 't>)
        =
        simulationResult.Steps
        |> Array.map (fun step -> step.T)


    /// Apply a predicate to position of a given body, for every recorded step of a simulation.
    let forPositionOf
        (bodyName: string)
        (predicate)
        (simulationResult: SimulationResult<'l, 't>)
        =
        let bodies = simulationResult.Preset.System.Bodies
        let bodyIndex = bodies |> findBodyIndex bodyName

        simulationResult.Steps
        |> Array.map (fun step -> predicate step.R.[bodyIndex])


    /// Apply a predicate to positions of a pair of bodies, for every recorded step of a simulation.
    let for2PositionsOf
        (firstBodyName: string)
        (secondBodyName: string)
        (predicate)
        (simulationResult: SimulationResult<'l, 't>)
        =
        let bodies = simulationResult.Preset.System.Bodies
        let firstBodyIndex = bodies |> findBodyIndex firstBodyName
        let secondBodyIndex = bodies |> findBodyIndex secondBodyName

        simulationResult.Steps
        |> Array.map (fun step -> predicate step.R.[firstBodyIndex] step.R.[secondBodyIndex])


    /// Apply a predicate to position and velocity of a body, **relative to** central body.
    let forPositionAndVelocityRelativeTo
        (orbitingBodyName: string)
        (centralBodyName: string)
        (predicate)
        (simulationResult: SimulationResult<'l, 't>)
        =
        let bodies = simulationResult.Preset.System.Bodies
        let orbitingBodyIndex = bodies |> findBodyIndex orbitingBodyName
        let centralBodyIndex = bodies |> findBodyIndex centralBodyName

        simulationResult.Steps
        |> Array.map (fun step ->
                let R = step.R.[orbitingBodyIndex] - step.R.[centralBodyIndex]
                let V = step.V.[orbitingBodyIndex] - step.V.[centralBodyIndex]
                predicate R V
            )