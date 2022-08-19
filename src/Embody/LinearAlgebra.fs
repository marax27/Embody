namespace Embody

module LinearAlgebra =

    /// Data structure that represents a 3D vector.
    type NumericalVector3<[<Measure>] 'u> =
        {
            x: float<'u>
            y: float<'u>
            z: float<'u>
        }

    /// Construct an actual 3D vector.
    let inline vector3 x y z = { x = x; y = y; z = z }

    /// A module which implements vector operations.
    module Vector3 =
        let foo () =
            123
