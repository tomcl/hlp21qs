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
let chunkBySize (size: int) (lst: 'a list) : 'a list list=
        // List.indexed
        // |> List.groupBy // group by index / size
        // NB have to use index here since definition of chunkBySize does this!
        failwithf "Not Implemented"

/// Return the modal element(s) of a list. If there are multiple modals, return
/// all of them in the order they appear in the list.
let modals (lst: 'a list) : 'a list =
        List.groupBy id // group like elements together
        List.map // transform each group to a pair of element and count
        List.sortBy
        List.head
        failwithf "Not Implemented"

/// Return the element that occurs most times in a list of integers.
/// If there are multiple such return the lowest.
let maxRepeats (lst: int list) : int =
        List.scan //- current state is repeat number paired with current integer
        List.sortBy //- sort on repeat number paired with - integer
        List.head
        failwithf "Not Implemented"

/// A palindrome is a word that reads the same forwards and backwards. 
/// A palindromic value is one that is repeated in the list in reverse order:
/// e.g. [1;2;4;3;1] has 1, 4 values palindromic, 2,3 not palindromic.
/// Return the list of all non-palindromic values (without repeated values) in the order they
/// first appear in the list.
let nonPalindromic (lst: 'a list) : 'a list =
        let rLst = List.rev lst
        List.zip lst rLst
        List.filter // non-palindromic
        List.map fst // origianl lits values
        failwithf "Not Implemented"

/// Given a list of words, return a unique list of all non-empty proper substrings of the words that are in the list.
/// A proper substring is one that is not the same as the original word.
/// NB - use method Contains: w.Contains(x) checks if string w contains string x
let subWords (lst: string list) : 'a list =
        List.filter // filter (fun x -> List.exists (fun w -> w.contains(x)) //the word is contained in some other word in the list
        failwithf "Not Implemented"

/// A list of pairs determines a state machine. Each pair is a transition from one state to another.
/// The first element of the pair is the current state, the second element is the next state.
/// The sequence stops when the next state does not exist in the list of pairs.
/// The state machine starts at the given start state.
/// return the list of state machine states in the order they are visited.
/// If the state machine runs for more than maxClocks, return None
let runStateMachine (maxClocks: int) (start: 'a) (lst: ('a * 'b) list)  : 'a list option =
        ( start,[0..maxClocks-1])
        ||> List.scan // Option.map folder function that does state transition or returns None
        |> List.takeWhile // take while scan result list elements are not None
        |> function | [] -> None, 
                    | lst -> List.map Option.get lst // get the values from the option list


        failwithf "Not Implemented"
        
/// Given a list of integers, return the list of all integers that are 
/// the sum of two other integers in the list.
/// The order of the list is the order the values appear in the list.
/// The list of integers may contain duplicates.
let sumOfTwo (lst: int list) : int list =
    let sumEls = // to make it much more time-efficient calculate this first
        List.allPairs lst lst
        ||> List.map (+)
        |> List.distinct // remove duplicates - optional
    lst
    |> List.filter (fun x -> List.exists (fun y -> x = y) sumEls)
    failwithf "Not Implemented"

/// Create a map that maps each number from 1 to maxInt to the largest divisor of that number.
let createLargestDivisorMap (maxInt: int) =
    [1..MaxInt]
    |> (fun n -> List.filter // keep only divisors /pair result with number)
    |> List.map // return max divisor list paired with number
    |> Map.ofList // create map from list
    failwithf "Not Implemented"

/// Given two maps, return a map that contains the union of the two maps.
/// duplicated keys should have the value from the first map.
let mapUnion (map1: Map<'a,'b>) (map2: Map<'a,'b>) : Map<'a,'b> =
    // start with map2
    // Map.fold over map1 adding to map2
    failwithf "Not Implemented"

/// Given a list of lists, encoded as a single list of options, where each sublist is terminated by None.
/// Thus [Some 1; Some 2; None; Some 3; Some 4; None] is [[1;2];[3;4]]
/// Return the list of lists
let splitList (lst: 'a option list) : 'a list list =
    // List.fold - state is pair of current list and previous list of lists
    // snd of state is previous lits of lists
    failwithf "Not Implemented"

/// Given a list of lists, return the list of all values that are in an odd number of the sublists.
let oddSublists (lst: 'a list list) : 'a list =
    // use Sets. That allows Set.difference
    // List.fold over lits of lists, keeping as state Set of elements that are currently in od number
    // use Set.difference to generate next state from previous state diferneced with current list converted to Set
    failwithf "Not Implemented"

/// Given a map defining the arrows in a directed graph, return the list of all nodes that are reachable 
/// from the start node.
/// Use Dijkstra's algorithm, or some variant with different end conditions, to find the reachable nodes.
let reachableNodes (start: 'a) (graph: Map<'a, 'a list>) : 'a list =
    // Dijkstra's algorithm
    List.fold over list of nodes, since path cannot be longer than that
    State is list of nodes reached so far
    Folder:
        List.collect - generate possible next state nodes from current state
        |> List.distinct // remove duplicates
    failwithf "Not Implemented"

/// Given a list of non-duplicate integers, return a list of lists of consecutive integers containing the same values.
/// You may assume the input list is sorted in ascending numerical order.
/// Example: [1;2;3;5;6;7;10] -> [[1;2;3];[5;6;7];[10]]
let consecutiveLists (lst: int list) : int list list =
    // Surprisingly, this is IMHO easiest to do without recursion or fold (type 1).
    // Can you see how?
    lst
    |> List.indexed
    |> List.groupBy (number - index)
    |> List.Map // (remove group value, and (another List.map) index from list of elements)

    failwithf "Not Implemented"
    

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0 // return an integer exit code


