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

// Function to read from a file
let readFromFile filePath =
    File.ReadAllText(filePath)

// Function to write to a file
let writeToFile filePath content =
    File.WriteAllText(filePath, content)

// Function to convert a map to a sorted list and return the first element as a string
let mapToSortedListAndFirstElement map =
    map 
    |> Map.toList 
    |> List.sortBy fst 
    |> List.head 
    |> sprintf "%A"

// Your existing code modified to output to files
let rng = System.Random 0
let test_student = (getRandomStudentList 0 1)[0]
let test_options = studentOptionsPartA rng test_student

let test_eedata = getValidEEdata 0 25

// Convert People, Curriculum, and Options maps to sorted lists and write the first element to files
let peopleFirst = mapToSortedListAndFirstElement test_eedata.People

let curriculumFirst = mapToSortedListAndFirstElement test_eedata.Curriculum

let optionsFirst = mapToSortedListAndFirstElement test_eedata.Options

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
    let people_first = mapToSortedListAndFirstElement test_eedata.People
    let curriculum_first = mapToSortedListAndFirstElement test_eedata.Curriculum
    let options_first = mapToSortedListAndFirstElement test_eedata.Options

    let groups_first = 
        match groupStudents moduleGroupSizes test_eedata with
        | Ok inner -> mapToSortedListAndFirstElement inner
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
