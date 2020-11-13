using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab07
{
    public partial class Form1 : Form
    {
        private DataTable foodTable;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadCategory()
        {
            string connectionString = "server = DESKTOP-L67JKQE\\SQLEXPRESS; database = Restaurant_Management; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Name FROM Category";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            //Mo ket noi
            conn.Open();

            //Lay du lieu tu csdl dua vao DataTable
            adapter.Fill(dt);

            //Dong ket noi va giai phong bo nho

            conn.Close();
            conn.Dispose();

            //Dua du lieu vao Combo Box
            cbbCategory.DataSource = dt;

            //Hien thi ten nhom san pham
            cbbCategory.DisplayMember = "Name";

            //Nhung khi lay gia tri thi lay ID cua nhom
            cbbCategory.ValueMember = "ID";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadCategory();
        }
    }
}
