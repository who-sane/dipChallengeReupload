using System;

namespace Classes
{
    public class Order
    {
        public Product Prod { get; set; }
        public string OrderDate { get; set; }
        public string ShipDate { get; set; }
        public int Quantity { get; set; }
        public string CustID { get; set; }
        public string ShipMode { get; set; }

         public float findTotal(int q, float price) {
            var total = q * price;
            return total;
        }
        public float getGST(int q, float price) {
            return findTotal(q, price)/10;
        }

    }

    
}