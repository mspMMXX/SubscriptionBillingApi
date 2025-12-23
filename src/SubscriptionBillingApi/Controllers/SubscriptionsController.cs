using Microsoft.AspNetCore.Mvc;
using SubscriptionBillingApi.Services;
using SubscriptionBillingApi.DTOs.Subscriptions;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionsController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// Creates a new subscription for a customer and a subscription plan.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> Create([FromBody] CreateSubscriptionDto dto)
        {
            // Create the subscription domain entity from the request DTO
            var subscription = new Subscription(
                dto.CustomerId, 
                dto.PlanId, 
                dto.StartDate
                );
            await _subscriptionService.CreateSubscriptionAsync(subscription);
            var responseDto = MapToDto(subscription);

            return CreatedAtAction(
                nameof(GetById),
                new { id = subscription.Id },
                responseDto);
        }

        /// <summary>
        /// Returns all subscriptions.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<SubscriptionDto>>> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            var dtos = subscriptions.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        /// <summary>
        /// Returns a single subscription by id, or 404 if it does not exist.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubscriptionDto>> GetById([FromRoute(Name = "id")] Guid subscriptionId)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
            if (subscription is null)
                return NotFound();

            return Ok(MapToDto(subscription));
        }

        /// <summary>
        /// Deletes a subscription by id.
        /// Returns 204 on success or 404 if it does not exist.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid subscriptionId)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
            if (subscription is null)
                return NotFound();

            await _subscriptionService.DeleteSubscriptionAsync(subscriptionId);
            return NoContent();
        }

        /// <summary>
        /// Maps the subscription domain entity to an API DTO.
        /// </summary>
        private static SubscriptionDto MapToDto(Subscription subscription)
        {
            return new SubscriptionDto
            {
                Id = subscription.Id,
                CustomerId = subscription.CustomerId,
                PlanId = subscription.PlanId,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                Status = subscription.Status,
                NextBillingDate = subscription.NextBillingDate,
                CancelDate = subscription.CancelDate
            };
        }
    }
}
