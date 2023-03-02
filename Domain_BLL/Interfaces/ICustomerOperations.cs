
using Data_PLL.Entities;
using Domain_BLL.Models;

namespace Domain_BLL.Interfaces
{
    public interface ICustomerOperations
    {
        Task<Customers> Login(string accountNumber, string pin);
        Task<CustomerViewModel> WithdrawAsync(Customers customer, decimal amount);
        Task CheckBalanceAsync(Customers customer);
        Task<CustomerViewModel> DepositAsync(Customers customer, decimal amount);
        Task<bool> TransferAsync(Customers customer, string recipientAccountNumber, decimal amount);
       
    }
}
