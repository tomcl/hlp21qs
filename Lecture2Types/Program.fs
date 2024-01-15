//----------------------------- Understanding Types--------------------------//

type MyType1 = int * string // MyType is a type abbreviation - not a new type!

let a: MyType1 = (1,"a")
let b: int * string = a
let a1 = a
let b1 = b

// My3Tuple is a parametrised type abbreviation. 
type My3Tuple<'a> = 'a * int * string 

// Parameter 'a must be given a definite value when it is used.
// The type of 'a can be inferred (with a warning)
let a3 : My3Tuple<float> = 1.0, 1, "a3"
let a4: My3Tuple<int list> = [], 1, "a3"

let l1: int list = [1]
let l1': list<int> = [2]
let l1'': List<'a> = [3]
let l1''' = l1 @ l1' @ l1''

//---------------------------------------------------------------------------//
//--------------------------Understanding Polymorphic Types------------------//
//---------------------------------------------------------------------------//

let f1 a b = a + b
let f1' = fun a -> (fun b -> a + b)

let ignore x = ()   

let pipe a b = a |> b // pipe a b = b a

let double = fun (n:int) -> n*2

let listDouble  = pipe double List.map 

let listDouble' = double |> List.map 

let listDouble'' = List.map double

let S1 f g x = (f x) (g x)

let S f g x = f x (g x) // brackets round f x are not necessary

// How to work out type of S?
(*
1. Start with unknown types of x, f, g, S
2. f, g, S must be functions : 'a -> 'b where we need to work out 'a and 'b separately for each of them
3a. type of x 'a
3b. type of g 'a -> 'b
3c. type of f: 'a -> 'b -> 'c 
3d. type of S:('a -> 'b -> 'c) -> ('a -> 'b) -> 'a  -> 'c




*)
//--------------------------------- id --------------------------------------//

let id1 x = x

let n1 = id 1 + id 2 + (int (id "10"))

let tuple = (id1 1, id1 "a")

//let testHMId1Para (id1: 'a -> 'a) =
//       (id1 1, id1 "a")


//------------------------------ mapTuple ------------------------------------//
       

let mapTuple f (a,b) = (f a, f b)
let m1 = mapTuple id (1,3)
let m2 = mapTuple id ("a", "b")
//let m3 = mapTuple id (1,"a")


//------------------------------ Arithmetic ----------------------------------//

let testArith a b = a + b


//let a2 = testArith 2 1

//let a1 = testArith 2.0  1.0


