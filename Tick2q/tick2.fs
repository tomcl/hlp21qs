module Tick2

//---------------------------Tick2 PartA skeleton code-------------------------------//


module PartACase1 =
    () // dummy value to make submodule non-empty
    // Three record types, one data value of each type. Choose suitable names.

module PartACase2 =
    () // dummy value to make submodule non-empty
    // One record type, three data values of this type. Choose suitable names.

module PartACase3 =
    () // dummy value to make submodule non-empty
    // One record type, three data values of this type. Choose suitable names.

//---------------------------Tick2 PartB case 2 skeleton code-------------------------------//

module PartBCase2 =

    open PartACase2 // get unqualified access to Case 2 types and values

    /// Return as a Ok string the name of the correct classification for a student
    /// on given course with given mark.
    /// Return Error if course or mark are not possible (marks must be in range 100 - 0). 
    /// The error message should say what the problem in the data was.
    let classify (course: string) (mark: float) : Result<string,string> =
        failwithf "Not implemented yet"

//---------------------------Tick2 PartB case 3 skeleton code-------------------------------//

module PartBCase3 =

    open PartACase3 // get unqualified access to Case 3 types and values

    /// Return as a Ok string the name of the correct classification for a studen on given course with given mark.
    /// Return Error if course or mark are not possible (marks must be in range 100 - 0). The error message should say what the problem in the data was.
    let classify (course: string) (mark: float) : Result<string,string> =
        failwithf "Not implemented yet"

//------------------------------------Tick2 PartC skeleton code-----------------------------------//

module PartC =
    open PartACase3 // get unqualified access to Case 3 types and values
    open PartBCase3 // get unqualified access to classify function

    type Marks = float // simplified set of marks (just one mark) used for testing

    /// Return the total mark for a student used to determine classification. 
    /// marks:  constituent marks of student on given course.
    /// course: name of course student is on
    /// Return None if the course is not valid or any of the marks are
    /// outside the correct range 0 - 100.
    let markTotal (marks: Marks) (course: string) : float option =
        match course with
        | "MEng"  | "BEng" | "MSc" when marks <= 100.0 && marks >= 0.0 ->
            Some marks // in this case with only one mark, student total is just the mark!
        | _ -> None

    /// Return a number in the range 0 - 2.5 which determines how much, 
    /// based on constituent marks, a student's markTotal should 
    /// be uplifted if it is
    /// within the valid possible uplift range (0 - -2.5%) of the boundary.
    /// Return None if the mark is not in the uplift range of the boundary. 
    /// Return an error if boundary is not a valid boundary.

    let upliftFunc 
        (total: Marks) 
        (markTotal: Marks -> string -> float Option)
        (boundaryMark: float)
        (boundary:string) 
        (course: string)
            : Result<float option, string> =

        failwithf "Not Implemented" // do not change - implementation not required

    /// Return the student classification, or an error message if there is
    /// an error in the data.
    let classifyAndUplift 
        (course: string) 
        (marks: Marks)
        (markTotal: Marks -> string -> float option) // made a parameter here for flexibility
        (upliftFunc: Marks -> string -> Result<float option,string>)
                : Result<string,string> =
        // This function needs the marks components (marks) as well as the overall mark computed from markTotal
        // Note that even if marks are a single float markTotal can return None, which must be dealt with.
        // in this case marks is not needed because it contains no extra info but in the general case it is
        // needed because upliftFunc depends on components of marks
        failwithf "Not implemented" // replace by your code ()

        // This illustrative code ignores the possible None and Error returns from
        // markTotal and upliftFunc.
        // Your code must cope with these, and work out which is the 
        // relevant boundary, and what is its mark.
        // let total = markTotal marks course
        // let effectiveMark = total + upliftFunc total boundaryMark boundaryName
        // let className = classify course effectiveMark
        // className

//------------------------------Simple test data and functions---------------------------------//
module TestClassify =
    /// test data comaptible with the Tick 2 problem
    let classifyUnitTests = [
        "MEng",75.0, Ok "First"
        "MSc", 75.0,Ok "Distinction"
        "BEng", 75.0, Ok "First"
        "MEng",65.0, Ok "UpperSecond"
        "MSc", 65.0, Ok "Merit"
        "BEng", 65.0, Ok "UpperSecond"        
        "MEng",55.0, Ok "LowerSecond"
        "MSc", 55.0, Ok "Pass"
        "BEng", 55.0, Ok "LowerSecond"        
        "MEng",45.0, Ok "Fail"
        "MSc", 45.0, Ok "Fail"
        "BEng", 45.0, Ok "Third"
        "BEng", 35.0, Ok "Fail"        
    ]

    let runClassifyTests unitTests classify testName =
        unitTests
        |> List.map (fun (data as (course,mark,_)) -> classify course mark, data)
        |> List.filter (fun (actualClass, (_,_,className)) -> actualClass <> className)
        |> function 
            | [] -> printfn $"all '{testName}' tests passed."
            | fails -> 
                fails 
                |> List.iter (fun (actual, (course,mark,className)) 
                                -> printfn $"Test Failed: {course}, {mark}, expected className={className}, \
                                          actual className={actual}")


//-------------------------------------------------------------------------------------------//
//---------------------------------Run Part B tests------------------------------------------//
//-------------------------------------------------------------------------------------------//

open TestClassify
let runTests() =
    runClassifyTests classifyUnitTests PartBCase2.classify "Case2"
    runClassifyTests classifyUnitTests PartBCase3.classify "Case3"


//-------------------------------------------------------------------------------------------//
//---------------------------------Tick2 Part X Skeleton code--------------------------------//
//-------------------------------------------------------------------------------------------//
module PartX =
    type Lens<'A,'B> = ('A -> 'B) * ('B -> 'A -> 'A)

    let lensMap (lens: Lens<'A,'B>) (f: 'B -> 'B) (a: 'A) =       
        (fst lens a |> f |> snd lens) a

    let mapCAndB (lensC: Lens<'A,'C>) (lensB: Lens<'A,'B>) (fc:'C->'C) (fb: 'B->'B) =
        lensMap lensC fc >> lensMap lensB fb

    let combineLens (l1: Lens<'A,'B>) (l2: Lens<'B,'C>) : Lens<'A,'C> =
        failwithf "not implemented yet" // replace with your definition
