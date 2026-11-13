using LearnIn.Domain.Entities.Subscriptions;

namespace LearnIn.Api.Projection.GraphQL.Subscriptions;

public sealed class SubscriptionType : ObjectType<Subscription>
{
    protected override void Configure(IObjectTypeDescriptor<Subscription> descriptor)
    {
        descriptor.Field(s => s.Id);
        descriptor.Field(s => s.UserId);
        descriptor.Field(s => s.TeacherId);
        descriptor.Field(s => s.Teacher);
        descriptor.Field(s => s.StartDate);
        descriptor.Field(s => s.EndDate);
        descriptor.Field(s => s.PricePaid);
        descriptor.Field(s => s.IsCurrentlyActive);
    }
}