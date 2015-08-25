module ImageHelpers

open System.Drawing

let saveImage (image:System.Drawing.Image) = 
  let imageFormat = Imaging.ImageFormat.Png
  let filename = sprintf "c:\\temp\\ca\\ca_R2_%i_%i.%s" image.Width image.Height (imageFormat.ToString())
  image.Save(filename, imageFormat);

  System.Diagnostics.Process.Start(filename) |> ignore



