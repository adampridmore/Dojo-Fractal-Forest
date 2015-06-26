// https://en.wikipedia.org/wiki/Cellular_automaton

#load "CellRules.fs"
#load "CellularAutomation.fs"
#load "CellularAutomationHelpers.fs"


open CellRules
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


let singleAliveRow width = 
  seq{
    for n in 0..width-1 do
      yield if n = width / 2 
            then A
            else D
  } 
  |> Seq.toArray

let randomRow width  = 
  let r = new System.Random();
  seq{0..width-1}
  |> Seq.map (fun _ -> r.Next() % 2 = 0)
  |> Seq.map (fun x -> match x with 
                       | true -> D
                       | false -> A)
  |> Seq.toArray

let renderImage (width:int) (height:int) = 
  let image = new Bitmap(width, height)
  use g = Graphics.FromImage(image);
  g.Clear(Color.White)

  let cellToColor cell =
    match cell with 
    | A -> Color.Black
    | D -> Color.LightGray

  (rowSequence (singleAliveRow width))
  //(rowSequence (randomRow width))
  |> Seq.take height
  |> Seq.iteri (fun y row ->  row 
                              |> Seq.iteri (fun x cell -> image.SetPixel(x,y, cellToColor cell) )
                              )

  image

let saveImage (image:System.Drawing.Image) = 
  let imageFormat = Imaging.ImageFormat.Png
  let filename = sprintf "c:\\temp\\ca\\ca_R2_%i_%i.%s" image.Width image.Height (imageFormat.ToString())
  image.Save(filename, imageFormat);

  System.Diagnostics.Process.Start(filename) |> ignore


let showImage (image:System.Drawing.Image) = 
  let form = new Form()
  form.Size <- new Size(image.Size.Width, image.Size.Height)

  let pb = new PictureBox();
  pb.Dock <- DockStyle.Fill
  form.Controls.Add(pb);

  pb.Image <- (image)

  form.ShowDialog() |> ignore
  () 
  
//seq{3000..500..10000} 
Seq.singleton 1000
|> Seq.iter (fun height -> 
  let width = height * 2
  let image = renderImage width height
  
  image |> saveImage 
  //image |> showImage
  ()
)
