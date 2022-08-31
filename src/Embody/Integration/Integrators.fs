namespace Embody.Integration

/// Contains implementations of the integrators (aka numerical integration methods).
module Integrators =

    open Embody.Domain
    open Embody.LinearAlgebra

    module private IntegratorHelpers =
        
        /// Given state and its derivatives, compute updated state using the Euler method.
        /// This function is commonly used by a majority of integrators.
        let inline advanceState<[<Measure>] 'state, [<Measure>] 't>
            (X: NumericalVector3<'state> array)
            (X': NumericalVector3<'state/'t> array)
            (Δt: float<'t>)
            : NumericalVector3<'state> array
            =
            Array.zip X X'
            |> Array.map (fun (x, x') -> x + x' * Δt)


        /// Calculate bodies' gravitational parameters.
        let inline calculateGravitationalParameters
            (system: CelestialSystem<'l, 't, 'm>)
            (settings: IntegratorSettings<'l, 't, 'm>)
            : float<'l^3/'t^2> array
            =
            let G = settings.GravitationalConstant
            system.Bodies |> Array.map (fun body -> G * body.Mass)


        /// Function that serves as a template for common integrators. Reduces boilerplate.
        let inline baseIntegrate<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm>
            (system: CelestialSystem<'l, 't, 'm>)
            (settings: IntegratorSettings<'l, 't, 'm>)
            (calculateNextStep: int -> IntegratorStep<'l, 't> -> IntegratorStep<'l, 't>)
            : IntegratorStep<'l, 't> array
            =
            let stepCount = int ((settings.TEnd - settings.TStart) / settings.DeltaT)
        
            let initialStep = {
                T = settings.TStart
                R = system.Bodies |> Array.map (fun body -> body.R)
                V = system.Bodies |> Array.map (fun body -> body.V)
            }

            seq {
                yield initialStep
                yield! (0, initialStep)
                |> Seq.unfold (fun (index, step) ->
                    if index >= stepCount then None
                    else
                        let newStep = calculateNextStep index step
                        Some (newStep, (index + 1, newStep))
                )
            } |> Seq.toArray


        let inline meanOf (values: NumericalVector3<'u> array) (otherValues: NumericalVector3<'u> array) =
            Array.zip values otherValues
            |> Array.map (fun (a, b) -> 0.5 * (a + b))


    open IntegratorHelpers


    /// Forward Euler integration method (commonly known as the Euler method).
    let integrateForwardEuler<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm>
        (system: CelestialSystem<'l, 't, 'm>)
        (settings: IntegratorSettings<'l, 't, 'm>)
        : IntegratorStep<'l, 't> array
        =
        let Δt = settings.DeltaT
        let μs = calculateGravitationalParameters system settings

        let inline calculateNextStep (index: int) (step: IntegratorStep<'l, 't>) =
            let A = settings.Accelerator step.R μs
            {
                T = settings.TStart + Δt * (float index + 1.0)
                R = advanceState step.R step.V Δt
                V = advanceState step.V A Δt
            }

        calculateNextStep |> baseIntegrate system settings


    /// Midpoint integration method.
    let integrateMidpoint<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm>
        (system: CelestialSystem<'l, 't, 'm>)
        (settings: IntegratorSettings<'l, 't, 'm>)
        : IntegratorStep<'l, 't> array
        =
        let Δt = settings.DeltaT
        let Δt1_2 = 0.5 * Δt
        let μs = calculateGravitationalParameters system settings

        let inline calculateNextStep (index: int) (step: IntegratorStep<'l, 't>) =
            let A = settings.Accelerator step.R μs
            let R_mid = advanceState step.R step.V Δt1_2
            let V_mid = advanceState step.V A Δt1_2
            let A_mid = settings.Accelerator R_mid μs
            {
                T = settings.TStart + Δt * (float index + 1.0)
                R = advanceState step.R V_mid Δt
                V = advanceState step.V A_mid Δt
            }

        calculateNextStep |> baseIntegrate system settings


    /// 4th Runge-Kutta method (aka RK4).
    let integrateRK4<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm>
        (system: CelestialSystem<'l, 't, 'm>)
        (settings: IntegratorSettings<'l, 't, 'm>)
        : IntegratorStep<'l, 't> array
        =
        let Δt = settings.DeltaT
        let Δt1_2 = 0.5 * Δt
        let μs = calculateGravitationalParameters system settings

        let inline accelerate R = settings.Accelerator R μs

        let inline weighK (k1s: NumericalVector3<'u> array) (k2s: NumericalVector3<'u> array) (k3s: NumericalVector3<'u> array) (k4s: NumericalVector3<'u> array) =
            k1s
            |> Array.mapi (fun i k1 -> (k1 + 2.0 * k2s.[i] + 2.0 * k3s.[i] + k4s.[i]) / 6.0)

        let inline calculateNextStep (index: int) (step: IntegratorStep<'l, 't>) =
            let k1V = step.V
            let k1A = accelerate step.R

            let k2V = advanceState step.V k1A Δt1_2
            let k2A = accelerate (advanceState step.R k1V Δt1_2)

            let k3V = advanceState step.V k2A Δt1_2
            let k3A = accelerate (advanceState step.R k2V Δt1_2)

            let k4V = advanceState step.V k3A Δt
            let k4A = accelerate (advanceState step.R k3V Δt)

            let R' = weighK k1V k2V k3V k4V
            let V' = weighK k1A k2A k3A k4A

            {
                T = settings.TStart + Δt * (float index + 1.0)
                R = advanceState step.R R' Δt
                V = advanceState step.V V' Δt
            }

        calculateNextStep |> baseIntegrate system settings


    /// Velocity Verlet method.
    let integrateVelocityVerlet<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm>
        (system: CelestialSystem<'l, 't, 'm>)
        (settings: IntegratorSettings<'l, 't, 'm>)
        : IntegratorStep<'l, 't> array
        =
        let Δt = settings.DeltaT
        let Δt1_2 = 0.5 * Δt
        let μs = calculateGravitationalParameters system settings

        let initialPositions = system.Bodies |> Array.map (fun body -> body.R)
        let mutable Atmp = settings.Accelerator initialPositions μs

        let calculateNextStep (index: int) (step: IntegratorStep<'l, 't>) =
            let V1 = advanceState step.V Atmp Δt1_2
            let R1 = advanceState step.R V1 Δt
            let A1 = settings.Accelerator R1 μs
            let V1 = advanceState step.V (meanOf Atmp A1) Δt
            Atmp <- A1

            {
                T = settings.TStart + Δt * (float index + 1.0)
                R = R1
                V = V1
            }

        calculateNextStep |> baseIntegrate system settings


    /// Leapfrog method.
    let integrateLeapfrog<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm>
        (system: CelestialSystem<'l, 't, 'm>)
        (settings: IntegratorSettings<'l, 't, 'm>)
        : IntegratorStep<'l, 't> array
        =
        let Δt = settings.DeltaT
        let Δt1_2 = 0.5 * Δt
        let μs = calculateGravitationalParameters system settings

        let initialPositions = system.Bodies |> Array.map (fun body -> body.R)
        let mutable A = settings.Accelerator initialPositions μs

        let calculateNextStep (index: int) (step: IntegratorStep<'l, 't>) =
            let V_mid = advanceState step.V A Δt1_2
            let R1 = advanceState step.R V_mid Δt
            A <- settings.Accelerator R1 μs
            let V1 = advanceState V_mid A Δt1_2

            {
                T = settings.TStart + Δt * (float index + 1.0)
                R = R1
                V = V1
            }

        calculateNextStep |> baseIntegrate system settings
