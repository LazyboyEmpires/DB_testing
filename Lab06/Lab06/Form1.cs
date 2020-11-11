using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Lab06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //Tạo chuỗi kết nối tới cơ sở dữ liệu RestaurantManagement
            string connectionString = "server =DESKTOP-L67JKQE\\SQLEXPRESS; database = Restaurant_Management; Integrated Security = true; ";

            //Tạo đối tượng kết nối
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //Thiết lập lệnh truy vấn cho đối tượng Command
            string query = "SELECT ID, Name, Type FROM  Category";
            sqlCommand.CommandText = query;//Nhớ gõ cái này dô ml

            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //Thực thi lệnh bằng phương thức ExcuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Gọi hàm hiển thị dữ liệu trên màn hình
            this.DisplayCategory(sqlDataReader);

            //Đóng kết nối
            sqlConnection.Close();
        }
        private void DisplayCategory(SqlDataReader reader)
        {
            //Xóa tất cả các dòng hiện tại
            lvCategory.Items.Clear();

            //Đọc một dòng dữ liệu
            while (reader.Read())
            {
                //Tạo một dòng mới trong ListView
                ListViewItem item = new ListViewItem(reader["ID"].ToString());

                //Thêm dòng mới vào ListView
                lvCategory.Items.Add(item);

                //Bổ sung các thông tin khác cho ListViewItem
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());


            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Tạo đối tượng kết nối
            string connectionString = "server =DESKTOP-L67JKQE\\SQLEXPRESS; database = Restaurant_Management; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //Thiết lập lệnh truy vấn cho đối tượng Command
            string query = "INSERT INTO Category(Name,[Type]) " + " VALUES (N'" + txtName.Text + "', " + txtType.Text + ")";
            sqlCommand.CommandText = query;

            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            //Đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Thêm nhóm món ăn thành công");
                //Tải lại dữ liệu
                btnLoad.PerformClick();

                //Xóa các ô nhập
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                //Disable các nút xóa và cập nhật
                btnCapNhat.Enabled = false;
                btnXoa.Enabled = false;

                MessageBox.Show("Cập nhật nhóm món ăn thành công");

            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Tạo dối tượng kết nối
            string connectionString = "server =DESKTOP-L67JKQE\\SQLEXPRESS; database = Restaurant_Management; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //Thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "DELETE FROM Category" +
                "WHERE ID = " + txtID.Text;

            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //Thực thi lệnh bằng phương thức ExcuteReader
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            if (numOfRowsEffected != 1)
            {
                MessageBox.Show("Có lỗi xảy ra.");
                return;
            }

            lvCategory.Items.Remove(lvCategory.SelectedItems[0]);
            txtID.Text = "";
            txtName.Text = "";
            txtType.Text = "";
            btnCapNhat.Enabled = false;
            btnXoa.Enabled = false;
            //Đóng kết nối
            sqlConnection.Close();

        }

        private void lvCategory_SelectedItemsChanged(object sender, EventArgs e)
        {
            ListViewItem item = lvCategory.SelectedItems[0];

            txtID.Text = item.SubItems[0].Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[2].Text == "0" ? "Thức uống" : "Đồ ăn";

            btnCapNhat.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server =DESKTOP-L67JKQE\\SQLEXPRESS; database = Restaurant_Management; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand command = sqlConnection.CreateCommand();

            string name = txtName.Text;
            string type = txtType.Text == "Thức uống" ? "0" : "1";
            string id = txtID.Text;

            command.CommandText = "UPDATE Category SET Name = N'{name}', [Type] = {type} WHERE ID = {id}";
            sqlConnection.Open();
            int numOfRowsEffected = command.ExecuteNonQuery();

            if (numOfRowsEffected !=1)
            {
                MessageBox.Show("Có lỗi xảy ra");
                return;
            }
            ListViewItem item = lvCategory.SelectedItems[0];
            item.SubItems[1].Text = name;
            item.SubItems[2].Text = type;

            txtID.Text = "";
            txtName.Text = "";
            txtType.Text = "";

            btnCapNhat.Enabled = false;
            btnXoa.Enabled = false;

            sqlConnection.Close();
        }
    }
}
