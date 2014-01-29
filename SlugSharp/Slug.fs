namespace SlugSharp
open SlugTextSanitizer

type Slug = 
    static member CreateFrom(input) =
        input
            |> trim
            |> lowercase
            |> substituteUmlautsAndAmpersands
            |> replaceSpacesByHyphens
            |> removeIllegalCharacters
            |> mergeConsecutiveHyphens
            |> removeTrailingHyphens
