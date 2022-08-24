namespace Embody.Integration

module Accelerators =

    open Embody.LinearAlgebra

    let private power (squaredLength: float<'l^2>): float<'l^-3> =
        let result = (float squaredLength) ** -1.5
        result |> LanguagePrimitives.FloatWithMeasure

    /// Accelerator that calculates forces between every pair of bodies.
    ///
    /// TODO: optimize: r_ij and r_ji are currently calculated separately, even though r_ij = -r_ji.
    let allBodiesConnected<[<Measure>] 'l, [<Measure>] 't>
        (positions: NumericalVector3<'l> array)
        (gravitationalParameters: float<'l^3/'t^2> array)
        : NumericalVector3<'l/'t^2> array
        =
        let bodyIndices = [0 .. positions.Length - 1]
        let zeroAcceleration: NumericalVector3<'l/'t^2> = vector3 0.0<_> 0.0<_> 0.0<_>

        let calculateSingleAcceleration (i: int): NumericalVector3<'l/'t^2> =
            let offsets = bodyIndices
                        |> List.except [ i ]
                        |> List.map (fun j -> j, positions.[j] - positions.[i])
            offsets
            |> List.map (fun (j,  ΔR) -> gravitationalParameters.[j] * (ΔR |> Vector3.squaredLength |> power) * ΔR)
            |> List.fold (fun acc elem -> acc + elem) zeroAcceleration

        positions |> Array.mapi (fun i R -> calculateSingleAcceleration i)
