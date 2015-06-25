#load "Tree.fs"
#r "../packages/FSharp.Collections.ParallelSeq.1.0.2/lib/net40/FSharp.Collections.ParallelSeq.dll"

open System
open System.Drawing
open System.Windows.Forms
open Tree
open FSharp.Collections.ParallelSeq

System.Environment.CurrentDirectory <- "c:\\temp"

// Create a form to display the graphics
//let width, height = 500, 500         
//let form = new Form(Width = width, Height = height)
//let box = new PictureBox(BackColor = Color.White, Dock = DockStyle.Fill)
//
//box.Image <- image
//box.Dock <- DockStyle.Fill
//form.Controls.Add(box) 

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



//// Growing trees
//let maxDepth = 8
//seq{1200..-200..0}
//|> Seq.iter (fun index -> 
//  [
//      {dirOffset = -1.2 + (float index * 0.0010);lenPercent = float index * 0.0005; widthPercent = 0.4};
//      {dirOffset = -1.3 + (float index * 0.0008);lenPercent = float index * 0.0005; widthPercent = 0.4};
//      {dirOffset = -1.0 + (float index * 0.0011);lenPercent = float index * 0.0006; widthPercent = 0.3};
//      {dirOffset = -0.6 + (float index * 0.0012);lenPercent = float index * 0.0006; widthPercent = 0.5};
//  ]
//  |> PSeq.map (fun x -> printfn "Index: %i %A" index x ; x)
//  |> drawTree true maxDepth
//  |> save (sprintf "Tree_%i.%s" index (imageFormat.ToString()))
// )


// Random Trees
//let maxDepth = 7 //randomInt 4 7
//let numberOfSplits = 7 //randomInt 3 6
//seq{0..1000}
//|> Seq.iter (fun index -> 
//  generateTreeParams numberOfSplits |> Seq.toList
//  |> PSeq.map (fun x -> printfn "Index: %i %A" index x ; x)
//  |> drawTree true maxDepth
//  |> save (sprintf "Tree_%i.%s" index (imageFormat.ToString()))
// )


// Single Tree
seq{0..10}
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
