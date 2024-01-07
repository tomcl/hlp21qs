module Simulate

    /// Module contains code specific to Map implementation of environment.
    /// The functions for you to implement currently fail with appropriate 
    /// "_feature_ is not implemented!" messages.
    module MapEnvt =
        /// Type of a logic circuit input or output used in simulation
        /// This could be bool but creating a separate type gives better
        /// type protection and more reability.
        type Wire =
            | One
            | Zero
        
        /// The type of the update function used to determine a single variable's new
        /// value based on the old values of all the variables as in an environment.
        /// This type is the type of the domain-specific language (DSL) functions that determine the logic implemented.
        type Update = Environment -> Result<Wire,string>

        /// Type of a variable with its current state. State if a binary 0 or 1
        /// but encapsulated in a Result because if a variable update function references a 
        /// non-existent variable the value after update must be an error message.
        and Variable = {
            Name: string
            UpdateFn: Update
            Value: Result<Wire,string>
            }
        
        /// State of the system as a set of variables
        /// could be a List or a Map indexed by Variable name as here.
        and Environment = Map<string,Variable>

        /// Create an environment from a list of variables
        /// if the environment uses a list then this is just the identity function!
        let createEnv (initData: Variable list) : Environment = 
            failwithf "createEnv not implemented"

        /// Lookup a variable's value in envt from its name. If no such variable
        /// exists return an error saying that with useful diagnostic info.
        let varLookup (vName: string) (envt:Environment) : Result<Wire,string> =
            failwithf "varLookup not implemented"
        
        /// Given a variable name, a new variable value, and an envt, return an updated
        /// envt changing the variable value to the specified new value.
        let updateEnvt (vName: string) (newValue: Result<Wire,string>) (env: Environment) =
            failwithf "updateEnvt not implemented"
    
    //-------------------------------------------------------
    // Code here is independent of environment implementation
    //-------------------------------------------------------
    open MapEnvt
    /// Return environment after one clock: e.g. updating every variable with its new value based
    /// on its UpdateFn and the _old_ values of variables as given by env
    let step (envt:Environment) :Environment = 
        (envt, envt) ||> Map.fold (fun (env: Environment) (vName:string) (var:Variable) -> 
            updateEnvt vName (var.UpdateFn envt) env)

    /// Return environments after 0, 1, ... n applications of the step function
    let nSteps (envt:Environment) (n:uint32): Environment list =
        (envt, [1u..n]) 
        ||> List.scan (fun env _i -> step env)


    /// Logic Invertor function
    // Given here as example. Use the individual update functions to produce
    // a single correct one.
    let wInvert (f1:Update) : Update =
        fun env ->
            f1 env
            |> Result.map (function | Zero -> One | One -> Zero)
        
    /// Ok constant logic value
    let wConst (w: Wire): Update =
        failwithf "wconst not implemented"

    /// Logic AND function
    let wAnd (f1: Update) (f2:Update) : Update =
        failwithf "wAnd not implemented"
        
    /// Logic OR function
    let wOr (f1: Update) (f2:Update) : Update =
        failwithf "wOr not implemented"

    /// Logic XOR function
    let wXor (f1: Update) (f2:Update) : Update =
        failwithf "wXor not implemented"

    /// Logic value of a named variable
    /// this is the same as varLookup
    let wVar (name: string): Update = varLookup name

    /// Logic of a half adder
    /// outputs are carry,sum
    let halfAdder a b = wAnd a b, wXor a b

    /// Logic of a full adder
    /// outputs are carry,sum
    let fullAdder a b cin =
        let carry1, sum1 = halfAdder a b
        let carry2, sum2 = halfAdder sum1 cin
        wOr carry2 carry1, sum2

    // makeCkt elements make a list of variables that can be turned into
    // an environment containing the circuit with createEnv

    /// Makes a numBits adder on variables as specified.
    /// The necessary output variables: q and cout, are returned
    /// with suitable update functions
    /// input variables a[0..], b[0..] must be created elsewhere.
    let makeCktAdder numBits aPrefix bPrefix qPrefix cin cout = 
        let rec makeAdderBit n  =
            match n with 
            | n when n < 0 -> wVar cin, []
            | n ->
                let makeVExp (s:string) = wVar <| sprintf $"{s}{n}"
                let cin, lsOuts = makeAdderBit (n-1) 
                let coutN,sumN = fullAdder (makeVExp aPrefix) (makeVExp bPrefix) cin
                coutN, {Name = sprintf $"{qPrefix}{n}" ; UpdateFn=sumN; Value = Ok Zero} :: lsOuts
        let msCarry, qVars = makeAdderBit (numBits-1)
        {Name = cout; UpdateFn=msCarry; Value = Ok Zero} :: qVars
    
    /// Make a single variable as specified, returning it as a Variable list
    /// convenience function
    let makeCktVar name update value : Variable list =
        [{Name=name ; UpdateFn = update; Value = value}]

    /// Make a single variable with name of form prefixnumb and init value init,
    /// and update function bitUpdate
    let makeCktVarBit (namePrefix:string) (n:uint32) (bitUpdate:Update) (init: Wire)=
        makeCktVar (sprintf $"{namePrefix}{n}") bitUpdate (Ok init)

    /// Give variables named after namePrefix, with 0..numbits-1 after, constant values
    /// such that bit n gets the nth bit of data. Little-endian bit numbering as normal.
    let rec makeCktInputs (namePrefix:string) (numBits:uint32) (data:uint32) =
        match numBits with
        | 0u -> []
        | n ->
            let getBit bitNum data = 
                if data &&& (1u <<< int bitNum) <> 0u then One else Zero
            let dat = getBit (n - 1u) data
            let thisInput = makeCktVarBit namePrefix (n-1u)  (fun _env -> Ok dat) (getBit (n-1u) data)
            thisInput @ makeCktInputs namePrefix (n-1u) data


    
            
            

                




    
        


