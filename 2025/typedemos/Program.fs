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

let q1 x = if x = 2 then "two" else "not two"

let q2(a,b) = a b

let q3 a b c = a b c

let q4 a b = (a b) b

let rec q5 a b = if a = 0 then b else q5 (a-1) (b+1.0)

let q6 x = [x] @ ["aa"]

let q8 f l = if l = [] then 0 else f (List.head l)

let q9 f g h x = f (g (h x))

let q10 x y =
    let g x = x,x
    let f a (c:float) = a + int (c / 3.0)
    f (fst (g 0)) (snd (g 1.0))

let q11 x  : int list * float list =
    let l1 = List.map x [1;2;3]
    let l2 = List.map x [1.0;2.0;3.0]
    l1,l2

