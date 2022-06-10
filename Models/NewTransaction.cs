using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFramewor_MVC.Models
{
    public partial class NewTransaction
    {
        public int TransactionId { get; set; }
        public int TransactionAccountNum { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime TransactionTime { get; set; }

        public virtual Account TransactionAccountNumNavigation { get; set; }
    }
}
