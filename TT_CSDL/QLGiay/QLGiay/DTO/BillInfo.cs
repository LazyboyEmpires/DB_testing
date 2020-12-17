using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGiay.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billID, int sneakerID, int count)
        {
            this.ID = id;
            this.BillID = billID;
            this.SneakerID = sneakerID;
            this.Count = count;
        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["idbill"];
            this.SneakerID = (int)row["idSneaker"];
            this.Count = (int)row["count"];
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private int sneakerID;

        public int SneakerID
        {
            get { return sneakerID; }
            set { sneakerID = value; }
        }

        private int billID;

        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
