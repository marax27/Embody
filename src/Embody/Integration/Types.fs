namespace Embody.Integration

open Embody.Domain
open Embody.LinearAlgebra

module private HelperTypes =
    type Time<[<Measure>] 't> = float<'t>

    type Position<[<Measure>] 'l> = NumericalVector3<'l>

    type Acceleration<[<Measure>] 'l, [<Measure>] 't> = NumericalVector3<'l/'t^2>

    type Velocity<[<Measure>] 'l, [<Measure>] 't> = NumericalVector3<'l/'t>

    type GravitationalParameter<[<Measure>] 'l, [<Measure>] 't> = float<'l^3/'t^2>

open HelperTypes


/// Accelerator is a function that calculates accelerations for each body,
/// given bodies' current positions.
type Accelerator<[<Measure>] 'l, [<Measure>] 't> =
    Position<'l> array -> GravitationalParameter<'l,'t> array
        -> Acceleration<'l,'t> array

/// Represents a single integrator step: contains a time step as well as all bodies'
/// positions and velocities at that time step.
type IntegratorStep<[<Measure>] 'l, [<Measure>] 't> = {
    T: Time<'t>
    R: Position<'l> array
    V: Velocity<'l,'t> array
}

/// Contains non-domain integrator parameters.
type IntegratorSettings<[<Measure>] 'l, [<Measure>] 't> = {
    TStart: Time<'t>
    TEnd: Time<'t>
    DeltaT: Time<'t>
    Accelerator: Accelerator<'l, 't>
}

/// A basis for actual integrators.
type Integrator<[<Measure>] 'l, [<Measure>] 't> =
    CelestialSystem<'l, 't> -> IntegratorSettings<'l, 't>
        -> IntegratorStep<'l, 't> array
