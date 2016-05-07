using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP.Models
{
    public enum Precision
    {
        Mk100,
        Mk200,
        Mk300
    }

    public enum Color
    {
        White,
        Black,
        Yellow,
        Red,
        Green,
        Blue
    }

    public enum Material
    {
        ABS,
        PLA
    }

    public enum Status
    {
        Pending,
        Confirmed,
        In_progress,
        Printed,
        Shipped,
        Delivered
    }
}