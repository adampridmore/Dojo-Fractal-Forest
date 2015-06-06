open System
open System.Drawing
open System.Windows.Forms
open Tree

#load "Tree.fs"

System.Environment.CurrentDirectory <- "c:\\temp"

// Create a form to display the graphics
let width, height = 500, 500         
let form = new Form(Width = width, Height = height)
let box = new PictureBox(BackColor = Color.White, Dock = DockStyle.Fill)
let image = new Bitmap(width, height)
let graphics = Graphics.FromImage(image)
//The following line produces higher quality images, 
//at the expense of speed. Uncomment it if you want
//more beautiful images, even if it's slower.
//Thanks to https://twitter.com/AlexKozhemiakin for the tip!
graphics.SmoothingMode <- System.Drawing.Drawing2D.SmoothingMode.HighQuality

box.Image <- image
box.Dock <- DockStyle.Fill
form.Controls.Add(box) 

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

let treeParams = generateTreeParams numberOfSplits
//treeParams |> Seq.iter (printfn "%A")
treeParams |> drawTree graphics height maxDepth

//form.ShowDialog()
image.Save(sprintf "Tree_%i.bmp" (randomInt 0 10000))
