namespace Embody.Visualisations


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

// TODO.
