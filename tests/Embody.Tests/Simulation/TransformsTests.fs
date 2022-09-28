namespace Tests.Simulation

open Xunit


module internal Samples =

    open Embody.Simulation
    open FSharp.Data.UnitSystems.SI.UnitSymbols

    let Preset: SimulationPreset<m, s> = {
        Settings = {
            TStart = 0.0<s>
            TEnd = 0.5<s>
            DeltaT = 0.1<s>
            Accelerator = fun _ _ -> failwith "Dummy accelerator!"
        }
        System = { Bodies = [||] }
        Integrator = fun _ _ -> failwith "Dummy integrator!"
    }

    let SimulationResult = {
        Preset = Preset
        Steps = [|
            { T = 0.0<s>; R = [||]; V = [||] }
            { T = 0.1<s>; R = [||]; V = [||] }
            { T = 0.2<s>; R = [||]; V = [||] }
            { T = 0.3<s>; R = [||]; V = [||] }
            { T = 0.4<s>; R = [||]; V = [||] }
        |]
    }


module ``When extracting time steps from simulation result`` =

    open Embody.Simulation
    open Tests.Utilities
    open FSharp.Data.UnitSystems.SI.UnitSymbols


    [<Fact>]
    let ``Given sample result, Then return expected time steps`` () =
        let expected = [| 0.0<s>; 0.1<s>; 0.2<s>; 0.3<s>; 0.4<s> |]

        let actual = Samples.SimulationResult |> Transforms.timeSteps

        actual |> shouldBeEqualTo expected
