using System;
using System.Windows.Forms;

using InstagramDataReader.Instagram;
using InstagramDataReader.Interfaces;
using InstagramDataReader.Instagram.Ui;

namespace InstagramDataReader
{
    public partial class MainForm : Form
    {
        private IInstagramReader _reader;
        private IInstagramUiHelper _uiHelper;

        public MainForm()
        {
            InitializeComponent();
            _uiHelper = new InstagramUiHelper(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WinApi.SetExplorerTheme(tvItems);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _reader?.Dispose();
        }

        private async void tsmiOpen_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                var result = fbd.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                tsslFile.Text = "Loading...";
                tspbLoading.Visible = true;

                _reader?.Dispose();

                _reader = await InstagramReader.CreateReader(fbd.SelectedPath);

                _reader.Conversations.ReplaceSelfUser(_reader.Profile);

                _uiHelper.FillTree(tvItems, _reader);

                tsslFile.Text = _reader.Directory;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                tspbLoading.Visible = false;
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiHelp_Click(object sender, EventArgs e)
        {

        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {

        }

        private void tvItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is INodeControl node)
                SetControl(node.CreateControl());
            else
                ClearControl();
        }

        private void SetControl(Control control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ClearControl();

            control.Dock = DockStyle.Fill;

            mainContainer.Panel2.Controls.Add(control);
        }

        private void ClearControl()
        {
            foreach (IDisposable ctrl in mainContainer.Panel2.Controls)
                ctrl.Dispose();

            mainContainer.Panel2.Controls.Clear();
        }
    }
}
