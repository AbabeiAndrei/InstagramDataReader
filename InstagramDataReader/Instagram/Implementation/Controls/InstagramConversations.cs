using System.Windows.Forms;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram.Controls
{
    public partial class InstagramConversationsControl : UserControl
    {
        public InstagramConversationsControl(IInstagramConversation conversation)
        {
            InitializeComponent();
            lblTitle.Text = conversation.Name;
            instagramMessageBindingSource.DataSource = conversation.Messages;
        }
    }
}
