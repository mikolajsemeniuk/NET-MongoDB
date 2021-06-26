using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using app.Exceptions;
using app.Inputs;
using app.Payloads;
using MongoDB.Driver;
using OneOf;

namespace app.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerPayload>> GetCustomersAsync();
        Task<OneOf<CustomerPayload, InvalidIdException>> GetCustomerAsync(Guid id);
        Task<CustomerPayload> AddCustomerAsync(CustomerInput input);
        Task<OneOf<CustomerPayload, InvalidIdException>> UpdateCustomerAsync(Guid id, CustomerInput input);
        Task<OneOf<CustomerPayload, InvalidIdException>> RemoveCustomerAsync(Guid id);
    }
}