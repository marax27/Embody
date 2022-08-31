namespace Embody

module LinearAlgebra =

    /// Data structure that represents a 3D vector.
    ///
    /// Consider instantiating a vector using a helper function `vector3`.
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


    /// Construct an actual 3D vector. A shorthand function.
    let inline vector3 x y z = { x = x; y = y; z = z }


    /// A module which implements vector operations.
    module Vector3 =

        /// Return a zero vector, with a desired unit of measure.
        let zero (): NumericalVector3<'u> = vector3 0.0<_> 0.0<_> 0.0<_>

        /// Compute squared length (squared magnitude) of a vector.
        let inline squaredLength (vector: NumericalVector3<'u>): float<'u^2> =
            vector.x * vector.x + vector.y * vector.y + vector.z * vector.z


        /// Compute length (magnitude) of a vector.
        let inline length (vector: NumericalVector3<'u>): float<'u> =
            let result = (squaredLength vector |> float) ** 0.5
            result |> LanguagePrimitives.FloatWithMeasure


        /// Compute a dot product of 2 vectors. Vectors can have different units of measure.
        let inline dot (left: NumericalVector3<'u>) (right: NumericalVector3<'v>): float<'u*'v> =
            left.x * right.x + left.y * right.y + left.z * right.z


        /// Compute a cross product of 2 vectors. Vectors can have different units of measure.
        let inline cross (left: NumericalVector3<'u>) (right: NumericalVector3<'v>): NumericalVector3<'u*'v> =
            vector3 (left.y * right.z - left.z * right.y) (left.z * right.x - left.x * right.z) (left.x * right.y - left.y * right.x)


        /// Return a unitless vector, i.e. stripped of its unit of measure.
        let inline strip (vector: NumericalVector3<'u>): NumericalVector3<1> =
            vector3 (float vector.x) (float vector.y) (float vector.z)
