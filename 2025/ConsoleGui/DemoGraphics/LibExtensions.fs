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
    /// Return a list of the Map values
    let valuesL (m: Map<'a,'B> ) = 
        m
        |> Map.values
        |> Seq.toList

    /// Return a list of the Map keys
    let keysL (m: Map<'a,'B> ) = 
        m
        |> Map.keys
        |> Seq.toList
    
    /// Return the value in Map m at key, or defaultValue if key is not in the map
    let findWith (key: 'a) (defaultValue: 'b) (m: Map<'a,'b>) =
        match Map.tryFind key m with
        | Some v -> v
        | None -> defaultValue

    /// Return the union of the two maps, if there are duplicate keys, 
    /// the value from the first map is used
    let union (m1: Map<'a,'b>) (m2: Map<'a,'b>) =
        m1
        |> Map.toSeq
        |> Seq.fold (fun m (k,v) -> Map.add k v m) m2

    /// Return the union of the two maps.
    /// The return value is f12 m1[key] m2[key] if key is in both maps.
    /// If key is only in one map, the return value is f1 or f2 applied to the m1 or m2 value.
    let unionWith (f1: 'b -> 'd) (f2: 'c -> 'd) (f12: 'b -> 'c -> 'd) (m1: Map<'a,'b>) (m2: Map<'a,'c>) =
        let m2' = Map.map (fun _ v2 -> f2 v2) m2
        (m2', m1)
        ||> Map.fold (fun m2' k v1 ->
            let v12 =
                match Map.tryFind k m2 with
                | Some v2 -> f12 v1 v2
                | None -> f1 v1
            Map.add k v12 m2')
            


/// Apply function (nextState) to a value (initially state) repeatedly while (loopCondition value) is met.
/// Return the value after the last application of the function, or
/// the value if the condition is not met initially.
let rec doWhile (loopCondition: 'a -> bool) (nextState: 'a -> 'a) (state: 'a) =
    match loopCondition state with
    | false ->  state
    | true -> doWhile loopCondition nextState (nextState state)

