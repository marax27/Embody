namespace Embody.Visualisations


module Plots2D =

    open XPlot.Plotly

    let inline private asScatter2D (series: Single2DSeries<'x, 'y>) =
        Scatter(x = series.X, y = series.Y, name = series.Name)

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


    let inline limitPointCount
        (targetPointCount: int)
        (series: Single2DSeries<'x, 'y>)
        : Single2DSeries<'x, 'y>
        =
        let currentCount = series.X.Length
        let keepEvery = currentCount / targetPointCount

        let inline limit array =
            array
            |> Array.mapi (fun i item -> if i % keepEvery = 0 then Some item else None)
            |> Array.choose id

        { series with X = limit series.X; Y = limit series.Y }
