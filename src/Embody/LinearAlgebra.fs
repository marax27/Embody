namespace Embody

module LinearAlgebra =

    /// Data structure that represents a 3D vector.
    type NumericalVector3<[<Measure>] 'u> =
        {
            x: float<'u>
            y: float<'u>
            z: float<'u>
        }

        static member (+) (v, u) = {
            x = v.x + u.x
            y= v.y + u.y
            z = v.z + u.z
        }

        static member (-) (v, u) = {
            x = v.x - u.x
            y = v.y - u.y
            z = v.z - u.z
        }

    /// Construct an actual 3D vector.
    let inline vector3 x y z = { x = x; y = y; z = z }

    /// A module which implements vector operations.
    module Vector3 =

        /// Compute squared length (squared magnitude) of a vector.
        let inline squaredLength vector =
            vector.x * vector.x + vector.y * vector.y + vector.z * vector.z

        let inline length (vector: NumericalVector3<'u>): float<'u> =
            let result = (squaredLength vector |> float) ** 0.5
            result |> LanguagePrimitives.FloatWithMeasure
