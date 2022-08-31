namespace Tests


module Utilities =

    open Xunit
    open FsUnit.Xunit

    /// Check if 2 arrays are equal. It is a composition of available FsUnit/Xunit assertions.
    let shouldBeEqualTo (expected: 'a array) (actual: 'a array) =

        actual |> should haveLength (expected |> Array.length)

        (Array.forall2 (=) expected actual) |> should be True
