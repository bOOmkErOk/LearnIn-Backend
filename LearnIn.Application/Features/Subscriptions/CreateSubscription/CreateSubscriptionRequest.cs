namespace LearnIn.Application.Features.Subscriptions.CreateSubscription;

public record CreateSubscriptionRequest(
    int TeacherId, 
    int DurationMonths, 
    decimal PricePaid);