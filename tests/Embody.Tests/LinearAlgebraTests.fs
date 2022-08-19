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
