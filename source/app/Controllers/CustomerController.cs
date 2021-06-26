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
    }
}