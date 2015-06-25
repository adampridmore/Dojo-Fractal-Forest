// https://en.wikipedia.org/wiki/Cellular_automaton

#load "CellularAutomation.fs"
#load "CellularAutomationHelpers.fs"

open CellularAutomation
open CellularAutomationHelpers
open System.Windows.Forms
open System.Drawing;


//let width = 200
//let row = (
//            (blankList (width/2)) @ 
//            ['X'] @ 
//            (blankList (width/2)) |> List.toArray
//          ) |> charArrayToCells
//
//(rowSequence row)
//|> Seq.take 100
//|> Seq.iter (fun row -> printfn "%A" (row |> arrayToString))
//
//(rowSequence ("              X              " |> toCharArray |> charArrayToCells))
//|> Seq.take 15
//|> Seq.iter (fun row -> printfn "%A" (row |> arrayToString))


let renderImage (width:int) (height:int) = 
  let image = new Bitmap(width, height)
  let g = Graphics.FromImage(image);
  g.Clear(Color.White)

  let cellToColor cell =
    match cell with 
    | A -> Color.Black
    | D -> Color.LightGray

  let initialRow = (seq{
    for n in 0..width-1 do
      yield if n = width / 2 
            then A
            else D
  } |> Seq.toArray)

  (rowSequence initialRow)
  |> Seq.take height
  |> Seq.iteri (fun y row ->  row 
                              |> Seq.iteri (fun x cell -> image.SetPixel(x,y, cellToColor cell) )
                              )

  image

seq{100..100..10000} 
|> Seq.iter (fun height -> 
  let width = height * 2
  let image = renderImage width height
  image.Save(sprintf "c:\\temp\\ca_%i_%i.bmp" width height);
)
  


//let form = new Form()
//form.Size <- new Size(width, height)

//let pb = new PictureBox();
//pb.Dock <- DockStyle.Fill
//form.Controls.Add(pb);

//let image = renderImage width height
//image.Save(sprintf "c:\\temp\\ca_%i_%i.bmp" width height);

//pb.Image <- (image :> Image)

//form.ShowDialog()