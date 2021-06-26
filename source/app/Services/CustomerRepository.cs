using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.Inputs;
using app.Interfaces;
using app.Models;
using app.Payloads;
using MongoDB.Driver;

namespace app.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        private readonly FilterDefinitionBuilder<Customer> filter = Builders<Customer>.Filter;

        public CustomerRepository(DataContext context) =>
            _context = context;

        public async Task<IEnumerable<CustomerPayload>> GetCustomersAsync() =>
            await _context.Customers
                .Find(filter.Empty)
                .Project(customer => new CustomerPayload
                {
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    Surname = customer.Surname
                })
                .ToListAsync();

        public async Task<CustomerPayload> AddCustomerAsync(CustomerInput input)
        {
            var customer = new Customer
            {
                Name = input.Name,
                Surname = input.Surname
            };
            await _context.Customers.InsertOneAsync(customer);
            return new CustomerPayload
            {
                Id = customer.CustomerId,
                Name = customer.Name,
                Surname = customer.Surname
            };
        }
    }
}