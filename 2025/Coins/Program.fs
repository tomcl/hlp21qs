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
    failwithf "Not implemented yet"

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
