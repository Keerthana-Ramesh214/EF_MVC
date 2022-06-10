using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFramewor_MVC.Models
{
    public partial class Account
    {
        public Account()
        {
            NewTransactions = new HashSet<NewTransaction>();
        }

        public int AccountNo { get; set; }
        public string AccountHolderName { get; set; }
        public double BalanceAmount { get; set; }
        public DateTime Doc { get; set; }
        public string AccountType { get; set; }

        public virtual ICollection<NewTransaction> NewTransactions { get; set; }
    }
}
