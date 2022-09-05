# Embody

[![.NET Build and Test](https://github.com/marax27/Embody/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/marax27/Embody/actions/workflows/build-and-test.yml)
[![GitHub issues](https://img.shields.io/github/issues-raw/marax27/Embody)](https://github.com/marax27/Embody/issues)
[![GitHub](https://img.shields.io/github/license/marax27/Embody)](https://github.com/marax27/Embody/blob/main/LICENSE.txt)
[![Nuget](https://img.shields.io/nuget/v/Embody)](https://www.nuget.org/packages/Embody)

**Embody** is an F# library that allows you to easily define, execute, and validate $n$-body simulations.


## Summary

**Embody** started out as an interactive .NET notebook where I could simulate and evaluate $n$-body systems (most notably, the Earth-Moon-Sun system, and Jupiter with its myriad of moon). The notebook kept on growing, so ultimately I decided to move most of the code into a separate package.

Currently, Embody is a single package that tackles a couple different responsibilities, including: vector operations, numerical integration, and plot visualisation.

## Getting started

The standard way of working with the library is via interactive .NET notebooks.

### Prerequisites
- [VS Code](https://code.visualstudio.com/)
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [.NET Interactive Notebooks](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode) extension

### First notebook

In order to create a blank notebook:
1. Press `Ctrl + Shift + P` in VS Code editor.
2. Select the ".NET Interactive: Create new blank notebook" option.
3. Select "Create as .dib".
4. Select "F#".
5. The notebook should open. Type the following line into the code block:
    ```fsharp
    #r "nuget: Embody"
    ```
    and then press `Shift + Enter` or press "Execute cell".

    In general, you should specify a package version after the package name, like this: `#r "nuget: Embody, 0.1.1"`. For the most recent version, please check the "Script & Interactive" tab at the [Package page](https://www.nuget.org/packages/Embody).
6. After the package is imported, you can perform a simple vector arithmetic to verify if the package works. Type the following code into the second code block:
    ```fsharp
    open Embody.LinearAlgebra

    let a = vector3 0.1 0.2 0.3
    let b = vector3 1.0 2.0 3.0

    a + b
    ```
    After executing the cell, you should see the following output:
    |  x  |  y  |  z  |
    |-----|-----|-----|
    | 1.1 | 2.2 | 3.3 |
7. Congratulations! You've successfully created a .NET interactive notebook, and imported the Embody package. The next step would be to define an $n$-body system. Example(s) can be found [here](https://github.com/marax27/Embody/tree/main/notebooks).


## Future plans / TODOs
- Documentation page.
- Theoretical background behind implemented numerical methods and orbital simulations.
- Performance optimization.
- Memory usage consideration (it is currently a bit too easy to have all your RAM consumed by a long-running simulation).
