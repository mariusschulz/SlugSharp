module internal SlugSharp.SlugTextSanitizer
open System.Text.RegularExpressions
    
let trim (input:string) = input.Trim()
let lowercase (input:string) = input.ToLower()

let replace (oldChar:string) (newChar:string) (input:string) =
    input.Replace(oldChar, newChar)

let replaceAmpersands input = replace ("&") ("and") input
let replaceSpacesByHyphens input = replace (" ") ("-") input

let removeIllegalCharacters input = Regex.Replace(input, "[^-a-z0-9]", "")

let substituteUmlautsAndAmpersands input =
    let sanitizeChar = function
        | 'ä' -> "ae"
        | 'ö' -> "oe"
        | 'ü' -> "ue"
        | 'ß' -> "ss"
        | '&' -> "and"
        | c -> c.ToString()

    String.concat "" (Seq.map sanitizeChar input)

let mergeConsecutiveHyphens input = Regex.Replace(input, "-{2,}", "-")

let removeTrailingHyphens (input:string) = input.TrimEnd('-')