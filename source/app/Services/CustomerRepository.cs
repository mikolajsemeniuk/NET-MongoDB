using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.Exceptions;
using app.Inputs;
using app.Interfaces;
using app.Models;
using app.Payloads;
using MongoDB.Driver;
using OneOf;

namespace app.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        private readonly FilterDefinitionBuilder<Customer> _filter = Builders<Customer>.Filter;

        public CustomerRepository(DataContext context) =>
            _context = context;

        public async Task<IEnumerable<CustomerPayload>> GetCustomersAsync() =>
            await _context.Customers
                .Find(_filter.Empty)
                .Project(customer => new CustomerPayload
                {
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    Surname = customer.Surname
                })
                .ToListAsync();

        public async Task<OneOf<CustomerPayload, InvalidIdException>> GetCustomerAsync(Guid id)
        {
            var customer = await _context.Customers
                .Find(_filter.Eq(customer => customer.CustomerId, id))
                .Project(customer => new CustomerPayload
                {
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    Surname = customer.Surname
                })
                .FirstOrDefaultAsync();
            if (customer == null)
                return new InvalidIdException();
            return customer;
        } 

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

        public async Task<OneOf<CustomerPayload, InvalidIdException>> UpdateCustomerAsync(Guid id, CustomerInput input)
        {
            var filter = _filter.Eq(customer => customer.CustomerId, id);
            var customer = await _context.Customers
                .Find(filter)
                .FirstOrDefaultAsync();

            if (customer == null)
                return new InvalidIdException();

            customer.Name = input.Name;
            customer.Surname = input.Surname;
            await _context.Customers.ReplaceOneAsync(filter, customer);
            return new CustomerPayload
            {
                Id = customer.CustomerId,
                Name = customer.Name,
                Surname = customer.Surname
            };
        }

        public async Task<OneOf<CustomerPayload, InvalidIdException>> RemoveCustomerAsync(Guid id)
        {
            var filter = _filter.Eq(customer => customer.CustomerId, id); 
            var customer = await _context.Customers
                .Find(filter)
                .FirstOrDefaultAsync();
            if (customer == null)
                return new InvalidIdException();
            await _context.Customers.DeleteOneAsync(filter);
            return new CustomerPayload
            {
                Id = customer.CustomerId,
                Name = customer.Name,
                Surname = customer.Surname
            };
        }
    }
}