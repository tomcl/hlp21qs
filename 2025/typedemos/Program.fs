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


