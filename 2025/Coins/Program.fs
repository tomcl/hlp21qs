// coinChange coins amount
// Given a set of coin denominations, how many ways can you make change for a given amount?
// Example: coinChange [1; 2; 5] 5 = 4
// If amount = 0 there is one way to make change (no coins).
// If amount < 0 there is no way to make change.
// If coins is empty and amount is non-zero there is no way to make change.
// Otherwise, there are two ways to make change: use the first coin or don't use the first coin.
//   - If you use the first coin, the amount is reduced by the value of the first coin.
//   - If you don't use the first coin, the amount is unchanged and the first coin is removed from the list of coins.

let rec coinChange (coins: int list) (amount: int) : int =
    match coins, amount with
    | _ , 0 -> 
        1
    | _ , a when a < 0 -> 
        0
    | [] , _ -> // NB here we have a non-zero amount
        0
    | c::cs , _ -> 
        coinChange coins (amount - c) + coinChange cs amount

// NOTES
// 1. this implementation is correct from inspection because it exactly follows the inductive specification of the problem.
// 2. folding lines after the -> is optional but in many cases makes the function more readable by lining up the case expressions.
// 3. The match expression is a powerful feature of F# that allows you to match a value against a set of patterns.
// 4. The second and third cases cannot be combined into one 
//    | _, a | [], a -> because 'when' clauses cannot be used with combined patterns.
// 5. Note that :: is infix notation for the  List.Cons operator, which can also be used in patterns.
// 6. The match expression is exhaustive, meaning that all possible cases are covered. If not, 
//   the match expression would be underlined in green. It is bad practice to ignore this warning, even when you know it is ok.
//   better practice is to remove the warning by adding  a final case: 
//      | _ -> failwithf "What? Can't happen". 
//   No more is needed if the error is locally not possible.


[<EntryPoint>]
let main argv =
    let coins = [1 ; 2 ; 5]
    let amount = 5
    let result = coinChange coins amount
    printfn $"coinChange %A{coins} {amount} = {result}\n\n"
    let tstChange = coinChange [1 ; 2 ; 5 ; 10] 100
    if coinChange [1 ; 2 ; 5 ; 10] 100 <> 2156 then
        printfn $"ERROR: coinChange [1 ; 2 ; 5 ; 10] 100:\nExpected: {2156}\nActual:    {tstChange}."
    else
        printfn "Test PASSED"
    0 // return an integer exit code
