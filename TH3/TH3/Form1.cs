using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH3
{
    public partial class frmNV : Form
    {
        List<NhanVien> ListMain = new List<NhanVien>();
        public frmNV()
        {
            InitializeComponent();
        }



        private void Q_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dunknow_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dunknow.SelectedItems.Count == 0)
            {
                string msg = "Choose one employee to change info";
                MessageBox.Show(msg, "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (btnAddjust.Text == "Sửa")
            {
                btnAddjust.Text = "Lưu";
                var selecItems = dunknow.SelectedItems[0];
                string maNV = selecItems.SubItems[0].Text;
                string tenNV = selecItems.SubItems[1].Text;
                string[] ngaySinhArr = selecItems.SubItems[2].Text.Split('/');
                string gioiTinh = selecItems.SubItems[3].Text;
                string diaChi = selecItems.SubItems[4].Text;
                string email = selecItems.SubItems[5].Text;
                string soDT = selecItems.SubItems[6].Text;
                string phongBan = selecItems.SubItems[7].Text;
                string hinh = selecItems.SubItems[8].Text;

                DateTime ngaySinh = new DateTime(int.Parse(ngaySinhArr[2]), int.Parse(ngaySinhArr[1]), int.Parse(ngaySinhArr[0]));
                txtID.Text = maNV;
                txtID.ReadOnly = true;

                txtName.Text = tenNV;
                if (gioiTinh == "Nam") checkedBoy.Checked = true;
                else checkedGirl.Checked = true;
                dtpBirth.Value = ngaySinh;
                txtAddress.Text = diaChi;
                txtEmail.Text = email;
                txtHinh.ImageLocation = hinh;

            }
            else if (btnAddjust.Text == "Lưu")
            {
                // Lấy thông tin mới của nhân viên
                var newNhanVien = GetNhanVien();

                // Xóa thông tin cũ của nhân viên
                var oldNhanVienIdx = ListMain.FindIndex(nv => nv.MaNV == newNhanVien.MaNV);
                ListMain.RemoveAt(oldNhanVienIdx);

                // Thêm thông tin nhân viên mới vào
                ListMain.Insert(oldNhanVienIdx, newNhanVien);

                // Hiển thị lại danh sách nhân viên
                AddListView();

                btnAddjust.Text = "Sửa";
                txtID.ReadOnly = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var nhanVien = GetNhanVien();
            ListMain.Add(nhanVien);
            AddListView();
        }
        private NhanVien GetNhanVien()
        {
            string maNV = this.txtID.Text;
            string gioiTinh = checkedBoy.Checked ? "Nam" : "Nữ";
            string hoTen = this.txtName.Text;
            DateTime ngaySinh = this.dtpBirth.Value;
            string soDT = this.phone.Text;
            string diaChi = txtAddress.Text;
            string phongBan = selectedItem.Text;
            string email = txtEmail.Text;
            string hinh = txtHinh.Text;

            return new NhanVien(maNV,hoTen,ngaySinh,gioiTinh,soDT,diaChi,phongBan,email,hinh);
        }

        
        private void ClearListView()
        {
            dunknow.Items.Clear();
        }

        private void AddListView()
        {
            ClearListView();
            foreach (var item in ListMain)
            {
                ListViewItem row = dunknow.Items.Add(item.MaNV);
                row.SubItems.Add(item.TenNV);
                row.SubItems.Add(item.NgaySinh.ToString("mm/DD/yyy"));
                row.SubItems.Add(item.GioiTinh);
                row.SubItems.Add(item.DiaChi);
                row.SubItems.Add(item.Email);
                row.SubItems.Add(item.SoDT);
                row.SubItems.Add(item.PhongBan);
                row.SubItems.Add(item.Hinh);

            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ListMain.Clear();
            AddListView();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            this.Reset();
        }

        private void Reset()
        {
            this.txtID.Text = "";
            this.checkedBoy.Checked = true;
            this.txtName.Text = "";
            this.dtpBirth.Value = DateTime.Now;
            this.phone.Text = "";
            this.txtAddress.Text = "";
            this.selectedItem.Text = "";
            this.txtEmail.Text = "";
            this.txtHinh.Text = "";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //OpenFileDialog op = new OpenFileDialog();
            //op.Filter = "JPG file|.jpg|All file|*.*";
            //if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    txtHinh.Text = op.FileName;
            //}
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "JPG file|*.jpg|All file|*.*";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtHinh.Text = op.FileName;
                txtHinh.ImageLocation = op.FileName;

            }
        }


    }
}
