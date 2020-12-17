using QLGiay.DAO;
using QLGiay.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGiay
{
    public partial class frmRowManagement : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }
        public frmRowManagement(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadRow();
            LoadCategory();
            LoadComboboxRow(cbSwap);
        }

        #region Method

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadSneakerListByCategoryID(int id)
        {
            List<Sneaker> listSneaker = SneakerDAO.Instance.GetSneakerByCategoryID(id);
            cbSneaker.DataSource = listSneaker;
            cbSneaker.DisplayMember = "Name";
        }
        void LoadRow()
        {
            flpRows.Controls.Clear();

            List<Row> rowList = RowDAO.Instance.LoadRowList();

            foreach (Row item in rowList)
            {
                Button btn = new Button() { Width = RowDAO.RowWidth, Height = RowDAO.RowHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Empty":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flpRows.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            lvBill.Items.Clear();
            List<QLGiay.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByRow(id);
            float totalPrice = 0;
            foreach (QLGiay.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.SneakerName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");

            //Thread.CurrentThread.CurrentCulture = culture;

            txtTotalPrice.Text = totalPrice.ToString("c", culture);

        }

        void LoadComboboxRow(ComboBox cb)
        {
            cb.DataSource = RowDAO.Instance.LoadRowList();
            cb.DisplayMember = "Name";
        }

        #endregion


        #region Events

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheckOut_Click(this, new EventArgs());
        }

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddSneaker_Click(this, new EventArgs());
        }

        void btn_Click(object sender, EventArgs e)
        {
            int rowID = ((sender as Button).Tag as Row).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(rowID);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccountProfile f = new frmAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Account Information (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin();
            f.loginAccount = LoginAccount;
            f.InsertSneaker += f_InsertSneaker;
            f.DeleteSneaker += f_DeleteSneaker;
            f.UpdateSneaker += f_UpdateSneaker;
            f.ShowDialog();
        }

        void f_UpdateSneaker(object sender, EventArgs e)
        {
            LoadSneakerListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Row).ID);
        }

        void f_DeleteSneaker(object sender, EventArgs e)
        {
            LoadSneakerListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Row).ID);
            LoadRow();
        }

        void f_InsertSneaker(object sender, EventArgs e)
        {
            LoadSneakerListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Row).ID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadSneakerListByCategoryID(id);
        }

        private void btnAddSneaker_Click(object sender, EventArgs e)
        {
            Row row = lvBill.Tag as Row;

            if (row == null)
            {
                MessageBox.Show("Pick a Shelf, please");
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByRowID(row.ID);
            int sneakerID = (cbSneaker.SelectedItem as Sneaker).ID;
            int count = (int)nmTotal.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(row.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), sneakerID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, sneakerID, count);
            }

            ShowBill(row.ID);

            LoadRow();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Row row = lvBill.Tag as Row;

            int idBill = BillDAO.Instance.GetUncheckBillIDByRowID(row.ID);
            int discount = (int)nmDiscount.Value;

            double totalPrice = Convert.ToDouble(txtTotalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Do you really want to abate for {0}\nTotal Price - (Total Price / 100) x Discount\n=> {1} - ({1} / 100) x {2} = {3}", row.Name, totalPrice, discount, finalTotalPrice), "Caution", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    ShowBill(row.ID);

                    LoadRow();
                }
            }
        }
        private void btnSwitchRow_Click(object sender, EventArgs e)
        {

            int id1 = (lvBill.Tag as Row).ID;

            int id2 = (cbSwap.SelectedItem as Row).ID;
            if (MessageBox.Show(string.Format("Are you really want to switch from {0} to {1}", (lvBill.Tag as Row).Name, (cbSwap.SelectedItem as Row).Name), "Caution", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                RowDAO.Instance.SwitchRow(id1, id2);

                LoadRow();
            }
        }

        #endregion        


    }
}
