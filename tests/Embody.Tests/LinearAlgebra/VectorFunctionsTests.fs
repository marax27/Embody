namespace Tests.LinearAlgebra

open Xunit
open FsUnit.Xunit
open Embody.LinearAlgebra


module ``When calculating vector3's properties`` =
    
    [<Measure>] type U
    [<Measure>] type V

    [<Fact>]
    let ``Test zero vector`` () =

        let expected = vector3 0.0<U^2> 0.0<U^2> 0.0<U^2>
        let actual = Vector3.zero ()

        let areEqual = expected = actual

        areEqual |> should be True

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

    [<Fact>]
    let ``Test dot vector`` () =
        
        let givenLeft = vector3 5.0<U> -3.0<U> 1.0<U>
        let givenRight = vector3 -1.0<V^2> -3.0<V^2> 2.5<V^2>
        let expected = 6.5<U*V^2>

        let actual = Vector3.dot givenLeft givenRight
        let areEqual = actual = expected

        areEqual |> should be True

    [<Fact>]
    let ``Test cross vector`` () =
        
        let givenLeft = vector3 1.0<U> 1.0<U> 3.0<U>
        let givenRight = vector3 1.0<V> 0.0<V> 2.0<V>
        let expected = vector3 2.0<U*V> 1.0<U*V> -1.0<U*V>

        let actual = Vector3.cross givenLeft givenRight
        let areEqual = expected = actual

        areEqual |> should be True

