module main

open System
open System.Drawing
open System.Windows.Forms
open Tree
open FSharp.Collections.ParallelSeq
open CellularAutomationMain

let doTrees() = 
  //System.Environment.CurrentDirectory <- "c:\\temp"

  let randomSeed = new System.Random()

  let randomFloat min max = 
    max - (randomSeed.NextDouble() * (max - min))

  let randomInt min max = 
    min + randomSeed.Next(max - min)

  let generateTreeParams numberOfSplits = 
    seq{ 
      for _ in 1..numberOfSplits do 
        yield {
          dirOffset = randomFloat -1. 1.;
          lenPercent = randomFloat 0.4 0.8; 
          widthPercent = randomFloat 0.3 0.6
        }
      } 

  let imageFormat = Imaging.ImageFormat.Png
  let save (fileName:string) (image:Image) = 
    image.Save(fileName, imageFormat)

  let writeToFile lines = 
    System.IO.File.WriteAllLines("params.txt", lines |> Seq.toArray)

  // Single Tree
  seq{0..7}
  //Seq.singleton 1
  |> PSeq.iter (fun index -> 
    let maxDepth = index
    [
        {dirOffset = 0.129;lenPercent = 0.403; widthPercent = 0.48};
        {dirOffset = 0.-0.007;lenPercent = 0.636; widthPercent = 0.52};
        {dirOffset = -0.299;lenPercent = 0.412; widthPercent = 0.315};
        {dirOffset = 0.714;lenPercent = 0.442; widthPercent = 0.560};
        {dirOffset = -0.086;lenPercent = 0.756; widthPercent = 0.517};
        {dirOffset = 0.0616;lenPercent = 0.601; widthPercent = 0.360};
    ]
    |> drawTree true maxDepth
    |> save (sprintf "Tree_%i.%s" index (imageFormat.ToString()))
  )

[<EntryPoint>]
let main argv = 
  //doCellularAutomationToFile()
  doTrees()
  
  0