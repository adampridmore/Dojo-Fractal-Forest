module CellularAutomation

type cell = A|D

let isAliveRule (cells:array<cell>) = 
  match cells with
  | [|D;D;D|] -> D
  | [|D;D;A|] -> A
  | [|D;A;D|] -> A
  | [|D;A;A|] -> A
  | [|A;D;D|] -> A
  | [|A;D;A|] -> D
  | [|A;A;D|] -> D
  | [|A;A;A|] -> D
  | _ -> failwith ("Invalid cell sequence in rules" + (sprintf "%A" cells))

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
  Seq.unfold (fun previousRow -> let currentRow = nextRow previousRow
                                 Some(previousRow, currentRow)) initialRow

