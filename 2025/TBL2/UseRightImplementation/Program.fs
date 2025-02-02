// For each of the following functions, implement the function using one of the following methods:
// 1. List, Map or Set functions (without fold or scan)
// 2. List, Map or Set functions (with fold or scan)
// 3. Using recursion and List, Map or Set functions (possibly with fold or scan)
// Prefer lower numbered items in the above methods above where possible.
// If you think a higher-numbered implementation is better even though a lower is possible,
// please explain why in a comment.
//
// Note - nearly everything is lists, but you may use other collections if you think it is appropriate.
// Note - for 3. implementations you may modify the function headers below to allow a recursive definition.

// Part A. Individual work:
// Without starting any implementation say which method 1., 2. or 3., you would use and why.
// For each method, guess which List or Map functions would be part of that implementation.
// Sketch a solution by stating in order the functions you would use and (if not obvious) their purpose.
// For a recursive definition (if you need that) state the recursive reduction rules and end cases in
// whatever way is easiest for you: or state how the recursion works without necessarily being
// precise about end cases - as long as it is obvious they exist.
//
// Part B. Team work:
// 1. Compare individual answers and decide which method to use. If there are differences, discuss and decide.
//    Make the "sketched solution" understandable.
// 2. (only after 1. is completed). Implement the functions using the agreed methods.



/// You are not allowed to use List.chunkBySize in this implementation!
/// the better implementation is chunkBySize
let chunkBySize (size: int) (lst: 'a list) : 'a list list =
     List.indexed lst
     |> List.groupBy (fun (i,_x) -> i / size)
     |> List.map (fun (_i, lst) -> lst |> List.map snd)

/// If list is not very large this is OK.
/// this is not tail recursive so will fail on large lists
/// An extra line or two to make it tail recursive.
let rec chunkBySize1 (size: int) (lst: 'a list) : 'a list list =
        match lst with
        | lst when lst.Length <= size -> [lst]
        | _ -> let chunk, rest = List.splitAt size lst
               chunk :: chunkBySize size rest

/// Fold as here is not very nice!
let chunkBySize2 (size: int) (lst: 'a list)  =
    ( ([],[]) , lst)
    ||> List.fold (fun (partChunk, chunks) x -> 
            if chunks.Length < size then 
                (x :: partChunk), chunks
            else 
                [x], List.rev partChunk :: chunks)
    |> fun (partChunk, chunks) -> 
            List.rev partChunk :: chunks
            |> List.rev


/// Return the modal element(s) of a list. If there are multiple modals, return
/// all of them in the order they appear in the list.
let modals (lst: 'a list) : 'a list =
        List.groupBy id lst// group like elements together
        |> List.map (fun (el,grp) -> el, grp.Length)// transform each group to a pair of element and count
        |> List.groupBy snd // group by number of elements: first group after sorting is the modals
        |> List.sortByDescending snd // sort by number of elements. Keeps order of modals
        |> List.tryHead // not really needed, but deal nicely with the empty list case
        |> function | None -> [] // no elements
                    | Some (num,grp) -> grp |> List.map fst // return elements from the group

/// Return the element that occurs most times consecutively in a list of integers.
/// If there are multiple such return the lowest.
let maxRepeats (lst: int list) : int =
    // NB will fail if list is empty but that is sort-of expected from the specification
    ([],lst) // start with empty list and the list of integers
    ||> List.fold (fun state n ->
        match state with
        | (n', numRepeat) :: repeatCountL when n = n' -> (n',numRepeat+1) :: repeatCountL
        | repeatCountL -> (n,1) :: repeatCountL)
    |> List.sortBy (fun (n, numRepeats) -> numRepeats, -n) // sort by number of repeats, then highest number
    |> function | ((el,_) :: _) -> el // get the first element
                | _ -> failwith "Empty list"

/// A palindrome is a word that reads the same forwards and backwards. 
/// A palindromic value is one that is repeated in the list in reverse order:
/// e.g. [1;2;4;3;1] has 1, 4 values palindromic, 2,3 not palindromic.
/// Return the list of all non-palindromic values (without repeated values) in the order they
/// first appear in the list.
let nonPalindromic (lst: 'a list) : 'a list =
        List.zip lst (List.rev lst) // zip with reversed list
        |> List.filter (fun (x,xr) -> x <> xr)// non-palindromic values have fst el <> snd el
        |> List.map fst // original list values

/// Given a list of words, return a unique list of all non-empty proper substrings of the words that are also in the list.
/// A proper substring is one that is not the same as the original word.
/// NB - use method Contains: w.Contains(x) checks if string w contains string x
let subWords (lst: string list) : string list =
        lst
        |> List.filter (fun x -> lst |> List.exists (fun w -> w.Contains(x)))

/// A list of pairs determines a state machine. Each pair is a transition from one state to another.
/// The first element of the pair is the current state, the second element is the next state.
/// The sequence stops when the next state does not exist in the list of pairs.
/// The state machine starts at the given start state.
/// return the list of state machine states in the order they are visited.
/// If the state machine runs for more than maxClocks, return None
let runStateMachine (maxClocks: int) (start: 'a) (lst: ('a * 'a) list)  : 'a list option =
        let stateMap = lst |> Map.ofList // create map from list
        ( Some start,[0..maxClocks-1])
        ||> List.scan (fun stateOpt _ -> 
                        stateOpt
                        |> Option.bind (fun state -> Map.tryFind state stateMap)) // folder function that does state transition or returns None
        |> List.takeWhile Option.isSome // take while scan result list elements are Some x
        |> function | lst when lst.Length = maxClocks -> None
                    | lst -> List.map Option.get lst |> Some// get the values from the option list
        
/// Given a list of integers, return the list of all integers that are 
/// the sum of two other integers in the list.
/// The order of the list is the order the values appear in the list.
/// The list of integers may contain duplicates.
let sumOfTwo (lst: int list) : int list =
    let sumEls = // to make it much more time-efficient calculate this once first
        List.allPairs lst lst
        // optional - could filter hereto make pairs ordered or even generate pairs only once for
        // each pair of elements - saving 50% time. Not worth it in most cases!
        |> List.map (fun (a,b) -> a+b)
        |> List.distinct // remove duplicates - optional - but why not!
    lst
    |> List.filter (fun x -> List.contains x sumEls)

/// Create a map that maps each number from 1 to maxInt to the largest proper divisor of that number.
let createLargestDivisorMap (maxInt: int) =
    [1..maxInt]
    |> List.map (fun n -> 
        [1..n/2] // all possible divisors
        |> List.filter (fun d -> n % d = 0) // keep only divisors)
        |> List.max // largest divisor - there muts be at least one (1)
        |> fun d -> n,d) // pair result with n
    |> Map.ofList // create map from list

/// Given two maps, return a map that contains the union of the two maps.
/// duplicated keys should have the value from the first map.
let mapUnion (map1: Map<'a,'b>) (map2: Map<'a,'b>) : Map<'a,'b> =
    map1
    |> Map.toSeq
    |> Seq.fold (fun m (k,v) -> Map.add k v m) map2

/// Given a list of lists, encoded as a single list of options, where each sublist is terminated by None.
/// Thus [Some 1; Some 2; None; Some 3; Some 4; None] is [[1;2];[3;4]]
/// Return the list of lists
let splitList (lst: 'a option list) : 'a list list =
    // List.fold - state is pair of current list and previous list of lists
    // snd of state is previous list of lists
    failwithf "Not Implemented"

/// Given a list of lists, return the list of all values that are in an odd number of the sublists.
let oddSublists (lst: 'a list list) : 'a list =
    // use Sets. That allows Set.difference
    // List.fold over list of lists, keeping as state Set of elements that are currently in od number
    // use Set.difference to generate next state from previous state diferneced with current list converted to Set
    failwithf "Not Implemented"

/// Given a map defining the arrows in a directed graph, return the list of all nodes that are reachable 
/// from the start node.
/// Use Dijkstra's algorithm, or some variant with different end conditions, to find the reachable nodes.
let reachableNodes (start: 'a) (graph: Map<'a, 'a list>) : 'a list =
    // Dijkstra's algorithm - breadth fist search adding nodes each step
    // we need at most one step per node in the graph - 1
    ([start],[1..graph.Count-1])
    ||> List.fold (fun reachedSoFar _ -> 
        reachedSoFar
        |> List.collect (fun node -> Map.tryFind node graph |> Option.defaultValue []) // generate possible next state nodes from current state
        |> List.append reachedSoFar // add new nodes to the list of reached nodes
        |> List.distinct) // remove duplicates

/// Given a list of non-duplicate integers, return a list of lists of consecutive integers containing the same values.
/// You may assume the input list is sorted in ascending numerical order.
/// Example: [1;2;3;5;6;7;10] -> [[1;2;3];[5;6;7];[10]]
let consecutiveLists (lst: int list) : int list list =
    // Surprisingly, this is IMHO easiest to do without recursion or fold (type 1).
    // Can you see how?
    List.indexed lst
    |> List.groupBy (fun (index,number) -> number - index)
    |> List.map (fun (grp, grpLst) -> grpLst |> List.map snd)
    

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0 // return an integer exit code


