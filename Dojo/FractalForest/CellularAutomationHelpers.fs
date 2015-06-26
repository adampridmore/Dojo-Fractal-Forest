module CellularAutomationHelpers

open CellularAutomation
open CellRules

let arrayToString (x:array<cell>) = 
 x 
 |> Seq.map (fun item -> match item with 
                         | A -> "X"
                         | _ -> " ")
 |> Seq.reduce (+)
 
let toCharArray (x:string) = x.ToCharArray()

let print x =
  printfn "%A" (x |> arrayToString)
  x
  
let blankList length = 
  List.init length (fun _ -> ' ')


let charArrayToCells (ca:array<char>) =
  let charToCell c = 
    match c with 
    | 'X' -> A
    | _ -> D
    
  ca |> Seq.map charToCell |> Seq.toArray
