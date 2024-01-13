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
