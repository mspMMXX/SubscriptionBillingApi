using Microsoft.AspNetCore.Mvc;
using SubscriptionBillingApi.Services;
using SubscriptionBillingApi.DTOs.Customers;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var dtos = customers.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> GetById([FromRoute] Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if(customer is null)
                return NotFound();

            return Ok(MapToDto(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
        {
            var customer = new Customer(
                dto.CustomerNumber,
                dto.LastName,
                dto.FirstName,
                dto.Phone,
                dto.Email
            );

            await _customerService.CreateCustomerAsync(customer);
            var responseDto = MapToDto(customer);

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.Id },
                responseDto);
        }

        public static CustomerDto MapToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                CustomerNumber = customer.CustomerNumber,
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Phone = customer.Phone,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
