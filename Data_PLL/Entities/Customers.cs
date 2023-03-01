

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_PLL.Entities
{
    public class Customers : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }

        [StringLength(4, ErrorMessage = "Pin shouldn't be more than 4 digits!")]
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }



        public IEnumerable<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();

    }
}
