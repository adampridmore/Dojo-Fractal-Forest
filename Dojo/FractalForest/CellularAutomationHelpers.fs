module CellularAutomationHelpers

open CellularAutomation
open CellRules
open System.Drawing
open System.Windows.Forms

let arrayToString (x:array<cell>) = 
 x 
 |> Seq.map (fun item -> match item with 
                         | A -> "X"
                         | _ -> " ")
 |> Seq.reduce (+)
 
let toCharArray (x:string) = x.ToCharArray()

let print x =
  printfn "%A" (x |> arrayToString)
  x
  
let blankList length = 
  List.init length (fun _ -> ' ')


let charArrayToCells (ca:array<char>) =
  let charToCell c = 
    match c with 
    | 'X' -> A
    | _ -> D
    
  ca |> Seq.map charToCell |> Seq.toArray

let singleAliveRow width = 
  seq{
    for n in 0..width-1 do
      yield if n = width / 2 
            then A
            else D
  } 
  |> Seq.toArray

let renderImage (width:int) (height:int) initialRow = 
  let image = new Bitmap(width, height)
  use g = Graphics.FromImage(image);
  g.Clear(Color.White)

  let cellToColor cell =
    match cell with 
    | A -> Color.Black
    | D -> Color.LightGray

  initialRow
  |> Seq.take height
  |> Seq.iteri (fun y row ->  row 
                              |> Seq.iteri (fun x cell -> image.SetPixel(x,y, cellToColor cell) )
                              )

  image

let showImage (image:System.Drawing.Image) = 
  let form = new Form()
  form.Size <- new Size(image.Size.Width, image.Size.Height)

  let pb = new PictureBox();
  pb.Dock <- DockStyle.Fill
  form.Controls.Add(pb);

  pb.Image <- (image)

  form.ShowDialog() |> ignore
  () 