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


//-----------------------------------------------------------------------------------------//
//---------------------------------TICK 2 TESTS--------------------------------------------//

open Tick2
open EETypes
open StudentGen
open System.IO

(*
    These tests are provided as is in case you want to use them to test your functions.
    They are not necessarily complete, but will catch obvious errors.
    Functions should be written so that their correctness is "obvious" from the code, where that is possible.
    Part E has no tests as it is not required for full marks.
*)

// Function to read from a file
let readFromFile filePath =
    File.ReadAllText(filePath)

// Function to write to a file
let writeToFile filePath content =
    File.WriteAllText(filePath, content)

// Return the minimum (key,value) map element in standard lexicographic ordering as a string
// Fails if the Map is empty (maybe this should be corrected)
let stringOfLowestMapElement map =
    map 
    |> Map.toList 
    |> List.minBy fst 
    |> sprintf "%A"

// The answer code modified to output to files:
let rng = System.Random 0
let test_student = (getRandomStudentList 0 1)[0]
let test_options = studentOptionsPartA rng test_student
let test_eedata = getValidEEdata 0 25

// Read strings to compare with expected from Tick2 function data
let peopleFirst = stringOfLowestMapElement test_eedata.People
let curriculumFirst = stringOfLowestMapElement test_eedata.Curriculum
let optionsFirst = stringOfLowestMapElement test_eedata.Options
let test_groups = groupStudents moduleGroupSizes test_eedata

// Now, let's compare the outputs with the contents of the files
let compareOutputs () =
    // Read the contents of the files
    let expected_options = readFromFile "test_options.txt"
    let expected_people_first = readFromFile "people_first.txt"
    let expected_curriculum_first = readFromFile "curriculum_first.txt"
    let expected_options_first = readFromFile "options_first.txt"
    let expected_groups_first = readFromFile "test_groups.txt"

    // Run the functions
    let student_options = studentOptionsPartA rng test_student
    let people_first = stringOfLowestMapElement test_eedata.People
    let curriculum_first = stringOfLowestMapElement test_eedata.Curriculum
    let options_first = stringOfLowestMapElement test_eedata.Options

    let groups_first = 
        match groupStudents moduleGroupSizes test_eedata with
        | Ok inner -> stringOfLowestMapElement inner
        | Error error -> sprintf "%A" error

    // Compare the outputs with the contents of the files
    let compareContent name expected actual =
        if expected = actual then
            printfn "%s: Success!" name
        else
            printfn "%s: Failed!\nExpected: %s\nActual: %s" name expected actual

    compareContent "studentOptionsPartA" (sprintf "%A" student_options) expected_options
    compareContent "People First Element" people_first expected_people_first
    compareContent "Curriculum First Element" curriculum_first expected_curriculum_first
    compareContent "Options First Element" options_first expected_options_first
    compareContent "Groups First Element" groups_first expected_groups_first

// Call the compareOutputs function to run the comparison
compareOutputs()
