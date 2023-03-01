using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_PLL;
using Data_PLL.Entities;

namespace Domain_BLL.Models
{
    public class CustomerViewModel
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string Pin { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<TransactionHistoryViewModel> TransactionHistories { get; set; }
    }

    public class TransactionHistoryViewModel
    {
       
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

   

}
