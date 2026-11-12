using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Subscriptions.CreateSubscription;

public record CreateSubscriptionCommand(
    int UserId, 
    int TeacherId, 
    int DurationMonths, 
    decimal PricePaid) : IRequest<Result<bool>>;