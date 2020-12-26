open TestLib
open Expecto
open Symbol





let validate w a n  = 
    makeBusDecoderComponent {X=0.;Y=0.} w a n
    |> busDecoderValidate



let invalidWIsInvalid (w:int,a:int, n:int) =
    match validate w a n with
    | Error( WIsInvalid,_) when w <= 0 -> true
    | _ when w <= 0 -> false
    | _ -> true

let invalidAIsInvalid (w:int,a:int, n:int) =
    match validate w a n with
    | Error( AIsInvalid,_) when w > 0 && (a < 0 || a >= w) -> true
    | _ when w > 0 && (a < 0 || a >= w )-> false
    | _ -> true

let invalidNIsInvalid (w:int,a:int, n:int) =
    match validate w a n with
    | Error( NIsInvalid,_) when w > 0 && a >= 0 && a < w && (n <= 0 || a+n > w) -> true
    | _ when  w > 0 && a >= 0 && a < w && (n <= 0 || a+n > w) -> false
    | _ -> true


let validWANIsValid (w:int,a:int, n:int) =
    match validate w a n with
    | Ok _ when w > 0 && a >=0 && n > 0 && a+n < w -> true
    | _ when w > 0 && a >=0 && n > 0 && a+n < w -> false
    | _ -> true

[<Tests>]
let simulatorTests =
    testList "busDecoderValidate Property Tests" [
        testProperty "Invalid w is Error(WIsInvalid,_)" invalidWIsInvalid
        testProperty "Invalid a is Error(AIsInvalid,_)" invalidAIsInvalid
        testProperty "Invalid n is Error(NIsInvalid,_)" invalidNIsInvalid
        testProperty "Valid w,a,n is Ok" validWANIsValid
    ]





[<EntryPoint>]
let main argv =

    TestLib.runTests ()
    System.Console.ReadKey() |> ignore
    0
