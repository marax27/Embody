namespace Tests.LinearAlgebra

open Xunit
open FsUnit.Xunit
open Embody.LinearAlgebra


module ``When performing binary vector3 operations`` =

    let SampleVector = vector3 5.0 -1.0 2.0
    let OtherVector = vector3 -15.0 -1.0 1.0

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Sample + Other` returns expected vector`` () =
        let expected = vector3 -10.0 -2.0 3.0
        let actual = SampleVector + OtherVector
        expected |> should equal actual

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Other + Sample` returns expected vector`` () =
        let expected = vector3 -10.0 -2.0 3.0
        let actual = OtherVector + SampleVector
        expected |> should equal actual

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Sample - Other` returns expected vector`` () =
        let expected = vector3 20.0 0.0 1.0
        let actual = SampleVector - OtherVector
        expected |> should equal actual

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Other - Sample` returns expected vector`` () =
        let expected = vector3 -20.0 0.0 -1.0
        let actual = OtherVector - SampleVector
        expected |> should equal actual


module ``When multiplying a vector by a scalar`` =

    let SampleVector = vector3 2.0 -4.0 7.0

    [<Fact>]
    let ``Given positive scalar, Then `Vector * Scalar` returns expected vector`` () =
        let expected = vector3 6.0 -12.0 21.0
        let actual = SampleVector * 3.0
        expected |> should equal actual

    [<Fact>]
    let ``Given positive scalar, Then `Scalar * Vector` returns expected vector`` () =
        let expected = vector3 6.0 -12.0 21.0
        let actual = 3.0 * SampleVector
        expected |> should equal actual

    [<Fact>]
    let ``Given negative scalar, Then `Vector * Scalar` returns expected vector`` () =
        let expected = vector3 -4.0 8.0 -14.0
        let actual = SampleVector * (-2.0)
        expected |> should equal actual

    [<Fact>]
    let ``Given negative scalar, Then `Scalar * Vector` returns expected vector`` () =
        let expected = vector3 -4.0 8.0 -14.0
        let actual = -2.0 * SampleVector
        expected |> should equal actual


module ``When dividing a vector by a scalar`` =
    
    let SampleVector = vector3 2.0 -4.0 1.0

    [<Fact>]
    let ``Given positive scalar, Then `Vector / Scalar` returns expected vector`` () =
        let expected = vector3 1.0 -2.0 0.5
        let actual = SampleVector / 2.0
        expected |> should equal actual

    [<Fact>]
    let ``Given negative scalar, Then `Vector / Scalar` returns expected vector`` () =
        let expected = vector3 -1.0 2.0 -0.5
        let actual = SampleVector / -2.0
        expected |> should equal actual


module ``When performing unary vector3 operations`` =

    let SampleVector = vector3 5.0 -1.0 0.0

    [<Fact>]
    let ``Given a sample vector, `-Vector` returns expected vector`` () =
        let expected = vector3 -5.0 1.0 0.0
        -SampleVector |> should equal expected

    [<Fact>]
    let ``Given a sample vector, `+Vector` returns expected vector`` () =
        +SampleVector |> should equal SampleVector
