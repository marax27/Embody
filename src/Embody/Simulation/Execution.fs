namespace Embody.Simulation

module Execution =

    /// Execute a single simulation.
    let simulate preset =
        let steps = preset.Integrator preset.System preset.Settings
        {
            Preset = preset
            Steps = steps
        }


    /// Execute a single simulation, asynchronously.
    let simulateAsync preset = async {
        let steps = preset.Integrator preset.System preset.Settings
        return {
            Preset = preset
            Steps = steps
        }
    }


    /// Execute multiple simulations in parallel, and return results once all simulations have finished.
    ///
    /// Limitations: all presets must use identical units of measure.
    let simulateInParallel presets =
        presets
        |> Seq.map simulateAsync
        |> Async.Parallel
        |> Async.RunSynchronously
