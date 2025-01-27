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
        failwithf "Not Implemented"

/// A palindrome is a word that reads the same forwards and backwards. 
/// A palindromic value is one that is repeated in the list in reverse order:
/// e.g. [1;2;4;3;1] has 1, 4 values palindromic, 2,3 not palindromic.
/// Return the list of all non-palindromic values (without repeated values) in the order they
/// first appear in the list.
let nonPalindromic (lst: 'a list) : 'a list =
        failwithf "Not Implemented"

/// Given a list of words, return a unique list of all non-empty proper substrings of the words that are in the list.
/// A proper substring is one that is not the same as the original word.
/// NB - use method Contains: w.Contains(x) checks if string w contains string x
let subWords (lst: string list) : 'a list =
        failwithf "Not Implemented"

/// A list of pairs determines a state machine. Each pair is a transition from one state to another.
/// The first element of the pair is the current state, the second element is the next state.
/// The sequence stops when the next state does not exist in the list of pairs.
/// The state machine starts at the given start state.
/// return the list of state machine states in the order they are visited.
/// If the state machine runs for more than maxClocks, return None
let runStateMachine (maxClocks: int) (start: 'a) (lst: ('a * 'b) list)  : 'a list option =
        failwithf "Not Implemented"
        
/// Given a list of integers, return the list of all integers that are 
/// the sum of two other integers in the list.
/// The order of the list is the order the values appear in the list.
/// The list of integers may contain duplicates.
let sumOfTwo (lst: int list) : int list =
    failwithf "Not Implemented"

/// Create a map that maps each number from 1 to maxInt to the largest divisor of that number.
let createLargestDivisorMap (maxInt: int) =
    failwithf "Not Implemented"

/// Given two maps, return a map that contains the union of the two maps.
/// duplicated keys should have the value from the first map.
let mapUnion (map1: Map<'a,'b>) (map2: Map<'a,'b>) : Map<'a,'b> =
    failwithf "Not Implemented"

/// Given a list of lists, encoded as a single list of options, where each sublist is terminated by None.
/// Thus [Some 1; Some 2; None; Some 3; Some 4; None] is [[1;2];[3;4]]
/// Return the list of lists
let splitList (lst: 'a option list) : 'a list list =
    failwithf "Not Implemented"

/// Given a list of lists, return the list of all values that are in an odd number of the sublists.
let oddSublists (lst: 'a list list) : 'a list =
    failwithf "Not Implemented"

/// Given a map defining the arrows in a directed graph, return the list of all nodes that are reachable 
/// from the start node.
/// Use Dijkstra's algorithm, or some variant with different end conditions, to find the reachable nodes.
let reachableNodes (start: 'a) (graph: Map<'a, 'a list>) : 'a list =
    failwithf "Not Implemented"

/// Given a list of non-duplicate integers, return a list of lists of consecutive integers containing the same values.
/// You may assume the input list is sorted in ascending numerical order.
/// Example: [1;2;3;5;6;7;10] -> [[1;2;3];[5;6;7];[10]]
let consecutiveLists (lst: int list) : int list list =
    // Surprisingly, this is IMHO easiest to do without recursion or fold (type 1).
    // Can you see how?
    failwithf "Not Implemented"
    

[<EntryPoint>]
let main argv =
    printfn "Hello World"
    0 // return an integer exit code


