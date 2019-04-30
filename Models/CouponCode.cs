using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{

    public class CouponCode
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long Discount { get; set; }
        public decimal PctDiscount { get; set; }
        public DateTime ValidTill { get; set; }
        public Status Status { get; set; }
    }

}