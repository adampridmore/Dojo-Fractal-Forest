open System
open System.Drawing
open System.Windows.Forms
open Tree

#load "Tree.fs"

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

let maxDepth = randomInt 8 10
let numberOfSplits = randomInt 2 4

let save (fileName:string) (image:Image) = 
  image.Save(fileName, Imaging.ImageFormat.Png)

let writeToFile lines = 
  System.IO.File.WriteAllLines("params.txt", lines |> Seq.toArray)

seq{0..100}
|> Seq.iter (fun index -> 
    let treeParams = generateTreeParams numberOfSplits |> Seq.toList
    //treeParams |> Seq.map(sprintf "%A") |> writeToFile

    treeParams
    |> drawTree maxDepth
    |> save (sprintf "Tree_%i.png" index)
  )
