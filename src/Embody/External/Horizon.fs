namespace Embody.External


/// A module that is responsible for working with data from Horizons System.
///
/// See: https://ssd.jpl.nasa.gov/horizons/
module Horizon =

    module private FileSystem =

        open System.IO

        let readLinesFromFile (filename: string) = seq {
            use stream = new StreamReader (filename)
            let mutable valid = true
            while (valid) do
                let line = stream.ReadLine()
                if (line = null) then valid <- false
                else yield line
        }


    open System.Text.RegularExpressions

    type VectorTableStep = {
        JulianDayNumber: float
        X: float; Y: float; Z: float
        VX: float; VY: float; VZ: float
        RG: float
    }

    type Ephemeride<'step> = {
        BodyName: string
        Steps: 'step array
    }

    let private _parseVectorTableHeading (lines: string seq) =
        let bodyNameRegex = new Regex("(?<=Revised.*\\s+)[a-zA-Z]+(?=\\s+/)")
        let bodyNameLine = lines |> Seq.skip 1 |> Seq.head
        let bodyName = bodyNameRegex.Match(bodyNameLine).Captures
                        |> Seq.map (fun s -> s.Value)
                        |> Seq.head

        let newLines = lines
                        |> Seq.skipWhile (fun line -> not <| line.Contains("$$SOE"))
                        |> Seq.skip 1
        (bodyName, newLines)

    let private _parseVectorTableContent (lines: string seq) =
        let floatRegex = new Regex("[+-]?[0-9]+[.][0-9]*([eE][+-]?[0-9]+)?")
        let parseLine line = floatRegex.Matches(line)
                            |> Seq.map (fun m -> float m.Value)
                            |> Seq.toArray

        let contentLines = lines
                            |> Seq.takeWhile (fun line -> not <| line.Contains("$$EOE"))
                            |> Seq.toArray

        let parseStep i =
            let p1 = parseLine contentLines.[4*i]
            let p2 = parseLine contentLines.[4*i + 1]
            let p3 = parseLine contentLines.[4*i + 2]
            let p4 = parseLine contentLines.[4*i + 3]
            {
                JulianDayNumber = p1.[0]
                X = p2.[0]; Y = p2.[1]; Z = p2.[2]
                VX = p3.[0]; VY = p3.[1]; VZ = p3.[2]
                RG = p4.[1]
            }

        let N = contentLines.Length
        if N % 4 <> 0 then
            failwithf "Found %d lines. Expected 4n lines." N

        [| 0..(N/4)-1 |]
        |> Array.map parseStep

    let parseVectorTable (filename: string): Ephemeride<VectorTableStep> =
        let lines = FileSystem.readLinesFromFile filename
        let (bodyName, ephemerideLines) = _parseVectorTableHeading lines
        let steps = _parseVectorTableContent ephemerideLines
        {
            BodyName = bodyName
            Steps = steps
        }
