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

(*
DEMO wanted:
1. Use ReadConsoleInput to read the next Keypress or mouse event
   https://learn.microsoft.com/en-us/windows/console/reading-input-buffer-events
2. Replace the direct ConsoleCanvas output here with the same via the untested but simple Screen.fs library.
   Purpose of this library is to cache screen output and only update the screen when necessary with render
3. Add to Screen a function string -> Model -> Model that draws a string starting at a given row and column into the NextScreen field of Model.
4. Write a simple tail recursive function cliLoop that reads the next keypress, updates the screen, and then calls itself
   until the user presses the ESC key. In addition, if the user clicks the mouse, it should write a * character at the
    mouse click location. 
5. The function should recurse with Model as its argument. UModel can be a record type with
    a single int field for this demo
6. The integer must be written to the top of the screen, and incrememnted by 1 each time the user presses a key
7. The screen should be rendered once per cliLoop iteration.

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

