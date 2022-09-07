# Major features in 𝒏 minutes


## Vector arithmetic

```fsharp
open Embody.LinearAlgebra
open FSharp.Data.UnitSystems.SI.UnitSymbols

let position = vector3 5.0<m> 4.0<m> 3.0<m>
let velocity = vector3 10.0<m/s> 0.0<m/s> 0.0<m/s>
let deltaT = 0.1<s>

let newPosition = position + velocity * deltaT

printfn "%A" newPosition
```

## Celestial system definition

```fsharp
open Embody.Domain
open Embody.LinearAlgebra

(* Instead of relying on SI unit system, one can choose their own units.
It can be useful when comparing with external data that use particular units.
The downside is that some parameters (e.g. gravitational constant)
depend on a choice of units and will have to be recalculated. *)
[<Measure>] type km
[<Measure>] type s
[<Measure>] type earthMass

let earthOrbitRadius = 150e6<km>
let averageEarthSpeed = 29.78<km/s>

let system : CelestialSystem<km, s, earthMass> = {
    Bodies = [|
        {
            Name = "Sol"
            Mass = 332950.0<earthMass>
            R = Vector3.zero()
            V = Vector3.zero()
        }
        {
            Name = "Earth"
            Mass = 1.0<earthMass>
            R = vector3 earthOrbitRadius 0.0<_> 0.0<_>
            V = vector3 0.0<_> averageEarthSpeed 0.0<_>
        }
    |]
}
```

## Simulation

Assuming we'd like to simulate the motion of the above system:

```fsharp
open Embody.Integration
open Embody.Simulation

(*
    The simulation preset expects a gravitational constant G expressed in
    'kilometers', 'seconds' and 'earth masses'.
    One can convert a common value of G to desired units. When units of measure
    are explicitly specified, the compiler will verify the unit of a calculation result.

    `kg` and `m` are used for conversion purposes. They are not used in the simulation.
*)
[<Measure>] type kg
[<Measure>] type m

let gravitationalConstant: float<km^3/earthMass/s^2> =
    let G = 6.674e-11<m^3/kg/s^2>                    // commonly known value of G

    // No time conversion needed: seconds are used in both cases.
    let m2km = 1.0<km>/1000.0<m>                     // distance conversion
    let kg2EarthMass = 1.0<earthMass>/5.9722e24<kg>  // mass conversion
    G * m2km * m2km * m2km / kg2EarthMass


(* Integrator settings.
    - The number of steps depends on `DeltaT`, `TStart` and `TEnd` parameters.
    - Accelerator: a function that computes body accelerations.
 *)
let settings: IntegratorSettings<km, s, earthMass> = {
    DeltaT = 3600.0<s>
    TStart = 0.0<s>
    TEnd = 3600.0<s> * 24.0 * 365.25
    GravitationalConstant = gravitationalConstant
    Accelerator = Accelerators.allBodiesConnected
}


(* Preset is a wrapper on all the things needed to kick off a simulation.
    - Celestial system
    - Integrator settings
    - Integrator: a function that implements a numerical method.
 *)
let preset: SimulationPreset<km, s, earthMass> = {
    System = system
    Settings = settings
    Integrator = Integrators.integrateVelocityVerlet
}

let result = preset |> Execution.simulate

printfn "Generated %d steps (~%f hours in a year)." result.Steps.Length (24.0*365.25)

let lastStep = result.Steps |> Array.last
let earthPosition = lastStep.R.[1] - lastStep.R.[0]
printfn "Final Earth's position (relative to Sun): %A" earthPosition
```

