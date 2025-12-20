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

        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> Create([FromBody] CreateSubscriptionDto dto)
        {
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

        [HttpGet]
        public async Task<ActionResult<List<SubscriptionDto>>> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            var dtos = subscriptions.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubscriptionDto>> GetById([FromRoute(Name = "id")] Guid subscriptionId)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
            if (subscription is null)
                return NotFound();

            return Ok(MapToDto(subscription));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid subscriptionId)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
            if (subscription is null)
                return NotFound();

            await _subscriptionService.DeleteSubscriptionAsync(subscriptionId);
            return NoContent();
        }

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
