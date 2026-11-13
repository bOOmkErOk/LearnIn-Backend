using LearnIn.Domain.Entities.Courses;

namespace LearnIn.Api.Projection.GraphQL.Courses;

public sealed class CourseType : ObjectType<Course>
{
    protected override void Configure(IObjectTypeDescriptor<Course> descriptor)
    {
        descriptor.Field(c => c.Id);
        descriptor.Field(c => c.Title);
        descriptor.Field(c => c.Description);
        descriptor.Field(c => c.PromoVideoUrl);
        descriptor.Field(c => c.TeacherId);
        descriptor.Field(c => c.CreatedAt);
        descriptor.Field(c => c.Lessons);
        
        descriptor.Field("totalLessons")
            .Resolve(ctx =>
            {
                var course = ctx.Parent<Course>();
                return course.Lessons?.Count ?? 0;
            });
        
        descriptor.Field("totalDurationSeconds")
            .Resolve(ctx =>
            {
                var course = ctx.Parent<Course>();
                return course.Lessons?.Sum(l => l.DurationSeconds) ?? 0;
            });
    }
}