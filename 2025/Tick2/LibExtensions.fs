module LibExtensions

//--------------------------------Additional Library Functions-------------------------------------//
//--------------------------List.combineResults, Array.combineResults------------------------------//

// TODO: these should be added to the EEExtensions library module?

module  List =
    /// Return Error (list of error contents), if there are any, or Ok (list of Ok contents)
    /// This ought to be in the standard library!
    let combineResults (rl: Result<'a,'err> list) =
        rl
        |> List.partition Result.isOk
        |> ( function | (rL, []) -> 
                            rL 
                            |> List.map (function | Ok r -> r | _ -> failwithf "Can't happen") 
                            |> Ok
                        | (_, eL) -> 
                            eL 
                            |> List.map (function | Error e -> e | _ -> failwithf "Can't happen") 
                            |> Error
            )

module Array =
    /// Return Error (list of error contents), if there are any, or Ok (list of Ok contents)
    /// This ought to be in the standard library!
    let combineResults (rl: Result<'a,'err> array) =
        rl
        |> List.ofArray
        |> List.combineResults
        |> Result.map List.toArray
        |> Result.mapError List.toArray

module Map =
    /// return a list of the Map values
    let valuesL (m: Map<'a,'B> ) = 
        m
        |> Map.values
        |> Seq.toList

    /// return a list of the Map keys
    let keysL (m: Map<'a,'B> ) = 
        m
        |> Map.keys
        |> Seq.toList


/// Apply function (nextState) to a value (initially state) repeatedly while (loopCondition value) is met.
/// Return the value after the last application of the function, or
/// the value if the condition is not met initially.
let rec doWhile (loopCondition: 'a -> bool) (nextState: 'a -> 'a) (state: 'a) =
    match loopCondition state with
    | false ->  state
    | true -> doWhile loopCondition nextState (nextState state)

