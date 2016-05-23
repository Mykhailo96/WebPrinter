using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WP.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public int Price { get; set; }

        [Display(Name = "Precision")]
        public Precision ObjectPrecision { get; set; }

        [Display(Name = "Color")]
        public Color ObjecttColor { get; set; }

        [Display(Name = "Material")]
        public Material ObjectMaterial { get; set; }

        [Display(Name = "Status")]
        public Status OrderStatus { get; set; }

        [Display(Name = "Order number")]
        public int OrderNumber { get; set; }

        [Display(Name = "File")]
        public string FileName { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class ValidateFileAttribute :RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return false;
            }

            return true;
        }
    }
}