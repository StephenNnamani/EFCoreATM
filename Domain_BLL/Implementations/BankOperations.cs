using Data_PLL;
using Data_PLL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain_BLL.Implementations
{
    public class BankOperations
    {
        private readonly AtmDbContextFactory _atmDb;


    
        public BankOperations()
        {
            _atmDb = new AtmDbContextFactory();
        }

       
    }
}
