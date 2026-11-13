using LearnIn.Api.Projection.GraphQL.Courses;
using LearnIn.Api.Projection.GraphQL.Lessons;
using LearnIn.Api.Projection.GraphQL.Subscriptions;
using LearnIn.Api.Projection.GraphQL.Users;
using LearnIn.Domain.Enums.Users;

namespace LearnIn.Api.Extensions.GraphQL;

public static class GraphqlExtensions
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType()
            .AddAuthorization()
            .AddFiltering()
            .AddProjections()
            .AddSorting()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .AddType(new EnumType<UserEnums>(d => d.Name("UserRole")))
            .BindRuntimeType<UserEnums, StringType>()
            .AddTypeExtension<CourseQueries>()
            .AddTypeExtension<UserQueries>()
            .AddTypeExtension<SubscriptionQueries>()
            .AddType<CourseType>()
            .AddType<LessonType>()
            .AddType<UserType>()
            .AddType<SubscriptionType>();

        return services;
    }
}