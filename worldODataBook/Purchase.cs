using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class Purchase
    {
        private int id;
        private int book_id;
        private int customer_id;
        private string purchase_date;

        public int ID { get => id; set => id = value; }
        public int Book_id { get => book_id; set => book_id = value; }
        public int Customer_id { get => customer_id; set => customer_id = value; }
        public string Purchase_date { get => purchase_date; set => purchase_date = value; }

        public Purchase(int id, int book_id, int customer_id, string purchase_date)
        {
            this.ID = id;
            this.book_id = book_id;
            this.customer_id = customer_id;
            this.purchase_date = purchase_date;
        }

        public Purchase(int book_id, int customer_id, string purchase_date)
        {
            this.ID = 0;
            this.book_id = book_id;
            this.customer_id = customer_id;
            this.purchase_date = purchase_date;
        }

        public override string ToString()
        {
            return $"{ID}.{book_id} {customer_id}   {purchase_date}";
        }
    }
}
