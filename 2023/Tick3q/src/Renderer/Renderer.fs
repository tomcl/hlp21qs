module Renderer

    open Elmish
    open Elmish.React
    open Fable.Core
    open Fable.Core.JsInterop
    open Fable.React
    open Fable.React.Props
    open Browser
    
    open Helpers
    open Symbol
  
    
    
    
    // App
    Program.mkProgram init update view
    |> Program.withReactBatched "app"
    |> Program.run

