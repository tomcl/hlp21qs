module Symbol
open Fable.React
open Fable.React.Props
open Browser
open Elmish
open Elmish.React
open Helpers
open CommonTypes

type Msg = Unit // no messages needed

type Model = Unit // No model needed

//-------------------------Helper functions for Tick 3-------------------------------------//

// Helper functions that are useful should normally be put into Helpers module
// For Tick3 only put Helper functions here for ease of assessment and feedback
// The obvious helpers to simplify solution are those that create and manipulate SVG elements.
// SVG boilerplate should be greatly reduced in a well-written system.

let posOf x y = {X=x;Y=y} // helper

// add your own functions as needed


//-----------------------------------------------------------------------------------------//


/// write this for Tick3 using your modified ComponentType
/// you may add to type definition in CommonTypes
let makeBusDecoderComponent (pos:XYPos) (w: int) (a: int) (n: int) = failwithf "Not implemented"

/// demo function - not needed for Tick3 answer
let makeDummyComponent (pos: XYPos): Component =
    { 
        X = int pos.X
        Y = int pos.Y
        W = 0 // dummy
        H = 0 // dummy
        Type = Not // dummy
    }

//-----------------------Elmish functions with no content in Tick3----------------------//

/// For this program init() generates the required result
let init () =
    (), Cmd.none

/// update function does nothing!
let update (msg : Msg) (model : Model): Model*Cmd<'a>  =
    match msg with
    | () -> model, Cmd.none // do nothing if we receive the (only) message

//----------------------------View Function for Symbol----------------------------//

/// Tick3 answer
let busDecoderView (comp: Component) = 
    failwithf "Not implemented"

/// demo function can be deleted
let busDecoderViewDummy (comp: Component) = 
    let fX = float comp.X
    let fY = float comp.Y 
    // in real code w,a,n would come from the component, but as the busDecoder case is not yet written this
    // is a workaround compatible with the dummy components
    let w,a,n = if fX < 100. then (3,0,8) else (4,3,5) // workaround
    //
    // This code demonstrates svg transformations, not needed for Tick3 but useful.
    // The elmish react syntax here uses CSS style transforms, not SVG attribute transforms. They are different.
    // In addition, svg elements transform under css differently from html.
    // See https://css-tricks.com/transforms-on-svg-elements/ for details if needed.
    //
    let scaleFactor=1.0 // to demonstrate svg scaling
    let rotation=0 // to demonstrate svg rotation (in degrees)
    g   [ Style [ 
            // the transform here does rotation, scaling, and translation
            // the rotation and scaling happens with TransformOrigin as fixed point first
            TransformOrigin "0px 50px" // so that rotation is around centre of line
            Transform (sprintf "translate(%fpx,%fpx) rotate(%ddeg) scale(%f) " fX fY rotation scaleFactor )
            ]
        
        ]  // g optional attributes in first list
        // use g svg element (SVG equivalent of div) to group any number of ReactElements into one.
        // use transform with scale and/or translate and/or rotate to transform group
        [
            polygon [ // a demo svg polygon triangle
                SVGAttr.Points "10,10 900,900 10,900"
                SVGAttr.StrokeWidth "5px"
                SVGAttr.Stroke "Black"
                SVGAttr.FillOpacity 0.1
                SVGAttr.Fill "Blue"] []
            line [X1 0.; Y1 0.; X2 0.; Y2 (100.) ; Style [Stroke "Black"]] [
             // child elements of line do not display since children of svg are dom elements
             // and svg will only display on svg canvas, not in dom.
             // hence this is not used
            ]
            text [ // a demo text svg element
                X 0.; 
                Y 100.; 
                Style [
                    TextAnchor "middle" // left/right/middle: horizontal algnment vs (X,Y)
                    DominantBaseline "hanging" // auto/middle/hanging: vertical alignment vs (X,Y)
                    FontSize "10px"
                    FontWeight "Bold"
                    Fill "Blue" // demo font color
                ]
            ] [str <| sprintf "X=%.0f Y=%.0f" fX fY] // child of text element is text to display
    ]
       


/// View function - in this case view is independent of model
let view (model : Model) (dispatch : Msg -> unit) =    
    [   // change for Tick3 answer
        makeDummyComponent {X=100.; Y=20.} // for Tick 3 two components
        makeDummyComponent {X=200.; Y=20.} 
    ] 
    |> List.map busDecoderViewDummy // change for Tick3 answer
    |> (fun svgEls -> 
        svg [
            Style [
                Border "3px solid green"
                Height 200.
                Width 300.   
            ]
        ]   svgEls )


type ValidateError =
   | WIsInvalid // ignoring a,n
   | AIsInvalid // for given w, ignoring n
   | NIsInvalid // for given a,w

/// Tick3 answer
let busDecoderValidate (comp:Component) : Result<Component, ValidateError*string> =
    failwithf "Not implemented"
    



    


