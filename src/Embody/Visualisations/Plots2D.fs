namespace Embody.Visualisations


module Plots2D =

    open XPlot.Plotly

    let inline private asScatter2D (series: Single2DSeries<'x, 'y>) =
        Scatter(x = series.X, y = series.Y, name = series.Name)

    /// Plot a 2D XPlot.Plotly chart.
    let plot (plot2D: Plot2D<'x, 'y>) =

        let layout = Layout2D.init ()
                    |> Layout2D.withXAxisType plot2D.XAxis.Type
                    |> Layout2D.withYAxisType plot2D.YAxis.Type
                    |> Layout2D.withXLabel plot2D.XAxis.Label
                    |> Layout2D.withYLabel plot2D.YAxis.Label
                    |> Layout2D.withTitle plot2D.Title
                    |> Layout2D.build

        plot2D.Series
        |> Seq.map asScatter2D
        |> Chart.Plot
        |> Chart.WithLayout layout


    /// Keep series's point count below a certain threshold.
    /// Might improve rendering performance at the cost of plot resolution.
    let inline limitPointCount
        (pointCountThreshold: int)
        (series: Single2DSeries<'x, 'y>)
        : Single2DSeries<'x, 'y>
        =
        let keepEvery =
            let currentCount = series.X.Length
            let ratio = currentCount / pointCountThreshold
            if ratio < 1 then 1 else ratio

        let inline limit array =
            array
            |> Array.mapi (fun i item -> if i % keepEvery = 0 then Some item else None)
            |> Array.choose id

        { series with X = limit series.X; Y = limit series.Y }
