module Tree
open System
open System.Drawing

type branchParams = {
  dirOffset: float;
  lenPercent: float;
  widthPercent: float
}

type treeParams = {
  maxDepth: int;
  branches : branchParams array;
}

let drawTree showParams maxDepth treeParams =
  let width, height = 800, 1024
  let image = new Bitmap(width, height)
  use graphics = Graphics.FromImage(image)
  graphics.SmoothingMode <- System.Drawing.Drawing2D.SmoothingMode.HighQuality
  
  graphics.Clear(Color.White)
  
  let flip x = (float)height - x

  let brush = new SolidBrush(Color.Black)
  
  // Compute the endpoint of a line
  // starting at x, y, going at a certain angle
  // for a certain length. 
  let endpoint x y angle length =
      x + length * cos angle,
      y + length * sin angle

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

  let draw x y angle length width brush =
    drawLine graphics brush x y angle length width

  let pi = Math.PI

  let rec branch currentDepth x y dir len width =
    draw x y dir len width brush
      
    let (x',y') = endpoint x y dir (len*0.95)

    if currentDepth >= maxDepth then () |> ignore
    else 
      treeParams
      |> Seq.iter (fun p -> branch (currentDepth+1) x' y' (dir+p.dirOffset) (len * p.lenPercent) (width*p.widthPercent))

  branch 1 250. 200. (0.5*pi) 150. 10.
  
  let font = new System.Drawing.Font("Arial", 9.0f)

  let drawParameters treeParams = 
    let text = treeParams |> Seq.map (sprintf "%A") |> Seq.reduce (+)
    let point = new PointF(0.0f,(float32 height - 200.0f))
    graphics.DrawString(text.ToString(), font, brush, point)

  if showParams then drawParameters treeParams
  else ()

  image

let tree treeParams =
  drawTree true treeParams.maxDepth treeParams.branches