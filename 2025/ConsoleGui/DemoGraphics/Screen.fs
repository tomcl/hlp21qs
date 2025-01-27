module Screen
//-------------------------------------------------//
// This is the F# version of the C# ConsoleCanvas  //
// 
open Types


/// Create a new empty screen with a ConsoleCanvas
let initScreen (model: Model<'a>) : Model<'a> = 
    let canvas = 
        ConsoleRenderer.ConsoleCanvas()
        |> fun canvas -> canvas.Clear()
    { model with
        Screen = Map.empty, canvas
        NextScreen = Map.empty
        Width = canvas.Width; 
        Height = canvas.Height; 
    }

/// Return a list of strings to write to the screen
/// To make sOld look like sNew
let diffScreen (sOld: ScreenModel) (sNew: ScreenModel) = 
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
        /// Return indexes grouping changes into contiguous runs
        let stringNums =
            List.pairwise changes
            |> (fun changePairs -> (0,changePairs))
            ||> List.scan (fun grp ((lastKey,lastCh),(key,ch)) -> 
                if lastKey + 1 = key then grp else grp + 1)
        /// Group changes by contiguous runs
        let groupedChanges =
            List.zip stringNums changes
            |> List.groupBy fst
            |> List.map snd
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

        
            
 
        

            
        

