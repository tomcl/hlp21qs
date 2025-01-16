module Tick2
open LibExtensions
open EETypes

// Assessment of Tick 2 will be 100% if passed.
// To pass the tick > 2 of Parts A - D must be correctly implemented.
// Part E should be attempted by students who have completed Parts A-D and have time to spare. Attempting this 
// part even without success will mean the solution, when you get it, will be of more use to you.
// Part X1 and X2 are for reflection only and must be discussed in the interview, but are not otherwise assessed.
// Students competent in material taught in weeks 1-4 should be able to complete all of Parts A-D of this tick.

//-------------------Answers should be written in this file replacing failwithf statements by implementations--------------//
//-------------------Any number of additional helper functions may be defined at top level in this file--------------------//
//-------------------Add code to run and test your functions in Program.fs-------------------------------------------------//
//-------------------Rigorous testing is not required for this Tick--------------------------------------------------------//

// Part A - implement the function below

/// Generate a list of random module codes for a student obeying the rules:
/// - The student must take three modules.
/// - The student registration must be in the OptionalFor list of each taken module.
/// - The student must not take the same module twice.
/// - You must use StudentGen.randomStudentOptions, and pass it rng.
let studentOptionsPartA (rng: System.Random) (person: Person) : ModuleOptions =
    failwithf "Not Implemented"

// Part B - implement the function below

/// Same functionality as EETypes.randomStudentOptions, but not recursive.
/// Use LibExtensions.doWhile to implement this function.
let randomStudentOptionsNotRecursive 
        (rng: System.Random) 
        (isValid: Person -> ModuleCode list -> bool) 
        (person: Person): ModuleOptions
    = failwithf "Not Implemented"

// Part C - implement the function below

/// Generate data with random students and valid module options using functions from StudentGen and answer to Part A.
/// Use seed for both the student and the module option
let getValidEEdata (seed: int) (numStudents: int) : EEData =
    failwithf "Not Implemented"

// Part D - implement the function below

/// - Divide each module's students into groups.
/// - For module m The number of students in each group must be equal to groupSize[m] or groupSize[m] + 1.
/// - The smaller groups must be formed from the students with higher Course marks.
/// - Each group must be formed from students with marks as close together as possible
/// - If there are zero students in any module the function should fail with the failing module code list.
/// - Let r be the return value. If the function succeeds r[m][p] is a group number that defines the group of student p in module m.
/// - Within a given module the group numbers must be contiguous from 1 to the number of groups.
/// - The function fails if groupSize[m] is larger than the number of students in module m for any module,
/// - in which case it returns Error with the list of failing module codes.
let groupStudents 
        (groupSize: Map<ModuleCode,int>) 
        (data: EEData) : 
            Result<Map<ModuleCode,Map<Person,int>>,ModuleCode list> =
    (*
    Note that N items divided into G groups of size S or S+1 will have N / G = S (integer division).
    From this we can see that the number of groups of size S + 1 will be N % S, and the number of groups of size S will be N / S - N % S.
    In answering this question try to use indexes as little as possible, and use the functions from the List module.    
    *)
    failwithf "Not Implemented"

// Part E - Optional. Implement each the functions and subfunctions below. You may not be able to implement all: do what you can.
// Use additional helper functions are subfunctions at your discretion.

/// - Find the students in a module
/// - Helper function for groupStudentsByEqualExamAverage
let studentsOfModule (moduleCode: ModuleCode) (data: EEData) : Person list =
    failwithf "Not Implemented"

// The recusions to find all partitions without duplication are an all or nothing solution, either you get it right or you don't.
// Therefore, even if you can't work out how to do them, you should still attempt a function that returns the overall result
// when given as input a list of partitions. It should be obvious from inspection that the function works, subject to the input
// containing at least one partition.

/// - Partition each module's students into groups as for groupStudents above, with differences:
/// - All groups muts be size groupSize except (possibly) for the last group in each module, which may be of smaller than groupSize.
/// - The groups must be formed so that the average 
/// - CourseMark of each group is as close as possible to the average CourseMark of the module's students
/// - Find the student partition into groups as above for which the sum of the squares of the differences between the group averages and 
/// - the module average is minimised.
/// - You may assume that the number of students in each module is non-zero so that this operation cannot fail

let groupStudentsByEqualExamAverage 
        (groupSize: Map<ModuleCode,int>) 
        (data: EEData) : 
            Map<ModuleCode,Map<Person,int>> =
    (*
        The implementation required here is to enumerate all possible partitions of 
        the students into groups satisfying the group size requirement, 
        and to choose the one that minimises the sum of the squares of the 
        differences between the group averages and the module average of CourseMarkSoFar.

        This is a combinatorial problem, and the number of possible partitions is very large. 
        Make sure that your solution does not enumerate any partition more than once, 
        because that makes the solution even slower.

        To obtain the required result you can (very concisely) use full recursion, 
        with functions that return a list of list of lists of students, 
        where the inner lists are the groups of students making the parts of one partition.
        
        You can convert these lists of students to the required map of group numbers 
        after you have found the best partition.

        Even though mathematically the operations may require sets, we will use a list data structures throughout, for convenience.
    *)

    let rec findAll2PartPartitions (groupSize: int) (students: Person list) : (Person list * Person list) list =

        (*
        A. Recursion rule to find all distinct partitions of students into 2 parts where the first part has size S
        - suggested function signature:
        - findAll2PartPartitions : (int: groupSize) -> (students: Person list) -> (Person list * Person list) list // the first list in the tuple is of size groupSize.
             // the output could perhaps be a list of anonymous records

        - if groupSize is larger than or the same as the number of students return a single partition of length 2, 
        - Otherwise the first student in students must be either 1. in or 2. out of the first part of the partition.
        - find all length 2 partitions in each case recusrsively, add in the first students for case 1, and concatenate the results.
        *)
        failwithf "Not implemented"

    let rec findAllPartitions (groupSize: int) (students: Person list) : Person list list list =
        (*
        B. 
        Recursion rule to find all partitions of N students into groups of size S (with one group possibly smaller):
        - If the number of students is <= S, return a list containing a single list of all students.
        - 1. Otherwise find the set P of all distinct partitions of the students with two parts, where the first part contains S students
        - 2. For each (p,others) in P, recursively find all partitions of others into groups of size S.
        - 3. Concatenate all the results of step 2, and return the result.
        Note: these rules will generate many duplicate partitions. To avoid this, you can modify step 1. so that the first parts 
        of each partition in P all contain the first student in students. This will avoid duplicates, and will 
        not affect the correctness of the solution.
        *)

        failwithf "Not implemented"

    failwithf "groupStudentsByEqualExamAverage Not implemented"
  



// The Parts below are for feedback only, be ready to discuss your answers in the interview.

// Part X1 - Reflect on randomStudentOptions. 
// - Is it pure functional with no mutable data? 
// - Does it have side effects?
// - To what extent do its side effects make analysis of the function difficult?
// - Would refactoring the function without side effects make it easier to understand and analyse?

// Part X2 - Compare the function randomStudentOptions with the function randomStudentOptionsNotRecursive.
// - Which is easier to understand?
// - Would a real while loop, with mutable variables, be easier to understand in this case?
// - In general what is there any difference between doWhile and using a while loop with mutable variables?

// Part X3 (not assessed in any way) - Reflect on the extent to which CoPilot helped in doing this Tick.

