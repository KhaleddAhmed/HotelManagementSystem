using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Dtos.Checkout
{
    public class GetCheckoutDto
    {
        public string GuestName { get; set; }
        public DateTime CheckoutDate { get; set; } = DateTime.Now;

        public double ServicesFees { get; set; }

        public double AdditionalFees { get; set; }

        public double TotalFees { get; set; }

        public bool IsPaid { get; set; } = false;
    }
}
