namespace Embody.Simulation

/// A module that offers a few simple ways of executing a simulation.
module Execution =

    /// Execute a single simulation.
    let inline simulate preset =
        let steps = preset.Integrator preset.System preset.Settings
        {
            Preset = preset
            Steps = steps
        }


    /// Execute a single simulation, asynchronously.
    let inline simulateAsync preset = async {
        let steps = preset.Integrator preset.System preset.Settings
        return {
            Preset = preset
            Steps = steps
        }
    }


    /// Execute multiple simulations in parallel, and return results once all simulations have finished.
    ///
    /// Limitations: all presets must use identical units of measure.
    let inline simulateInParallel presets =
        presets
        |> Seq.map simulateAsync
        |> Async.Parallel
        |> Async.RunSynchronously
