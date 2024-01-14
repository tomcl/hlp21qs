//----------------------------- Understanding Types--------------------------//

type MyType1 = int * string

let a: MyType1 = (1,"a")
let b: int * string = a
let a1 = a
let b1 = b

type My3Tuple<'a> = 'a * int * string

let a3 : My3Tuple<float> = 1.0, 1, "a3"
let a4: My3Tuple<int list> = [], 1, "a3"

let l1: int list = [1]
let l2: list<int> = [2]
let l3: List<int> = [3]
let l4 = l1 @ l2 @ l3

//---------------------------------------------------------------------------//
//--------------------------Understanding Polymorphic Types------------------//
//---------------------------------------------------------------------------//

let f1 a b = a + b
let f2 = fun a -> (fun b -> a + b)

let S' f g x = (f x) (g x)

let S f g x = f x (g x) // brackets round f x are not necessary

// How to work out type of S?
(*
1. Start with unknown types of x, f, g, S
2. f, g, S must be functions : 'a -> 'b where we need to work out 'a and 'b sepaartely for each of them
3a. type of x
3b. type of g
3c. type of f
3d. type of S




*)
//--------------------------------- id --------------------------------------//

//    let id1 x = x

//    let n1 = id 1 + id 2 + (int (id "10"))

//    let tuple = (id1 1, id1 "a")

//    let testHMId1Para (id1: 'a -> 'a) =
//        (id1 1, id1 "a")


//------------------------------ mapTuple ------------------------------------//
       

//    let mapTuple f (a,b) = (f a, f b)
//    let m1 = mapTuple id (1,3)
//    let m2 = mapTuple id ("a", "b")
//    let m3 = mapTuple id (1,"a")


//------------------------------ Arithmetic ----------------------------------//

//    let testArith a b = a + b


//    let a2 = testArith 2 1

//    let a1 = testArith 2.0  1.0

