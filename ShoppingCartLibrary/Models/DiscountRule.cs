using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCartLibrary.Models
{
    public class DiscountRule
    {
        public double Discount { get; set; }
        
        public int DifferentItemNumber { get; set; }
    }
}
