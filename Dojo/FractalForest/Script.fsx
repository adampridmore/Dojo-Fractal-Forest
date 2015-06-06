open System
open System.Drawing
open System.Windows.Forms

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
let brush = new SolidBrush(Color.FromArgb(0, 0, 0))

box.Image <- image
box.Dock <- DockStyle.Fill
form.Controls.Add(box) 

// Compute the endpoint of a line
// starting at x, y, going at a certain angle
// for a certain length. 
let endpoint x y angle length =
    x + length * cos angle,
    y + length * sin angle

let flip x = (float)height - x

// Utility function: draw a line of given width, 
// starting from x, y
// going at a certain angle, for a certain length.
let drawLine (target : Graphics) (brush : Brush) 
             (x : float) (y : float) 
             (angle : float) (length : float) (width : float) =
    let x_end, y_end = endpoint x y angle length
    let origin = new PointF((single)x, (single)(y |> flip))
    let destination = new PointF((single)x_end, (single)(y_end |> flip))
    let pen = new Pen(brush, (single)width)
    target.DrawLine(pen, origin, destination)

let draw x y angle length width = 
    drawLine graphics brush x y angle length width

let pi = Math.PI

// Now... your turn to draw
// The trunk
//draw 250. 50. (pi*(0.5)) 100. 4.
//let x, y = endpoint 250. 50. (pi*(0.5)) 100.
//// first and second branches
//draw x y (pi*(0.5 + 0.3)) 50. 2.
//draw x y (pi*(0.5 - 0.4)) 50. 2.

type branchParams = {
  dirOffset: float;
  lenPercent: float;
  widthPercent: float
}

let randomSeed = new System.Random()

let randomFloat min max = 
  max - (randomSeed.NextDouble() * (max - min))

let randomInt min max = 
  min + randomSeed.Next(max - min)

// seq{0..1000} |> Seq.map (fun _ -> randomInt -50 100 |> float) |> Seq.average

//let treeParams = 
//  [ 
//    {dirOffset= 0.4; lenPercent=0.6; widthPercent=0.5};
//    {dirOffset= -0.5; lenPercent=0.6; widthPercent=0.4};
//    {dirOffset=(0.3); lenPercent=0.4; widthPercent=0.3};
//    {dirOffset=(-0.4); lenPercent=0.4; widthPercent=0.2}
//
//    {dirOffset=(0.2); lenPercent=0.7; widthPercent=0.2}
//    {dirOffset=(0.4); lenPercent=0.2; widthPercent=0.2}
//    {dirOffset=(0.6); lenPercent=0.3; widthPercent=0.2}
//    {dirOffset=(-0.8); lenPercent=0.4; widthPercent=0.2}
//    {dirOffset=(1.0); lenPercent=0.5; widthPercent=0.2}
//    {dirOffset=(1.2); lenPercent=0.6; widthPercent=0.2}
//]

let maxDepth = randomInt 5 10
let numberOfSplits = randomInt 1 3
let treeParams = seq{ 
  for _ in 1..numberOfSplits do 
    yield {
      dirOffset = randomFloat -1. 1.;
      lenPercent = randomFloat 0.4 0.8; 
      widthPercent = randomFloat 0.3 0.6 }
}

treeParams |> Seq.iter (printfn "%A")

let rec branch currentDepth x y dir len width =
  draw x y dir len width
  
  let (x',y') = endpoint x y dir (len*0.95)

  if currentDepth >= maxDepth then () |> ignore
  else 
    treeParams
    |> Seq.iter (fun p -> branch (currentDepth+1) x' y' (dir+p.dirOffset) (len * p.lenPercent) (width*p.widthPercent))

branch 1 250. 10. (0.5*pi) 150. 10.

form.ShowDialog()


(* To do a nice fractal tree, using recursion is
probably a good idea. The following link might
come in handy if you have never used recursion in F#:
http://en.wikibooks.org/wiki/F_Sharp_Programming/Recursion
*)

