// https://en.wikipedia.org/wiki/Cellular_automaton

let isAliveRule (cells:array<char>) = 
  match cells with
  | [|' ';' ';' '|] -> ' '
  | [|' ';' ';'X'|] -> 'X'
  | [|' ';'X';' '|] -> 'X'
  | [|' ';'X';'X'|] -> 'X'
  | [|'X';' ';' '|] -> 'X'
  | [|'X';' ';'X'|] -> ' '
  | [|'X';'X';' '|] -> ' '
  | [|'X';'X';'X'|] -> ' '
  | _ -> 'E'

let getCellFromArray (array:array<char>) index =
  match index with
  | index when index < 0 -> ' '
  | index when index >= Array.length array -> ' '
  | _ -> array.[index]

let isAlive (row:array<char>) index =
  [|
    getCellFromArray row (index-1);
    getCellFromArray row (index);
    getCellFromArray row (index+1)
  |] 
  |> isAliveRule
    
let arrayToString x = 
 x 
 |> Seq.map (fun item -> item.ToString())
 |> Seq.reduce (+)
 
let toCharArray (x:string) = x.ToCharArray()


let nextRow (row:array<char>) = 
  row
  |> Seq.mapi (fun i _ -> isAlive row i)
  |> Seq.toArray

let print x =
  printfn "%A" (x |> arrayToString)
  x
  
let blankList length = 
  List.init length (fun _ -> ' ')

let rowSequence initialRow = 
  Seq.unfold (fun previousRow -> let currentRow = nextRow previousRow
                                 Some(previousRow, currentRow)) initialRow

let width = 200
let row = (
            (blankList (width/2)) @ 
            ['X'] @ 
            (blankList (width/2)) |> List.toArray
          )
(rowSequence row)
|> Seq.take 100
|> Seq.iter (fun row -> printfn "%A" (row |> arrayToString))
