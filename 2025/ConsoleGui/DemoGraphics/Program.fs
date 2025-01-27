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




[<EntryPoint>]
let main argv =
    let canvas = 
        ConsoleCanvas()
        |> fun c -> c.CreateBorder()
        |> fun c -> c.Text(50,15,"Hello World!")
        |> (fun c -> c.Render())

        


    [1..10] |> List.iter (fun i ->
        canvas
        |> fun c -> c.Text(50+i,15+i,$"Hello World {i}: {canvas.Width}X{canvas.Height}!")
        |> fun c -> c.Render()
        |> ignore)
    0 // Return 0 to indicate successful execution

