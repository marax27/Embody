namespace Tests.LinearAlgebra

open Xunit
open Embody.LinearAlgebra

module ``When creating a vector3`` =
    
    [<Measure>] type U

    [<Fact>]
    let ``Given sample values, Then vector3 contains expected values`` () =
    
        let sut = vector3 1.2345 9.0 0.999

        Assert.Equal(1.2345, sut.x)
        Assert.Equal(9.0, sut.y)
        Assert.Equal(0.999, sut.z)

    [<Fact>]
    let ``Given typed values, Then vector3 preserves types`` () =
        
        let sut = vector3 5.0<U> 10.0<U> 2.0<U>

        Assert.Equal(5.0<U>, sut.x)
        Assert.Equal(10.0<U>, sut.y)
        Assert.Equal(2.0<U>, sut.z)

    [<Fact>]
    let ``Given typed values, Then vector3 contains expected values`` () =
        
        let sut = vector3 5.0<U> 10.0<U> 2.0<U>

        Assert.Equal(5.0, float sut.x)
        Assert.Equal(10.0, float sut.y)
        Assert.Equal(2.0, float sut.z)


module ``When calculating vector3's properties`` =
    
    [<Measure>] type U

    [<Fact>]
    let ``Given sample vector, Then it has expected squared length`` () =
        
        let expected = 121.0
        let sut = vector3 2.0 6.0 9.0

        let actual = sut |> Vector3.squaredLength 

        Assert.Equal(actual, expected)

    [<Fact>]
    let ``Given sample vector, Then it has expected length`` () =
        
        let expected = 11.0<U>
        let sut = vector3 2.0<U> 6.0<U> 9.0<U>

        let actual = sut |> Vector3.length

        Assert.Equal(actual, expected)


module ``When performing binary vector3 operations`` =

    let SampleVector = vector3 5.0 -1.0 2.0
    let OtherVector = vector3 -15.0 -1.0 1.0

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Sample + Other` returns expected vector`` () =
        let expected = vector3 -10.0 -2.0 3.0
        let actual = SampleVector + OtherVector
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Other + Sample` returns expected vector`` () =
        let expected = vector3 -10.0 -2.0 3.0
        let actual = OtherVector + SampleVector
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Sample - Other` returns expected vector`` () =
        let expected = vector3 20.0 0.0 1.0
        let actual = SampleVector - OtherVector
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``Given 2 vectors (Sample, Other), Then `Other - Sample` returns expected vector`` () =
        let expected = vector3 -20.0 0.0 -1.0
        let actual = OtherVector - SampleVector
        Assert.Equal(expected, actual)

