module CellularAutomation
open CellRules

let getCellFromArray (array:array<cell>) index =
  match index with
  | index when index < 0 -> D
  | index when index >= Array.length array -> D
  | _ -> array.[index]
    

let isAlive (row:array<cell>) index =
  [|
    getCellFromArray row (index-1);
    getCellFromArray row (index);
    getCellFromArray row (index+1)
  |] 
  |> isAliveRule

let nextRow (row:array<cell>) = 
  row
  |> Seq.mapi (fun i _ -> isAlive row i)
  |> Seq.toArray

let rowSequence initialRow = 
  Seq.unfold (fun previousRow -> Some(previousRow, nextRow previousRow)) initialRow

