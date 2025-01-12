module Tick2
open EETypes

// Assessment of Tick 2 will be 100% if passed.
// To pass the tick > 50% of Parts A - D must be correctly implemented.
// Part E should be attempted by students who have completed Parts A-D.
// Part X1 and X2 are for reflection only and must be discussed in the interview, but are not otherwise assessed.
// Students competent in material taught in weeks 1-4 should be able to complete all of Parts A-D of this tick.

//-------------------Answers should be written in this file replacing failwithf statements by implementations--------------//
//-------------------Any number of additional helper functions may be defined at top level in this file--------------------//
//-------------------Add code to run and test your functions in Program.fs-------------------------------------------------//
//-------------------Rigorous testing is not required for this Tick--------------------------------------------------------//

// Part A - implement the function below

/// Generate a list of random module codes for a student obeying the rules:
/// - The student must take three modules.
/// - The student registration muts be in the OptionalFor list of each taken module.
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
/// - For module m The number of students in each group must be equal to groupSize m or groupSize (m+1).
/// - The smaller groups must be formed from the students with higher Course marks.
/// - Each group must be formed from students with marks as close together as possible
/// - Let r be the return value. If the function succeeds r[m][p] is a group number that defines the group of student p in module m.
/// - Within a given module the group numbers must be contiguous from 1 to the number of groups.
/// - If the function fails a list of the modules that fail should be returned.
let groupStudents 
        (groupSize: Map<ModuleCode,int>) 
        (data: EEData) : 
            Result<Map<ModuleCode,Map<Person,int>>,ModuleCode list> =
    (*
    Note that N items divided into G groups of size S or S+1 will have N / G = S (integer division).
    From this we can see that the number of groups of size S + 1 will be N % G, and the number of groups of size S will be G - N % G.
    In answering this question try to use indexes as little as possible, and use the functions from the List module.    
    *)
    failwithf "Not Implemented"

// Part E - Optional. Implement the fuction below.

/// - Divide each module's students into groups as for groupStudnents above, except that the groups must be formed so that the average 
/// - CourseMark of each group is as close as possible to the average CourseMark of the module's students
/// - Choose the student partition for which the sum of the squares of the differences between the group averages and the module average is minimised.
let groupStudentsByEqualExamAverage 
        (groupSize: Map<ModuleCode,int>) 
        (data: EEData) : 
            Result<Map<ModuleCode,Map<Person,int>>,ModuleCode list> =
    (*
        The imlementation required here is to enumerate all possible partitions of the students into groups satisfying the group size requirement, 
        and to choose the one that minimises the sum of the squares of the differences between the group averages and the module average of CourseMark.

        This is a combinatorial problem, and the number of possible partitions is very large. Make sure that your solution does not
        enumerate any partition more than once, because that makes the solution even slower.

        To obtain the required result you can (very concisely) use full recursion, with functions that return a list of lists of students,
        where the inner lists are the groups of students. 
        
        You can convert these lists of students to the required map of group numbers after you have found the best partition.

        Recursion rules to find all partitions of N students into G groups where N is a multiple of G and S = N / G.
        - If G = 1 return a list containing a list of all students.
        - If N = 0 return an empty list.
        - If N > 0 and G > 1, then choose a student p0 from the list of students.
        - find all subsets of students of size S that contain p0.
        - For each subset, find all partitions of the remaining students into G-1 groups.

        One recommended implementation is using two separate full recursive functions:
        - One to find student subsets of size S from a given set containing a given student.
        - One to solve the problem, by finding all partitions of the students into groups of size S.
        - Note that for this porblem using lists to represent sets of students is a better choice than Sets
          because the Set module has fewer library functions and less convenient syntax than the List module.

    *)
    failwithf "Not Implemented"


// the Parts below are for feedback only, be ready to discuss your answers in the interview.

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

