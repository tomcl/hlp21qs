//-----------------------------------------------------------------------------------------//
//---------------------------------TICK 2 TESTS--------------------------------------------//

open Tick2
open EETypes
open StudentGen
open System.IO



// Function to read from a file
let readFromFile filePath =
    File.ReadAllText(Path.Combine [|__SOURCE_DIRECTORY__; "tests"; filePath|] )

// Function to write to a file
let writeToFile filePath content =
    File.WriteAllText(Path.Combine [|__SOURCE_DIRECTORY__; "tests"; filePath|], content)

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
    let compareContent name (expected:string) (actual:string)  =
        /// needed because of issues with line endings writing to files and reading.
        let removeWhiteSpace (s:string) = s.Replace(" ","").Replace("\n","").Replace("\t","").Replace("\r","")
        if removeWhiteSpace expected = removeWhiteSpace actual then
            printfn "%s: Success!" name
        else
            printfn $"{expected.Length} -- {actual.Length}\n"
            printfn "%s: Failed!\nExpected: %s\nActual  : %s" name expected actual

    compareContent "studentOptionsPartA" (sprintf "%A" student_options) expected_options
    compareContent "People First Element" people_first expected_people_first
    compareContent "Curriculum First Element" curriculum_first expected_curriculum_first
    compareContent "Options First Element" options_first expected_options_first
    compareContent "Groups First Element" groups_first expected_groups_first

// Call the compareOutputs function to run the comparison
compareOutputs()
