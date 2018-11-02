using System.Collections.Generic;
using System.Windows.Forms;

namespace TreeVolution
{
    public partial class Form1 : Form
    {
        private readonly List<PictureBox> _pictureBoxes = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();

            foreach (var control in Controls)
            {
                if (control is PictureBox)
                {
                    _pictureBoxes.Add((PictureBox)control);
                }
            }

            foreach (var pictureBox in _pictureBoxes)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.MouseClick += pictureBox_MouseClick;
            }

            var treeParams = CreateInitialTreeParams();

            GenerateAndRenderNewTrees(treeParams);
        }

        private void GenerateAndRenderNewTrees(Tree.treeParams treeParams)
        {
            foreach (var pictureBox in _pictureBoxes)
            {
                var mutateTree = TreeEvolver.mutateTree(treeParams);
                pictureBox.Image = Tree.tree(mutateTree);
                pictureBox.Tag = mutateTree;
            }
        }

        void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var pictureBox = (PictureBox) sender;
            var treeParams = (Tree.treeParams) pictureBox.Tag;
            GenerateAndRenderNewTrees(treeParams);
        }

        private static Tree.treeParams CreateInitialTreeParams()
        {
            var branchParamses = new[]
            {
                new Tree.branchParams(-0.2, 0.7, 0.7),
                new Tree.branchParams(-0.1, 0.8, 0.8),
                new Tree.branchParams(0.0, 0.8, 0.8),
                new Tree.branchParams(0.1, 0.8, 0.8),
                new Tree.branchParams(0.2, 0.7, 0.7),
            };

            var treeParams = new Tree.treeParams(3, branchParamses);
            return treeParams;
        }
    }
}
