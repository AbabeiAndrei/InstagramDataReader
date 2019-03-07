using System.Windows.Forms;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram.Controls
{
    public partial class InstragramCommentsControl : UserControl
    {
        public InstragramCommentsControl(IInstagramCommentCollection instagramCommentCollection)
        {
            InitializeComponent();
            lblTitle.Text = instagramCommentCollection.Name;
            instagramCommentBindingSource.DataSource = instagramCommentCollection;
        }
    }
}
