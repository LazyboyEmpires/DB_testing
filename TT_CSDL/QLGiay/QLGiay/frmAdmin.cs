using QLGiay.DAO;
using QLGiay.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGiay
{
    public partial class frmAdmin : Form
    {
        BindingSource sneakerList = new BindingSource();

        BindingSource accountList = new BindingSource();

        public Account loginAccount;
        public frmAdmin()
        {
            InitializeComponent();
            LoadData();
        }

        #region methods

        List<Sneaker> SearchSneakerByName(string name)
        {
            List<Sneaker> listSneaker = SneakerDAO.Instance.SearchSneakerByName(name);

            return listSneaker;
        }
        void LoadData()
        {
            dgvSneaker.DataSource = sneakerList;
            dgvAcc.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpFromDate.Value, dtpToDate.Value);
            LoadListSneaker();
            LoadAccount();
            LoadCategoryIntoCombobox(cbSneakerCategory);
            AddSneakerBinding();
            AddAccountBinding();
        }

        void AddAccountBinding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", dgvAcc.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dgvAcc.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dgvAcc.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void AddSneakerBinding()
        {
            txtSneakerName.DataBindings.Add(new Binding("Text", dgvSneaker.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtSneakerID.DataBindings.Add(new Binding("Text", dgvSneaker.DataSource, "ID", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dgvSneaker.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListSneaker()
        {
            sneakerList.DataSource = SneakerDAO.Instance.GetListSneaker();
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Account added successfully");
            }
            else
            {
                MessageBox.Show("Account add failed");
            }

            LoadAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Successful account update");
            }
            else
            {
                MessageBox.Show("Account update failed");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("You can not delete yourself");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Account deleted successfully");
            }
            else
            {
                MessageBox.Show("Account deletion failed");
            }

            LoadAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Password reset was successful");
            }
            else
            {
                MessageBox.Show("Password reset failed");
            }
        }
        #endregion

        #region events
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }


        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            ResetPass(userName);
        }


        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }


        private void btnSearchSneaker_Click(object sender, EventArgs e)
        {
            sneakerList.DataSource = SearchSneakerByName(txtSearchSneakerName.Text);
        }
        private void txbID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvSneaker.SelectedCells.Count > 0)
                {
                    int id = (int)dgvSneaker.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbSneakerCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbSneakerCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbSneakerCategory.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnAddSneaker_Click(object sender, EventArgs e)
        {
            string name = txtSneakerName.Text;
            int categoryID = (cbSneakerCategory.SelectedItem as Category).ID;
            float price = (float)nmSneakerPrice.Value;

            if (SneakerDAO.Instance.InsertSneaker(name, categoryID, price))
            {
                MessageBox.Show("Successfully added Sneaker");
                LoadListSneaker();
                if (insertSneaker != null)
                    insertSneaker(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("There was an error adding Sneaker");
            }
        }

        private void btnEditSneaker_Click(object sender, EventArgs e)
        {
            string name = txtSneakerName.Text;
            int categoryID = (cbSneakerCategory.SelectedItem as Category).ID;
            float price = (float)nmSneakerPrice.Value;
            int id = Convert.ToInt32(txtSneakerID.Text);

            if (SneakerDAO.Instance.UpdateSneaker(id, name, categoryID, price))
            {
                MessageBox.Show("Successfully repaired");
                LoadListSneaker();
                if (updateSneaker != null)
                    updateSneaker(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("There was an error fixing the S");
            }
        }

        private void btnDeleteSneaker_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtSneakerID.Text);

            if (SneakerDAO.Instance.DeleteSneaker(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListSneaker();
                if (deleteSneaker != null)
                    deleteSneaker(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }
        private void btnShowSneaker_Click(object sender, EventArgs e)
        {
            LoadListSneaker();
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpFromDate.Value, dtpToDate.Value);
        }

        private event EventHandler insertSneaker;
        public event EventHandler InsertSneaker
        {
            add { insertSneaker += value; }
            remove { insertSneaker -= value; }
        }

        private event EventHandler deleteSneaker;
        public event EventHandler DeleteSneaker
        {
            add { deleteSneaker += value; }
            remove { deleteSneaker -= value; }
        }

        private event EventHandler updateSneaker;
        public event EventHandler UpdateSneaker
        {
            add { updateSneaker += value; }
            remove { updateSneaker -= value; }
        }

        #endregion              

        private void btnFristBillPage_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnPrevioursBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);

            if (page > 1)
                page--;

            txbPageBill.Text = page.ToString();
        }

        private void btnNextBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpFromDate.Value, dtpToDate.Value);

            if (page < sumRecord)
                page++;

            txbPageBill.Text = page.ToString();

        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpFromDate.Value, dtpToDate.Value);

            int lastPage = sumRecord / 10;

            if (sumRecord % 10 != 0)
                lastPage++;

            txbPageBill.Text = lastPage.ToString();
        }
    }
}
