using System.Windows.Forms;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram.Controls
{
    public partial class InstagramConversationControl : UserControl
    {
        public InstagramConversationControl(IInstagramContacts instagramContacts)
        {
            InitializeComponent();
            lblTitle.Text = instagramContacts.Name;
            instagramContactBindingSource.DataSource = instagramContacts;
        }
    }
}
