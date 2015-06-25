module CellularAutomation

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

let nextRow (row:array<char>) = 
  row
  |> Seq.mapi (fun i _ -> isAlive row i)
  |> Seq.toArray

let rowSequence initialRow = 
  Seq.unfold (fun previousRow -> let currentRow = nextRow previousRow
                                 Some(previousRow, currentRow)) initialRow

