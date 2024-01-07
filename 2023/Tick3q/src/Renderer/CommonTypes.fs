module CommonTypes
    open Fable.Core

(*******************************************************************************************************************

TICK 3
------

This file contains the some of the core types currently used by Issie. For Tick 3 you need consider only a few of these types - most
are tagged as "not needed"

The real Issie types have been simplified here.

*********************************************************************************************************************)

    let draw2dCanvasWidth = 3000 // not relevant Tick 3
    let draw2dCanvasHeight = 2000 // not relevant Tick 3

   

    /// Name identified the LoadedComponent used.
    /// The labels define legends on symbol.
    /// Label strings are unique per CustomComponent.
    /// Multiple CustomComponent instances are differentiated by Component data.
    /// Not used in Tick 3
    type CustomComponentType = {
        Name: string
        // Tuples with (label * connection width).
        InputLabels: (string * int) list
        OutputLabels: (string * int) list 
    }

    /// Memory data for RAM,ROM - Not used in Tick 3
    type Memory = {
        // How many bits the address should have.
        // The memory will have 2^AddressWidth memory locations.
        AddressWidth : int 
        // How wide each memory word should be, in bits.
        WordWidth : int
        // Data is a Map of (address,location value>, where addresses and data are
        // 64 bit integers. This makes words longer than 64 bits not supported.
        // This can be changed by using strings or bigint instead of int64, but that is
        // less efficient.
        Data : Map<int64,int64>
    }

    // Types of specific components
    type ComponentType =
        | Input of BusWidth: int | Output of BusWidth: int | IOLabel 
        | BusSelection of OutputWidth: int * OutputLSBit: int
        | Constant of Width: int * ConstValue: int
        | Not | And | Or | Xor | Nand | Nor | Xnor |Decode4
        | Mux2 | Demux2
        | NbitsAdder of BusWidth: int
        | Custom of CustomComponentType // schematic sheet used as component
        | MergeWires | SplitWire of BusWidth: int // int is bus width
        // DFFE is a DFF with an enable signal.
        // No initial state for DFF or Register? Default 0.
        | DFF | DFFE | Register of BusWidth: int | RegisterE of BusWidth: int 
        | AsyncROM of Memory | ROM of Memory | RAM of Memory // memory is contents

    /// Component type (cut-down for Tick3)
    /// Id uniquely identifies the component within a sheet and is used by draw2d library.
    /// Label is optional descriptor displayed on schematic.
    type Component = {
        Type : ComponentType
        X : int // left side of component rectangle X position on canvas
        Y : int // top of component rectangle Y position on canvas
        H : int // Y value (height) of bounding box
        W : int // X value (width) of rectangle bounding box
    }

    
    //=======//
    // Other //
    //=======//

    type NumberBase = | Hex | Dec | Bin | SDec // not used in Tick 3

    /// Colors to highlight components
    /// Case name is used as HTML color name.
    /// See JSHelpers.getColorString
    /// lots of colors can be added, see https://www.w3schools.com/colors/colors_names.asp
    /// The Text() method converts it to the correct HTML string
    /// Where speed matters the color must be added as a case in the match statement
    type HighLightColor = Red | Blue | Yellow | Green | Orange | Grey
    with 
        member this.Text() = // the match statement is used for performance
            match this with
            | Red -> "Red"
            | Blue -> "Blue"
            | Yellow -> "Yellow"
            | Green -> "Green"
            | Grey -> "Grey"
            | c -> sprintf "%A" c
            
            

    // The next types are not strictly necessary, but help in understanding what is what.
    // Used consistently they provide type protection that greatly reduces coding errors
    // Not used in Tick 3
    /// SHA hash unique to a component
    [<Erase>]
    type ComponentId      = | ComponentId of string

    // Note used in Tick 3
    /// Human-readable name of component as displayed on sheet.
    /// For I/O/labelIO components a width indication eg (7:0) is also displayed, but NOT included here
    [<Erase>]
    type ComponentLabel   = | ComponentLabel of string
    

