using LearnIn.Application.Features.Subscriptions.CreateSubscription;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LearnIn.Api.Projection.Controllers.Subscriptions;

[Authorize]
[ApiController]
[Route("api/[controller]")]

[Authorize]
public class SubscriptionsController : BaseController
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("create")]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        var studentId = GetCurrentUserId();

        var command = new CreateSubscriptionCommand(
            UserId: studentId,
            TeacherId: request.TeacherId,
            DurationMonths: request.DurationMonths,
            PricePaid: request.PricePaid
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}