using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFoodAppUI.Views
{
    public class Receipt
    {
        public int MAHD { get; set; }
        public int MAND { get; set; }
        public float TONGTIEN { get; set; }
        public DateTime? TGDAT { get; set; }
        public Boolean GIAOHANG { get; set; }
        public DateTime? TGGIAO { get; set; }
        public float SHIP { get; set; }
    }
}
