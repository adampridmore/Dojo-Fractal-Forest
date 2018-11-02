#load "Tree.fs"
#load "TreeEvolver.fs"

open System.Drawing
open Tree
open TreeEvolver

System.Environment.CurrentDirectory <- "c:\\temp\\tree"


let imageFormat = Imaging.ImageFormat.Png
let save (fileName:string) (image:Image) = 
  image.Save(fileName, imageFormat)
  image

let tp1 = {
  maxDepth=3;
  branches = 
    [| 
      {dirOffset = -0.1;lenPercent = 0.8; widthPercent = 0.5};
      {dirOffset = 0.0;lenPercent = 0.8; widthPercent = 0.5};
      {dirOffset = 0.1;lenPercent = 0.8; widthPercent = 0.5} 
    |]
  }
   
let treeSave name treeParams = 
  treeParams |> tree  |> save name |> ignore
  treeParams
  
tp1 
|> treeSave "t1.png"
|> mutateTree |> treeSave "t2.png"
|> mutateTree |> treeSave "t3.png"
|> mutateTree |> treeSave "t4.png"
|> mutateTree |> treeSave "t5.png"