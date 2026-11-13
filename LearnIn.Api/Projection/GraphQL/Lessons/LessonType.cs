using LearnIn.Domain.Entities.Courses;

namespace LearnIn.Api.Projection.GraphQL.Lessons;

public sealed class LessonType : ObjectType<Lesson>
{
    protected override void Configure(IObjectTypeDescriptor<Lesson> descriptor)
    {
        descriptor.Field(l => l.Id);
        descriptor.Field(l => l.Title);
        descriptor.Field(l => l.Description);
        descriptor.Field(l => l.VideoUrl);
        descriptor.Field(l => l.SequenceNumber);
        descriptor.Field(l => l.DurationSeconds);
        descriptor.Field(l => l.CourseId);
    }
}