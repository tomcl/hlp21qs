// Learn more about F# at http://fsharp.org

open System
open FSharp.Core

//-----------------------------------------------------------------------------------------
//-------------------------------------TEST CODE-------------------------------------------
//-----------------------------------------------------------------------------------------
open Simulate
open Simulate.MapEnvt

/// The test runs a simulated 4-bit adder for 17 steps. Your task is to implement the functions in simulate.fs and ensure
/// the correct function of this test: The output of the adder should be what you expect over 17 cycles, given the inputs.
let testFourBitAdder() =
    let makeDemoCkt cin =
        let circuit = makeCktAdder 4 "A" "Q" "Q" "CIN" "COUT"
        let inputs = makeCktInputs "A" 4u 0u @ makeCktVar "CIN" (fun _env -> cin)  cin
        createEnv (circuit @ inputs)

    let printEnvt envt msg =
        printfn "%s" msg
        printfn "Initial environment\n%A\n--------------------\n" envt

    let printTestInfo cin =
        printfn "Step 0 outputs are variable values from initial environment."
        printfn "Cin = %A" cin

    let printOutputs (envt:Environment) =
        List.map (fun vs -> vs, varLookup vs envt) ["COUT";"Q3";"Q2";"Q1";"Q0"]
        |> List.map (fun (vs,value) -> 
                let vStr = sprintf "%A" value + ","
                sprintf $"{vs}=%-8s{vStr}")
        |> String.concat " "

    [Ok One; Error "CIN"]
    |> List.iter (fun cin -> 
            let envt = makeDemoCkt cin
            printEnvt envt (sprintf "\n\n--------------------\nTesting with cin = %A" cin)
            printTestInfo cin
            nSteps envt 17u
            |> List.map (fun ev -> printOutputs ev)
            |> List.iteri (printfn "Step %2d: %s" ))

[<EntryPoint>]
let main argv =
    printfn "Starting tests!"
    testFourBitAdder()
    0 // return an integer exit code
