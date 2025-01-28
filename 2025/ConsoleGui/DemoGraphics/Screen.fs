module Screen
//-------------------------------------------------//
// This is a wrapper for C# ConsoleCanvas  Which provides a convenenient way to write
// characters to the console screen.  The ScreenModel is a 2D map of characters, where
// ScreenModel[y][x] is the character in  row y, column x.
// Characters not in the map are assumed to be spaces.
//
// Model<'a> is a partly user defines model that contains screen and user-defined persistent state
//
// The ScreenModel type is used by the render function, which writes the NextScreen to the console.
// The NextScreen field of Model should updated by the user code, and then render called to update the screen.
//
// The 'a type parameter of Model is the user state type in Model, it should be a record type
// containing all persistent user state
// getScreenChar is a helper function to get the character in NextScreen at a given row and column.
// initScreen creates a new empty screen with a ConsoleCanvas.
open Types


/// Create a new empty screen with a ConsoleCanvas
let initScreen (model: Model<'a>) : Model<'a> = 
    let canvas = 
        ConsoleRenderer.ConsoleCanvas()
        |> fun canvas -> canvas.Clear()
    { model with
        ConsoleScreen = Map.empty, canvas
        NextScreen = Map.empty
        Width = canvas.Width; 
        Height = canvas.Height; 
    }

/// Return the current (but possibly not rendered) character at row y, column x
let getScreenChar (y: int) (x: int) (model: Model<'a>) = 
    match Map.tryFind y model.NextScreen with
    | Some row -> 
        match Map.tryFind x row with
        | Some c -> c
        | None -> ' '
    | None -> ' '

/// Return a list of strings to write to the screen
/// To make sOld look like sNew
/// Used by render
let private diffScreen (sOld: ScreenModel) (sNew: ScreenModel) = 
    /// any key old or new in the two maps
    let allKeys (oldM: Map<'k,'v>) (newM: Map<'k,'v>) =
        Seq.append oldM.Keys newM.Keys
        |> Seq.toList
        |> List.distinct
        |> List.sort

    let getChar (y: int) (x: int) (scr: ScreenModel) = 
        match Map.tryFind y sOld with
        | Some row -> 
            match Map.tryFind x row with
            | Some c -> c
            | None -> ' '
        | None -> ' '

    let diffRow (y: int) =
        /// all changes in the row as (x, newChar) pairs
        let changes =
            allKeys sOld sNew
            |> List.collect (fun x -> 
                match getChar y x sOld, getChar y x sNew with
                | ch1, ch2 when ch1 <> ch2 -> [x,ch2]
                | _ -> [])

        /// Group changes by contiguous runs
        let groupedChanges =
            List.indexed changes
            |> List.groupBy (fun (i,(col,_ch)) -> col - i)
            |> List.map (snd >> List.map snd)
        /// Convert run of changes into lowest col index and string of changes.
        /// Return strings to write to the screen, indexed by first y (row) and then x (col) index.

        let stringsToWrite =
            groupedChanges
            |> List.map (fun changes -> 
                y,
                fst (List.head changes), 
                changes |> List.map (fun (_,ch) -> $"{ch}") |> String.concat "")
        stringsToWrite
    allKeys sOld sNew
    |> List.collect diffRow

/// Write the NextScreen to the console
/// Update Screen to be NextScreen, so Screen is in sync with the Conslanole
let render (model: Model<'a>) : Model<'a> = 
    let (screen,canvas) = model.ConsoleScreen
    diffScreen screen model.NextScreen
    |> List.iter (fun (y,x,s) -> canvas.Text(x, y, s) |> ignore)
    canvas.Render() |> ignore
    { model with ConsoleScreen = model.NextScreen, snd model.ConsoleScreen }

        
            
 
        

            
        

