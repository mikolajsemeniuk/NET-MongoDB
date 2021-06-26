using System.Collections.Generic;
using System.Threading.Tasks;
using app.Inputs;
using app.Payloads;

namespace app.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerPayload> AddCustomerAsync(CustomerInput input);
        Task<IEnumerable<CustomerPayload>> GetCustomersAsync();
    }
}