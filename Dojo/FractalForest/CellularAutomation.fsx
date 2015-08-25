// https://en.wikipedia.org/wiki/Cellular_automaton

#load "CellRules.fs"
#load "CellularAutomation.fs"
#load "CellularAutomationHelpers.fs"
#load "ImageHelpers.fs"

#r "System.Drawing.dll"

open CellularAutomation
open CellularAutomationHelpers
open ImageHelpers
open CellRules

let randomRow width  = 
  let r = new System.Random();
  seq{0..width-1}
  |> Seq.map (fun _ -> r.Next() % 2 = 0)
  |> Seq.map (fun x -> match x with 
                       | true -> D
                       | false -> A)
  |> Seq.toArray
  
let height = 1000
let width = height * 2
let initialRow = 
  (rowSequence (singleAliveRow width))
  //(rowSequence (randomRow width))

renderImage width height initialRow
|> saveImage 
//|> showImage
