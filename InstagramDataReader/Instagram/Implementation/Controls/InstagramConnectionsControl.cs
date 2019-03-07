using System.Windows.Forms;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram.Controls
{
    public partial class InstagramConnectionsControl : UserControl
    {
        public InstagramConnectionsControl(IInstagramConnectionCollection instagramConnectionCollection)
        {
            InitializeComponent();
            lblTitle.Text = instagramConnectionCollection.Name;
            instagramConnectionBindingSource.DataSource = instagramConnectionCollection;
        }
    }
}
