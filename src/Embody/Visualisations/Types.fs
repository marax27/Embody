namespace Embody.Visualisations

open Embody

// Common definitions.

/// Represents a plot's axis (X, Y, maybe even Z).
type AxisDefinition = {
    Label: string
    Type: AxisType
}
and AxisType = Linear | Logarithmic


// 2D plot-specific definitions.

/// Represents a single 2D plot series (a single plot can contain 1 or more series).
type Single2DSeries<[<Measure>] 'x, [<Measure>] 'y> = {
    Name: string
    X: float<'x> array
    Y: float<'y> array
}


/// Represents a single 2D plot.
type Plot2D<[<Measure>] 'x, [<Measure>] 'y> = {
    Title: string
    XAxis: AxisDefinition
    YAxis: AxisDefinition
    Series: Single2DSeries<'x, 'y> seq
}


// 3D plot-specific definitions.

/// Represents a single trajectory on a 3D plot.
type Single3DTrajectory<[<Measure>] 'l, [<Measure>] 't> = {
    Name: string
    T: float<'t> array
    R: LinearAlgebra.NumericalVector3<'l> array
}


/// Represents a single 3D plot.
type Plot3D<[<Measure>] 'l, [<Measure>] 't> = {
    Title: string
    Trajectories: Single3DTrajectory<'l, 't> seq
}
