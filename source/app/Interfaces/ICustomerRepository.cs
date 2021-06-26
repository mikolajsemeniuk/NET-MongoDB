using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using app.Inputs;
using app.Payloads;

namespace app.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerPayload> AddCustomerAsync(CustomerInput input);
        Task<CustomerPayload> GetCustomerAsync(Guid id);
        Task<IEnumerable<CustomerPayload>> GetCustomersAsync();
        Task RemoveCustomerAsync(Guid id);
        Task<CustomerPayload> UpdateCustomerAsync(Guid id, CustomerInput input);
    }
}