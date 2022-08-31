namespace Embody.Visualisations


module Plots3D =

    open Embody.LinearAlgebra
    open XPlot.Plotly

    let inline private asScatter3D
        (trajectory: Single3DTrajectory<'l, 't>)
        =
        let X = trajectory.R |> Array.map (fun r -> r.x)
        let Y = trajectory.R |> Array.map (fun r -> r.y)
        let Z = trajectory.R |> Array.map (fun r -> r.z)

        Scatter3d(
            x = X,
            y = Y,
            z = Z,
            name = trajectory.Name,
            text = trajectory.T,
            marker = Marker(size = 1.0)
        )


    /// Plot a 3D XPlot.Plotly chart.
    let inline plot (plot3D: Plot3D<'l, 't>) =
        plot3D.Trajectories
        |> Seq.map asScatter3D
        |> Chart.Plot
        |> Chart.WithLegend true

    
    /// Keep series's point count below a certain threshold.
    /// Might improve rendering performance at the cost of plot resolution.
    let inline limitPointCount
        (pointCountThreshold: int)
        (series: Single3DTrajectory<'l, 't>)
        : Single3DTrajectory<'l, 't>
        =
        let keepEvery =
            let currentCount = series.T.Length
            let ratio = currentCount / pointCountThreshold
            if ratio < 1 then 1 else ratio

        let inline limit array =
            array
            |> Array.mapi (fun i item -> if i % keepEvery = 0 then Some item else None)
            |> Array.choose id

        { series with T = limit series.T; R = limit series.R }
