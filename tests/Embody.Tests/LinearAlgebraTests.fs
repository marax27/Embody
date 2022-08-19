module Tests.LinearAlgebra

open System
open Xunit
open Embody.LinearAlgebra

[<Fact>]
let ``My test`` () =
    let given = "Name."
    let expected = "Hello Name."

    let actual = hello given

    Assert.Equal(expected, actual)
