using System.Windows.Forms;
using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram.Ui
{
    public class InstagramUiHelper : IInstagramUiHelper
    {
        private readonly Form _form;

        public InstagramUiHelper(Form form)
        {
            _form = form;
        }

        public void FillTree(TreeView tvItems, INode reader)
        {
            var node = CreateTreeNode(reader);

            tvItems.Nodes.Clear();

            tvItems.Nodes.Add(node);

            node.Expand();
        }

        private TreeNode CreateTreeNode(INode node)
        {
            var tn = new TreeNode(node.Name)
            {
                Tag = node
            };

            foreach (var child in node.Childs)
                tn.Nodes.Add(CreateTreeNode(child));

            return tn;
        }
    }
}
