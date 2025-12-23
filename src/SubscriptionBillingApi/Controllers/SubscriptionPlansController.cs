using Microsoft.AspNetCore.Mvc;
using SubscriptionBillingApi.Services;
using SubscriptionBillingApi.DTOs.SubscriptionPlans;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlansController : ControllerBase
    {
        private readonly SubscriptionPlanService _subscriptionPlanService;

        public SubscriptionPlansController(SubscriptionPlanService subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }

        /// <summary>
        /// Creates a new subscription plan and returns the created resource (201).
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SubscriptionPlanDto>> Create([FromBody] CreateSubscriptionPlanDto dto)
        {
            // Create domain entity from request DTO
            var subscriptionPlan = new SubscriptionPlan(
                dto.Name, 
                dto.Price, 
                dto.Currency, 
                dto.BillingInterval
                );
            await _subscriptionPlanService.CreateSubscriptionPlanAsync(subscriptionPlan);
            var responseDto = MapToDto(subscriptionPlan);

            return CreatedAtAction(nameof(GetById), new { id = subscriptionPlan.Id }, responseDto);
        }

        /// <summary>
        /// Returns all subscription plans.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<SubscriptionPlanDto>>> GetAll()
        {
            var subscriptionPlans = await _subscriptionPlanService.GetAllSubscriptionPlansAsync();
            var dtos = subscriptionPlans.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        /// <summary>
        /// Returns a single subscription plan by id, or 404 if it does not exist.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubscriptionPlanDto>> GetById([FromRoute(Name = "id")] Guid subscriptionPlanId)
        {
            var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlanByIdAsync(subscriptionPlanId);
            if (subscriptionPlan is null)
                return NotFound();

            return Ok(MapToDto(subscriptionPlan));
        }

        /// <summary>
        /// Deletes a subscription plan by id.
        /// Returns 204 on success or 404 if it does not exist.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid subscriptionPlanId)
        {
            var subscriptionPlan = await _subscriptionPlanService.GetSubscriptionPlanByIdAsync(subscriptionPlanId);
            if (subscriptionPlan is null)
                return NotFound();

            await _subscriptionPlanService.DeleteSubscriptionPlanAsync(subscriptionPlanId);
            return NoContent();
        }

        /// <summary>
        /// Maps the subscription plan domain entity to an API DTO.
        /// </summary>
        private static SubscriptionPlanDto MapToDto(SubscriptionPlan subscriptionPlan)
        {
            return new SubscriptionPlanDto
            {
                Id = subscriptionPlan.Id,
                Name = subscriptionPlan.Name,
                Price = subscriptionPlan.Price,
                Currency = subscriptionPlan.Currency,
                BillingInterval = subscriptionPlan.BillingInterval,
                IsActive = subscriptionPlan.IsActive
            };
        }
    }
}
