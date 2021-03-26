// =============================================
// Convert a number to Roman numerals
// =============================================

(*

Use the "tally" system:

* Start with N copies of "I".
  Eg. The number 8 becomes "IIIIIIII"
* Replace "IIIII" with "V"
* Replace "VV" with "X"
* Replace "XXXXX"  with "L"
* Replace "LL" with "C"
* Replace "CCCCC"  with "D"
* Replace "DD" with "M"

*)


/// Helper to convert the built-in .NET library method
/// to a pipeable function
let replace (oldValue:string) (newValue:string) (inputStr:string) =
    inputStr.Replace(oldValue, newValue)



// Inline version
let toRomanNumerals number =
    String.replicate number "I"
    |> replace "IIIII" "V"  // partial application
    |> replace "VV" "X"
    |> replace "XXXXX" "L"
    |> replace "LL" "C"
    |> replace "CCCCC" "D"
    |> replace "DD" "M"



// test it
toRomanNumerals 12
toRomanNumerals 14
toRomanNumerals 1947


// test it on all the numbers up to 30
[1..30] |> List.map toRomanNumerals |> String.concat ","


// ======================================
// What about the special forms IV,IX,XC etc?
//
// ======================================

let toRomanNumerals_v2 number =
    String.replicate number "I"
    |> replace "IIIII" "V"
    |> replace "VV" "X"
    |> replace "XXXXX" "L"
    |> replace "LL" "C"
    |> replace "CCCCC" "D"
    |> replace "DD" "M"
    // TIP: additional special forms should be
    // done highest to lowest
    |> replace "DCCCC" "CM"
    |> replace "CCCC" "CD"
    |> replace "LXXXX" "XC"
    |> replace "XXXX" "XL"
    |> replace "VIIII" "IX"
    |> replace "IIII" "IV"

// test it
toRomanNumerals_v2 4
toRomanNumerals_v2 14
toRomanNumerals_v2 19
toRomanNumerals_v2 1947
toRomanNumerals_v2 1999

// test it on all the numbers up to 30
[1..30] |> List.map toRomanNumerals_v2 |> String.concat ","

// ======================================
// Pipelines are useful because you can easily insert new code in the middle
// without breaking existing code
// ======================================

let toRomanNumerals_withLog number =

    let logger step output =
        printfn "The output at step '%s' was %s" step output
        output  // return the output

    String.replicate number "I"
    |> logger "at beginning"
    |> replace "IIIII" "V"
    |> replace "VV" "X"
    |> replace "XXXXX" "L"
    |> replace "LL" "C"
    |> replace "CCCCC" "D"
    |> replace "DD" "M"
    |> logger "before special cases"

    // additional special forms
    |> replace "DCCCC" "CM"
    |> replace "CCCC" "CD"
    |> replace "LXXXX" "XC"
    |> replace "XXXX" "XL"
    |> replace "VIIII" "IX"
    |> replace "IIII" "IV"
    |> logger "final result"




// test it
toRomanNumerals_withLog 4
toRomanNumerals_withLog 14
toRomanNumerals_withLog 19
