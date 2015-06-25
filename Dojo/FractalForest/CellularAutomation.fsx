// https://en.wikipedia.org/wiki/Cellular_automaton

#load "CellularAutomation.fs"

open CellularAutomation

let arrayToString x = 
 x 
 |> Seq.map (fun item -> item.ToString())
 |> Seq.reduce (+)
 
let toCharArray (x:string) = x.ToCharArray()

let print x =
  printfn "%A" (x |> arrayToString)
  x
  
let blankList length = 
  List.init length (fun _ -> ' ')


let width = 200
let row = (
            (blankList (width/2)) @ 
            ['X'] @ 
            (blankList (width/2)) |> List.toArray
          )
(rowSequence row)
|> Seq.take 100
|> Seq.iter (fun row -> printfn "%A" (row |> arrayToString))

(rowSequence ("              X              " |> toCharArray))
|> Seq.take 15
|> Seq.iter (fun row -> printfn "%A" (row |> arrayToString))


