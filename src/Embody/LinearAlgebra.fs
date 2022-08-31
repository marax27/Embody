namespace Embody

module LinearAlgebra =

    /// Data structure that represents a 3D vector.
    type NumericalVector3<[<Measure>] 'u> =
        {
            x: float<'u>
            y: float<'u>
            z: float<'u>
        }

        static member inline (+) (v, u) = {
            x = v.x + u.x
            y= v.y + u.y
            z = v.z + u.z
        }

        static member inline (-) (v, u) = {
            x = v.x - u.x
            y = v.y - u.y
            z = v.z - u.z
        }

        static member inline (*) (v: NumericalVector3<'v>, scalar: float<'s>) = {
            x = v.x * scalar
            y = v.y * scalar
            z = v.z * scalar
        }

        static member inline (*) (scalar: float<'s>, v: NumericalVector3<'v>) =
            v * scalar

        static member inline (/) (v: NumericalVector3<'v>, scalar: float<'s>) = {
            x = v.x / scalar
            y = v.y / scalar
            z = v.z / scalar
        }

        static member inline (~-) v = {
            x = -v.x
            y = -v.y
            z = -v.z
        }

        static member inline (~+) v = v

    /// Construct an actual 3D vector.
    let inline vector3 x y z = { x = x; y = y; z = z }

    /// A module which implements vector operations.
    module Vector3 =

        /// Compute squared length (squared magnitude) of a vector.
        let inline squaredLength (vector: NumericalVector3<'u>): float<'u^2> =
            vector.x * vector.x + vector.y * vector.y + vector.z * vector.z

        let inline length (vector: NumericalVector3<'u>): float<'u> =
            let result = (squaredLength vector |> float) ** 0.5
            result |> LanguagePrimitives.FloatWithMeasure
