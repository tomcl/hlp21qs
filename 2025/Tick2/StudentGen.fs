module StudentGen

open EETypes
open LibExtensions

module Constants =
    let numStudentOptions = 3



 

let splitOnChar (c:char) (s:string) = s.Split([|c|], System.StringSplitOptions.RemoveEmptyEntries)

let names = 
    """
        Kurt Harmon
        Marcia Lucas
        Irwin Little
        Estela Meadows
        Rosalie Campbell
        Pansy Mays
        Lino House
        Bianca Watkins
        Lowell Mercer
        Shon Hoffman
        Philip Cochran
        Lena Jackson
        Erwin Frey
        Theodore Brock
        Lorena Parsons
        Moises Lowery
        Leonard Huynh
        Kaitlin Terry
        Emanuel Green
        Kerri Gould
        Stanley Cox
        Tamika Vargas
        Zane Woods
        Viola Copeland
        Tanya Serrano
        Jacklyn Logan
        Leif Mills
        Chase Cole
        Reinaldo Gibson
        Hilario Mcdonald
        Roxanne Cobb
        Cornelia Alexander
        Calvin Mckee
        Paris Odonnell
        Ulysses Hebert
        Leon Morse
        Avery Stanley
        Irving Cantrell
        Coleen Humphrey
        Dannie Kaufman
        Jasmine Osborn
        Tracey Andrade
        Jayson Patel
        Dionne Rhodes
        Ali Larson
        Bryon Lamb
        Clara Pruitt
        Eunice Howell
        Walter Chambers
        Aurora Small
        Hai Huang
        Gayle Boone
        Joel Maxwell
        Rosario Bennett
        Alexandra Ray
        Tanya Walter
        Emmett Taylor
        Ophelia Mann
        Pat Mcneil
        Raquel Zimmerman
        Karl Sweeney
        Leo Savage
        Margarita Koch
        Kayla Solomon
        Hobert Terrell
        Rosemary Jenkins
        Sid Stevens
        Marcel Werner
        Fredrick Goodman
        Chong Mack
        Lacy Heath
        Robt Brooks
        Miriam Rose
        Glenn Thornton
        Iris Moon
        Lina Craig
        Eve Nguyen
        Santos Daniel
        Petra Cowan
        Alfreda Cobb
        Osvaldo Wolfe
        Keith Madden
        Shon Welch
        Lillie Hodges
        Francis Hughes
        Marcellus Nash
        Catherine Blevins
        Tanisha Escobar
        Ollie Lamb
        Austin Watson
        Nettie Mooney
        Hal Ellison
        Lou Little
        Antoinette Brown
        Antony Barron
        Burl Watkins
        Chelsea Robinson
        Natalia Burch
        Isaac Lawrence
        Leigh Simpson
        Martin Morrison
        Allison Perez
        Jon Booker
        Stacey Harris
        Jacinto Simpson
        Margo Villarreal
        Blake Dunn
        Byron Mcneil
        Tracy Mckee
        Brooke Christensen
        Carmela Bryant
        Grace White
        Johnathon Horton
        King Walsh
        Donnie Hebert
        Alfonzo Sutton
        Margarito Hunt
        Carmine Conway
        Damion Conner
        Leon Wyatt
        Cristina Pena
        Alberto Schultz
        Kurt Martinez
        Raul Knight
        Jarrod Lewis
        Reyes Orozco
        Candy Jordan
        Dwayne Osborn
        Evangeline Jensen
        Beth Wolfe
        Amos Riggs
        Margarita Kaiser
        Reynaldo Anderson
        Robyn Thornton
        Lisa Poole
        Kenton Goodman
        Lamont Hays
        April Duffy
        Cleo Ritter
        Gilberto Key
        Tonya Bridges
        Charles Miller
        Ulysses Mercado
        Jarod Frederick
        Michelle Logan
        Tia Howell
        Jan Carney
        Jamison Abbott
        Eileen Burke
        Lewis Daniel
        Virgilio Hines
        Aldo Rios
        Marissa Little
        Warren Mcmahon
        Flora Howe
        Otto Andrews
        Lenny Prince
        Connie Moses
        Bobby Cuevas
        Leanne Stuart
        Ike Pearson
        Dorian Knight
        Bruno Strickland
        Reed Murray
        Buck Davenport
        Rashad Key
        Neva Castillo
        Claude Gamble
        Harvey Briggs
        Kaitlin Torres
        Rudolph Webb
        Jamie Doyle
        Fermin Swanson
        Katina Simpson
        Romeo Orr
        Lynne Downs
        Margaret Marks
        Shirley Garza
        Sherri Stark
        Allen Logan
        Imogene Combs
        Monte Burch
        Marquita Conrad
        Gonzalo Coffey
        Rhoda Lin
        Kennith Santana
        Daisy Duffy
        Muriel Glenn
        Jeannine Hunt
        Theresa Alexander
        Norma Winters
        Rae Hall
        Frankie Mcdaniel
        Blair Cantu
        Eloise Larsen
        Deshawn Fleming
        Norberto Oneal
        Isabelle Owens
        Jospeh York
        Valeria Bentley"""

    |> splitOnChar '\n'
    |> Array.map (fun s -> s.Trim())
    |> Array.map (splitOnChar ' ') 
    |> Array.map (function | [|first; last|] -> first,last
                           | nameParts -> failwithf $"badly formatted student name: {nameParts}")
    |> Array.unzip
    |> fun (firsts, lasts) -> Array.distinct firsts, Array.distinct lasts
    |> fun (firsts, lasts) -> 
        let num = min firsts.Length lasts.Length
        (firsts[0..num-1], lasts[0..num-1])
    ||> Array.allPairs

/// Return list of numStudents distinct students names selected from firstNames, lastNames.
/// All items in the list are distinct, although first and last names individually are not.
/// Fail if the number of names required is too large.
/// Were very large numbrs needed, we should instead generate only the required number of names.
/// However performance is not an issue here.
let rec getStudentNameList (numStudents: int) =
    if numStudents > names.Length then 
        failwithf $"Too many students requested: {numStudents} > {names.Length}"
    else
        names[0..numStudents-1]
    |> Array.toList
    

let getRandomCIDList (numCIDs: int) =
    let CIDStart = 10000000
    let CIDEnd = CIDStart + 89999999
    let random = System.Random 0
    let randomCID() = random.Next(CIDStart, CIDEnd)
    List.init (2 * numCIDs + 1000) (fun _ -> randomCID())
    |> List.distinct
    |> List.take numCIDs


let getRandomStudentList (seed: int) (numStudents: int) : Person list =
    let random = System.Random seed
    let getRandomERegYr() =
       let listIndex = random.Next(eRegYrList.Length - 1)
       eRegYrList.[listIndex]
    List.zip (getRandomCIDList numStudents) (getStudentNameList numStudents)
    |> List.map (fun (cid, (firstName,lastName)) -> 
        {
            CID = $"%08d{cid}"
            Name = {|First = firstName; Last = lastName|}
            ERegYr = getRandomERegYr()
            CourseMarkSoFar = random.NextDouble() * 60. + 40.
        })

/// Generate a list of students with valid random module options.
/// isValid is a function that checks if the module options are valid for the student.
/// If the options are not valid, a new set of random options is generated.
/// rng is a random number generator, e.g. created with 'System.Random seed'.
let rec randomStudentOptions (rng: System.Random) (isValid: Person -> ModuleCode list -> bool) (person: Person): ModuleOptions =
    let randomModuleCode() =
       let listIndex = rng.Next(moduleList.Length - 1)
       moduleList.[listIndex].ModuleCode
    let options = List.init Constants.numStudentOptions (fun _ -> randomModuleCode())
    if isValid person options then
        {CID = person.CID; Options = options}
    else
        randomStudentOptions rng isValid person
    
    
