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

        /// <summary>
        /// Creates a new customer and returns the created resource (201) including its location.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
        {
            // Create the domain entity from the incoming request DTO
            var customer = new Customer(
                dto.CustomerNumber,
                dto.LastName,
                dto.FirstName,
                dto.Phone,
                dto.Email
                );

            // Persist via service layer (business/persistence coordination)
            await _customerService.CreateCustomerAsync(customer);
            // Return a DTO (do not expose domain entities directly)
            var responseDto = MapToDto(customer);

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.Id },
                responseDto);
        }

        /// <summary>
        /// Returns all customers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var dtos = customers.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        /// <summary>
        /// Returns a single customer by id, or 404 if it does not exist.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> GetById([FromRoute(Name = "id")] Guid customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if(customer is null)
                return NotFound();

            return Ok(MapToDto(customer));
        }

        /// <summary>
        /// Deletes a customer by id. Returns 204 on success, or 404 if it does not exist.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid customerId)
        {
            // Explicit existence check to return a clean 404 instead of silently doing nothing
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer is null)
                return NotFound();

            await _customerService.DeleteCustomerAsync(customerId);
            return NoContent();
        }

        /// <summary>
        /// Maps the domain entity to an API DTO.
        /// Kept as a static helper to ensure consistent output formatting.
        /// </summary>
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
