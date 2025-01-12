// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

//-----------------------------------------------------------------------------------------//
//----------------------------------Type aliasses------------------------------------------//

type Tuple1 = int * int

type Tuple2 = int * int

let f1 (x : Tuple1) : Tuple2 = x // Tuple1 is the same as Tuple2

//-----------------------------------------------------------------------------------------//
//----------------------------------Named Record Types-------------------------------------//

type Rec1 = { A: int; B: int }

type Rec2 = { A: int; B: int }

//let f2 (r: Rec1) : Rec2 = r  // Rec1 is not the same as Rec2

//let f2' (r: Rec1) : Rec2 = {A=r.A; B=r.B} // Here the type of the function return value is inferred from the function definition


//-----------------------------------------------------------------------------------------//
//----------------------------Value Types Versus Parameter Types---------------------------//


let myId = fun x ->  x // NB this is identical to built-in id function

// let mixedTuple()  = myId "a", myId 2

(*
let mixedtuple1 (myId: 'a -> 'a)  = 
    myId "a", myId 2
*)


(*
let mixedtuple2()  = 
    let myId = fun x -> x 
    myId "a", myId 2
*)


(*
let mixedtuple3()  = 
    let myId = fun x -> x 
    myId "a", myId 2, myId
*)


let fL1 a b c =
    c
    |> List.filter a
    |> List.map b

let ex1 = fL1 (fun n -> n % 2 = 0) (fun n -> n * n) [1..10]

printfn $"ex1 = %A{ex1}\n"

let fL2 f g cL =
    cL
    |> List.map (fun x -> List.mapi f x)
    |> List.map g
    

let exOfReplicate = String.replicate 3 "ab"

printfn $"""example of String.replicate 3 "ab" = {exOfReplicate}\n"""

let exOfConcat = String.concat "." ["a";"b";"c"]

printfn $"""example of String.concat "." ["a";"b";"c"] = {exOfConcat}\n"""

let ex2 = fL2 String.replicate (String.concat "") [["a" ; "b" ; "c" ; "d"] ; ["1" ; "2" ; "3" ; "4"]]

printfn $"ex2 = %A{ex2}"