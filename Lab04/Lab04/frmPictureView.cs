using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class frmPictureView : Form
    {
        int count = 0;
        public frmPictureView()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlg = this.OpenFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    frmPicture frm = new frmPicture(OpenFileDlg.FileName);
                    frm.MdiParent = this;
                    count++;
                    frm.Text = "Picture -" + count + "-" + OpenFileDlg.FileName;
                    frm.Show();
                }
                this.toolStripStatusLabel1.Text = "Tổng số Form con: " + count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlg = this.saveFileDlg.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                frmPicture frm = this.ActiveMdiChild as frmPicture;
                try
                {
                    Image img = frm.pbHinh.Image;
                    img.Save(saveFileDlg.FileName, ImageFormat.Bmp);
                }
                catch 
                {
                    MessageBox.Show("Lỗi lưu file");
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = this.toolStripToolStripMenuItem.Checked;
            if (check)
            {
                this.toolStrip1.Visible = true;
            }
            else
                this.toolStrip1.Visible = false;
        }

        private void arrangIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void arrangeHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void arrangeVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void maximizeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
                frm.WindowState = FormWindowState.Maximized;
        }

        private void minimizeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.WindowState = FormWindowState.Minimized;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var currentChilForm = ActiveMdiChild as frmPicture;
            currentChilForm.zoomToolStripMenuItem.PerformClick();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var currentChilForm = ActiveMdiChild as frmPicture;
            currentChilForm.zoomToolStripMenuItem1.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var currentChildForm = ActiveMdiChild as frmPicture;
            currentChildForm.editToolStripMenuItem.PerformClick();
            
        }
    }
}
