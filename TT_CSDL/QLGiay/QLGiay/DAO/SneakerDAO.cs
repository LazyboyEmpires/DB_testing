using QLGiay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QLGiay.DAO
{
    public class SneakerDAO
    {
        private static SneakerDAO instance;

        public static SneakerDAO Instance
        {
            get { if (instance == null) instance = new SneakerDAO(); return SneakerDAO.instance; }
            private set { SneakerDAO.instance = value; }
        }

        private SneakerDAO() { }

        public List<Sneaker> GetSneakerByCategoryID(int id)
        {
            List<Sneaker> list = new List<Sneaker>();

            string query = "select * from Sneaker where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Sneaker sneaker = new Sneaker(item);
                list.Add(sneaker);
            }

            return list;
        }

        public List<Sneaker> GetListSneaker()
        {
            List<Sneaker> list = new List<Sneaker>();

            string query = "select * from Sneaker";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Sneaker sneaker = new Sneaker(item);
                list.Add(sneaker);
            }

            return list;
        }

        public List<Sneaker> SearchSneakerByName(string name)
        {

            List<Sneaker> list = new List<Sneaker>();

            string query = string.Format("SELECT * FROM dbo.Sneaker WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Sneaker sneaker = new Sneaker(item);
                list.Add(sneaker);
            }

            return list;
        }

        public bool InsertSneaker(string name, int id, float price)
        {
            string query = string.Format("INSERT dbo.Sneaker ( name, idCategory, price )VALUES  ( N'{0}', {1}, {2})", name, id, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateSneaker(int idSneaker, string name, int id, float price)
        {
            string query = string.Format("UPDATE dbo.Sneaker SET name = N'{0}', idCategory = {1}, price = {2} WHERE id = {3}", name, id, price, idSneaker);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteSneaker(int idSneaker)
        {
            BillInfoDAO.Instance.DeleteBillInfoBySneakerID(idSneaker);

            string query = string.Format("Delete Sneaker where id = {0}", idSneaker);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
