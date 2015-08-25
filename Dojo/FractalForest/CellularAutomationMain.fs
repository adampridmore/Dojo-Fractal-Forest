module CellularAutomationMain

open System
open System.Drawing
open System.Windows.Forms
open CellularAutomation
open CellularAutomationHelpers
open ImageHelpers

let doCellularAutomationConsole() = 
  let width = 100
  let row = (
              (blankList (width/2)) @ 
              ['X'] @ 
              (blankList (width/2)) |> List.toArray
            ) |> charArrayToCells
  (rowSequence row)
  |> Seq.take 100
  |> Seq.iter (fun row -> printfn "%A" (row |> arrayToString))
  ()

let doCellularAutomationToFile() = 
  let height = 2000
  let width = height * 2
  let initialRow = 
    (rowSequence (singleAliveRow width))
    //(rowSequence (randomRow width))

  renderImage width height initialRow
  |> saveImage 

