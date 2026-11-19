using LearnIn.Domain.Entities.Users;

namespace LearnIn.Api.Projection.GraphQL.Users;

public sealed class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(u => u.Id);
        descriptor.Field(u => u.FirstName);
        descriptor.Field(u => u.LastName);
        descriptor.Field(u => u.Email);
        descriptor.Field(u => u.Role);
        descriptor.Field(u => u.CreatedCourses);

        descriptor.Ignore(u => u.Password);
        descriptor.Ignore(u => u.Balance);
        descriptor.Ignore(u => u.Subscriptions);
    }
}