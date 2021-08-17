using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Models
{
    public class DisplayOrderSummaryModel
    {
        [Key]
        public Guid BookingId { get; set; }
        public string OrderId { get; set; }
        public string MovieName { get; set; }
        public int TicketQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string TransactionDetais { get; set; }
        public DateTime BookingDate { get; set; }

    }
}