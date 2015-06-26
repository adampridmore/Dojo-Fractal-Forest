module CellRules

type cell = A|D

// https://en.wikipedia.org/wiki/Rule_30
let isAliveRuleRule30 (cells:array<cell>) = 
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

// https://en.wikipedia.org/wiki/Rule_90
let isAliveRuleRule90 (cells:array<cell>) = 
  match cells with
  | [|D;D;D|] -> D
  | [|D;D;A|] -> A
  | [|D;A;D|] -> D
  | [|D;A;A|] -> A
  | [|A;D;D|] -> A
  | [|A;D;A|] -> D
  | [|A;A;D|] -> A
  | [|A;A;A|] -> D
  | _ -> failwith ("Invalid cell sequence in rules" + (sprintf "%A" cells))

// https://en.wikipedia.org/wiki/Rule 110
let isAliveRuleRule110 (cells:array<cell>) = 
  match cells with
  | [|D;D;D|] -> D
  | [|D;D;A|] -> A
  | [|D;A;D|] -> A
  | [|D;A;A|] -> A
  | [|A;D;D|] -> D
  | [|A;D;A|] -> A
  | [|A;A;D|] -> A
  | [|A;A;A|] -> D
  | _ -> failwith ("Invalid cell sequence in rules" + (sprintf "%A" cells))

// https://en.wikipedia.org/wiki/Rule 184
let isAliveRuleRule184 (cells:array<cell>) = 
  match cells with
  | [|D;D;D|] -> D
  | [|D;D;A|] -> D
  | [|D;A;D|] -> D
  | [|D;A;A|] -> A
  | [|A;D;D|] -> A
  | [|A;D;A|] -> A
  | [|A;A;D|] -> D
  | [|A;A;A|] -> A
  | _ -> failwith ("Invalid cell sequence in rules" + (sprintf "%A" cells))

let isAliveRule (cells:array<cell>) = 
  //isAliveRuleRule30 cells
  //isAliveRuleRule90 cells
   isAliveRuleRule110 cells
  //isAliveRuleRule184 cells