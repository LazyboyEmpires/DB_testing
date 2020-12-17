using QLGiay.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGiay.DAO
{
    public class RowDAO
    {
        private static RowDAO instance;

        public static RowDAO Instance
        {
            get { if (instance == null) instance = new RowDAO(); return RowDAO.instance; }
            private set { RowDAO.instance = value; }
        }

        public static int RowWidth = 90;
        public static int RowHeight = 90;

        private RowDAO() { }

        public void SwitchRow(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchTabel @idRow1 , @idTabel2", new object[] { id1, id2 });
        }

        public List<Row> LoadRowList()
        {
            List<Row> tableList = new List<Row>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetRowList");

            foreach (DataRow item in data.Rows)
            {
                Row table = new Row(item);
                tableList.Add(table);
            }

            return tableList;
        }
    }
}
