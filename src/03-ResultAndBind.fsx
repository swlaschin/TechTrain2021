// ================================================
// Demo using Result and bind
// ================================================

// Some data to validate
type Input = {
   Name : string
   Email : string
}

//-------------------------------------
// Here are the validation functions
//-------------------------------------

let checkNameNotBlank input =
  if input.Name = "" then
    Error "Name must not be blank"
  else
    Ok input

let checkName50 input =
  if input.Name.Length > 50 then
    Error "Name must not be longer than 50 chars"
  else
    Ok input

let checkEmailNotBlank input =
  if input.Email = "" then
    Error "Email must not be blank"
  else
    Ok input

//-------------------------------------
// Chain these functions together - the ugly way
//-------------------------------------


let validateInput_ugly input =
    input
    |> checkNameNotBlank
    |> (fun result ->
        match result with
        | Ok input ->
            checkName50 input
        | Error err ->
            Error err
        )
    |> (fun result ->
        match result with
        | Ok input ->
            checkEmailNotBlank input
        | Error err ->
            Error err
        )


//-------------------------------------
// Chain these functions together - the nice way
//-------------------------------------

let validateInput input =
    input
    |> checkNameNotBlank
    |> Result.bind checkName50
    |> Result.bind checkEmailNotBlank


// -------------------------------
// test that the validation works
// -------------------------------

let goodInput = {Name="Scott";Email="x@example.com"}
validateInput goodInput

let blankName = {Name="";Email="x@example.com"}
validateInput blankName

let blankEmail = {Name="Scott";Email=""}
validateInput blankEmail


//-------------------------------------
// Different names for "bind"
//-------------------------------------

// The "bind" function has many different names in different contexts
// E.g "flatMap"
// For example, we could also call it "andThen" to make it read more easily
let andThen = Result.bind

// and here is the same code written using "andThen"
let validateInput_AndThen input =
    input
    |> checkNameNotBlank
    |> andThen checkName50
    |> andThen checkEmailNotBlank


//-------------------------------------
// It is easy to add some more validation functions
//-------------------------------------

// examples:
// * email contains @ symbol  -- use Email.Contains("@")
// * email length < 50        -- use Email.Length

let checkEmailMustHaveAtSign input =
  if not (input.Email.Contains("@")) then
    Error "Email must have @ sign"
  else
    Ok input

let checkEmailLength input =
  if not (input.Email.Length < 50) then
    Error "Email must be < 50 chars"
  else
    Ok input

let validateInput_v2 input =
    input
    |> checkNameNotBlank
    |> Result.bind checkName50
    |> Result.bind checkEmailNotBlank
    |> Result.bind checkEmailMustHaveAtSign  // Can also use built-in Result.bind
    |> Result.bind checkEmailLength


