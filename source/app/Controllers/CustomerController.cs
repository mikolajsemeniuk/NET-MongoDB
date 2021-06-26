using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using app.Inputs;
using app.Interfaces;
using app.Payloads;
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

        [HttpPost]
        public async Task<ActionResult<CustomerPayload>> AddCustomerAsync([FromBody] CustomerInput input) =>
            Ok(await _repository.AddCustomerAsync(input));

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPayload>> GetCustomerAsync(Guid id) =>
            Ok(await _repository.GetCustomerAsync(id));

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerPayload>> AddCustomerAsync([FromRoute] Guid id, [FromBody] CustomerInput input) =>
            Ok(await _repository.UpdateCustomerAsync(id, input));

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerPayload>> RemoveCustomerAsync(Guid id)
        {
            await _repository.RemoveCustomerAsync(id);
            return Ok();
        }
    }
}