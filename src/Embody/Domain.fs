namespace Embody

/// Contains building blocks required to define an n-body system to be simulated.
module Domain =

    open LinearAlgebra

    /// Data structure that represents a single celestial body.
    type CelestialBody<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm> = {
        Name: string
        Mass: float<'m>
        R: NumericalVector3<'l>
        V: NumericalVector3<'l/'t>
    }

    /// Data structure that represents a simulated celestial system, with
    /// all its bodies.
    type CelestialSystem<[<Measure>] 'l, [<Measure>] 't, [<Measure>] 'm> = {
        Bodies: CelestialBody<'l, 't, 'm> array
    }
