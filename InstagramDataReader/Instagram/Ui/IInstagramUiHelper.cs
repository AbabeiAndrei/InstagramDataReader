using System;
using System.Windows.Forms;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram.Ui
{
    public interface IInstagramUiHelper
    {
        void FillTree(TreeView tvItems, INode reader);
    }
}
