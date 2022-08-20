﻿namespace Embody

module Metrics =

    open Integration
    open LinearAlgebra
    open Simulation.Base

    /// Calculate total energy (TE) of an n-body system.
    /// TE is the sum of kinetic energy (KE) and potential energy (PE).
    ///
    /// On its own, system's energy doesn't say much. However, difference between
    /// current TE and initial TE can serve as a metric of simulation's quality.
    /// TE should stay constant in a closed system, and any deviation could be considered an error.
    let calculateTotalEnergy
        (preset: SimulationPreset<'l, 't, 'm>)
        (step: IntegratorStep<'l, 't>)
        : float<'m*'l^2/'t^2>
        =

        let bodies = preset.System.Bodies
        let bodyCount = bodies.Length
        let bodyIndices = [0 .. bodyCount - 1]

        let G = preset.Settings.GravitationalConstant
        let μs = bodyIndices |> List.map (fun i -> G * bodies.[i].Mass)

        let inline calculateKineticEnergy i =
            0.5 * bodies.[i].Mass * (step.V.[i] |> Vector3.squaredLength)

        let inline calculatePotentialEnergy i =
            let partial = bodyIndices
                        |> List.except [ i ]
                        |> List.sumBy (fun j ->
                            let distance = (step.R.[j] - step.R.[i]) |> Vector3.length
                            bodies.[j].Mass / distance
                        )
            -μs.[i] * partial

        let totalKinetic = bodyIndices |> List.sumBy calculateKineticEnergy
        let totalPotential = bodyIndices |> List.sumBy calculatePotentialEnergy

        totalKinetic + totalPotential
