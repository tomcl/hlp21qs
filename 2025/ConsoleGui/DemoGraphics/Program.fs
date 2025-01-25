open System
open ConsoleRenderer

(*

Super-simple Graphics Library for .Net using terminal IO.
For more info, see: https://github.com/NinovanderMark/ConsoleRenderer

In that repository, you will find the following:

For all the methods of ConsoleCanvas, see:
ConsoleRenderer/ConsoleCanvas.cs

For more examples (written in C# - use chatgpt to convert to F# if need be), see:
ConsoleRenderer.Examples/Programs/*.cs


*)

/// F# version of C# ConsoleCanvas to make Intellisense work on members!
type ConsoleCanvas(width:int, height:int) =
    inherit ConsoleRenderer.ConsoleCanvas(width, height)


[<EntryPoint>]
let main argv =
    let canvas = 
        ConsoleCanvas(100,30)
        |> fun c -> c.CreateBorder()
        |> fun c -> c.Text(50,15,"Hello World!")
        |> fun c -> c.Render()

        


    [1..10] |> List.iter (fun i ->
        Console.ReadKey() |> ignore
        canvas.Text(50+i,15+i,$"Hello World {i}!")
        |> fun c -> c.Render()
        |> ignore)
    0 // Return 0 to indicate successful execution

