namespace Embody.Visualisations

open Embody

// Common definitions.

type AxisDefinition = {
    Label: string
    Type: AxisType
}
and AxisType = Linear | Logarithmic


// 2D plot-specific definitions.

type Single2DSeries<[<Measure>] 'x, [<Measure>] 'y> = {
    Name: string
    X: float<'x> array
    Y: float<'y> array
}


type Plot2D<[<Measure>] 'x, [<Measure>] 'y> = {
    Title: string
    XAxis: AxisDefinition
    YAxis: AxisDefinition
    Series: Single2DSeries<'x, 'y> seq
}


// 3D plot-specific definitions.

type Single3DTrajectory<[<Measure>] 'l, [<Measure>] 't> = {
    Name: string
    T: float<'t> array
    R: LinearAlgebra.NumericalVector3<'l> array
}


type Plot3D<[<Measure>] 'l, [<Measure>] 't> = {
    Title: string
    Trajectories: Single3DTrajectory<'l, 't> seq
}
