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
        List<NhanVien> ListMain = new List<NhanVien>;    
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

            return new NhanVien(maNV, hoTen, ngaySinh, gioiTinh, soDT, diaChi, phongBan, email, hinh);
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
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
