using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class frmPicture : Form
    {
        Point p = new Point();
        //phương thức tạo lập picture có tham sô
        public frmPicture()
        {
            InitializeComponent();
        }
        public frmPicture(string name) : this()
        {
            pbHinh.ImageLocation = name;
            toolStripStatusLabel1.Text = name;
        }
        //sự kiện load fmpicture
        private void frmPicture_Load(object sender, EventArgs e)
        {
            p = this.pbHinh.Location;
        }
        //Reload file cho hinh
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlg = this.openFileDlg.ShowDialog();
            string title = "";
            if (dlg==DialogResult.OK)
            {
                title = this.Text.Substring(0, this.Text.LastIndexOf('-'))
                    + openFileDlg.FileName;
                this.Text = title;
                this.pbHinh.ImageLocation = openFileDlg.FileName;
            }
        }
        //Phong lon hinh
        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pbHinh.Width += 50;
            this.pbHinh.Height += 50;
        }

        private void zoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.pbHinh.Width -= 50;
            this.pbHinh.Height -= 50;
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.pbHinh.Location = new Point(p.X, p.Y - e.NewValue);
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.pbHinh.Location = new Point(p.X -e.NewValue,p.Y);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("mspaint", this.pbHinh.ImageLocation);
        }
    }
}
