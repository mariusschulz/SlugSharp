module public Slug
open System.Text.RegularExpressions
    
let private trim (input:string) = input.Trim()
let private lowercase (input:string) = input.ToLower()

let private replace (oldChar:string) (newChar:string) (input:string) =
    input.Replace(oldChar, newChar)

let private replaceAmpersands = replace "&" "and"
let private replaceSpacesByHyphens = replace " " "-"

let private removeIllegalCharacters input = Regex.Replace(input, "[^-a-z0-9]", "")

let private substituteUmlautsAndAmpersands input =
    let sanitizeChar = function
        | 'ä' -> "ae"
        | 'ö' -> "oe"
        | 'ü' -> "ue"
        | 'ß' -> "ss"
        | '&' -> "and"
        | c -> c.ToString()

    Seq.map sanitizeChar input |> String.concat ""

let private mergeConsecutiveHyphens input = Regex.Replace(input, "-{2,}", "-")

let private removeTrailingHyphens (input:string) = input.TrimEnd('-')

let CreateFrom input =
    input
        |> trim
        |> lowercase
        |> substituteUmlautsAndAmpersands
        |> replaceSpacesByHyphens
        |> removeIllegalCharacters
        |> mergeConsecutiveHyphens
        |> removeTrailingHyphens
