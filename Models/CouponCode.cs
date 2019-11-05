using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models {

    public class CouponCode {
        public long Id { get; set; }
        public string Code { get; set; }
        public long MaxAmount { get; set; }
        public long MinAmount { get; set; }
        public long Discount { get; set; }
        public decimal PctDiscount { get; set; }
        public DateTime ValidTill { get; set; }
        public Status Status { get; set; }

        public long CityId { get; set; }
        public City City { get; set; }
    }

}