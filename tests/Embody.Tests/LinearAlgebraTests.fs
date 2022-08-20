namespace Tests.LinearAlgebra

open Xunit
open FsUnit.Xunit
open Embody.LinearAlgebra

module ``When creating a vector3`` =
    
    [<Measure>] type U

    [<Fact>]
    let ``Given sample values, Then vector3 contains expected values`` () =
    
        let sut = vector3 1.2345 9.0 0.999

        sut.x |> should equal 1.2345
        sut.y |> should equal 9.0
        sut.z |> should equal 0.999

    [<Fact>]
    let ``Given typed values, Then vector3 preserves types`` () =
        
        let sut = vector3 5.0<U> 10.0<U> 2.0<U>

        sut.x |> should equal 5.0<U>
        sut.y |> should equal 10.0<U>
        sut.z |> should equal 2.0<U>

    [<Fact>]
    let ``Given typed values, Then vector3 contains expected values`` () =
        
        let sut = vector3 5.0<U> 10.0<U> 2.0<U>

        float sut.x |> should equal 5.0
        float sut.y |> should equal 10.0
        float sut.z |> should equal 2.0


module ``When calculating vector3's properties`` =
    
    [<Measure>] type U

    [<Fact>]
    let ``Given sample vector, Then it has expected squared length`` () =
        
        let expected = 121.0
        let sut = vector3 2.0 6.0 9.0

        let actual = sut |> Vector3.squaredLength 

        expected |> should equal actual

    [<Fact>]
    let ``Given sample vector, Then it has expected length`` () =
        
        let expected = 11.0<U>
        let sut = vector3 2.0<U> 6.0<U> 9.0<U>

        let actual = sut |> Vector3.length

        expected |> should equal actual


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

