using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using app.Inputs;
using app.Interfaces;
using app.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository) =>
            _repository = repository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPayload>>> GetCustomersAsync() =>
            Ok(await _repository.GetCustomersAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerPayload>> GetCustomerAsync(Guid id)
        {
            var customer = await _repository.GetCustomerAsync(id);
            return customer.Match<ActionResult>(customer => Ok(customer),
                invalidId => NotFound(invalidId.Message));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerPayload>> AddCustomerAsync([FromBody] CustomerInput input) =>
            Ok(await _repository.AddCustomerAsync(input));

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerPayload>> UpdateCustomerAsync([FromRoute] Guid id, [FromBody] CustomerInput input)
        {
            var customer = await _repository.UpdateCustomerAsync(id, input);
            return customer.Match<ActionResult>(customer => Ok(customer),
                invalidId => NotFound(invalidId.Message));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerPayload>> RemoveCustomerAsync(Guid id)
        {
            var customer = await _repository.RemoveCustomerAsync(id);
            return customer.Match<ActionResult>(customer => Ok(customer),
                invalidId => NotFound(invalidId.Message));
        }
    }
}