using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public Precision ObjectPrecision { get; set; }
        public Color ObjecttColor { get; set; }
        public Material ObjectMaterial { get; set; }
        public Status OrderStatus { get; set; }
        public int OrderNumber { get; set; }
        public string Address { get; set; }

        public string FileName { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}