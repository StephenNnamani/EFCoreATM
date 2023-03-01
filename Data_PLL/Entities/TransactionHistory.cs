using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_PLL.Entities
{
    public class TransactionHistory : BaseEntity
    {
        public decimal Balance { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Remark { get; set; }
        public int CustomersId { get; set; }


        public Customers CustomersNavigation { get; set; }

    }
}
