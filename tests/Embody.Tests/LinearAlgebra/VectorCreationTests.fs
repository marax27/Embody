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
