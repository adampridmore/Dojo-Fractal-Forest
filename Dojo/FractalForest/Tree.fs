module Tree
open System
open System.Drawing
open System.Windows.Forms

type branchParams = {
  dirOffset: float;
  lenPercent: float;
  widthPercent: float
}

let drawTree graphics height maxDepth treeParams =
  let flip x = (float)height - x

  let brush = new SolidBrush(Color.FromArgb(0, 0, 0))
  
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

  let draw x y angle length width = 
      drawLine graphics brush x y angle length width

  let pi = Math.PI

  let rec branch currentDepth x y dir len width =
    draw x y dir len width
  
    let (x',y') = endpoint x y dir (len*0.95)

    if currentDepth >= maxDepth then () |> ignore
    else 
      treeParams
      |> Seq.iter (fun p -> branch (currentDepth+1) x' y' (dir+p.dirOffset) (len * p.lenPercent) (width*p.widthPercent))

  branch 1 250. 10. (0.5*pi) 150. 10.
