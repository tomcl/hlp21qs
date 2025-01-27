module Types

/// Screen is a 2D map of maps of characters
/// Screen[Y][X] = char
/// Empty rows or columns are not stored, and  are assumed to be spaces
type ScreenModel = Map<int, Map<int, char>>


type Model<'MODEL> = {
    /// F# 2D map of chars with ConsoleCanvas implementation
    Screen: ScreenModel * ConsoleRenderer.ConsoleCanvas
    /// Next screen to transition to
    NextScreen: ScreenModel
    /// Width of the screen in characters
    Width: int
    /// Height of the screen in characters
    Height: int
    /// User State
    UModel: 'MODEL
    }

