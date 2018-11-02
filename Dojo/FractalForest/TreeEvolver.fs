module TreeEvolver

open Tree

let randomSeed = new System.Random(0)

let random min max = 
  min + randomSeed.NextDouble() * (max - min)

let mutateTree treeParams =
  let magnitude = 0.05

  let smallRandom() = random -magnitude magnitude

  let mutateBranch branch = 
    {
      dirOffset = branch.dirOffset + smallRandom();
      lenPercent = branch.lenPercent + smallRandom();
      widthPercent = branch.widthPercent + smallRandom();
    }

  let newBranches = treeParams.branches |> Array.map mutateBranch

  {treeParams with branches = newBranches}
