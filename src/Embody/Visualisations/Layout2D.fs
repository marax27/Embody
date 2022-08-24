namespace Embody.Visualisations


module internal Layout2D =

    open XPlot.Plotly

    type Builder = {
        xAxisType: string
        yAxisType: string
        xLabel: string
        yLabel: string
        title: string
    }

    let inline init () = {
        xAxisType = ""
        yAxisType = ""
        xLabel = ""
        yLabel = ""
        title = ""
    }

    let private mapAxisType = function
    | Linear -> ""
    | Logarithmic -> "log"

    let inline withXAxisType axisType builder = { builder with xAxisType = mapAxisType axisType }

    let inline withYAxisType axisType builder = { builder with yAxisType = mapAxisType axisType }

    let inline withXLabel label builder = { builder with xLabel = label }

    let inline withYLabel label builder = { builder with yLabel = label }

    let inline withTitle title builder = { builder with title = title }

    let inline build builder =
        new Layout(
            xaxis = Xaxis(``type`` = builder.xAxisType, title=builder.xLabel, autorange=true),
            yaxis = Yaxis(``type`` = builder.yAxisType, title=builder.yLabel, autorange=true),
            title = builder.title,
            showlegend = true
        )