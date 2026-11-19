using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Courses.DeleteCourse;

public record DeleteCourseCommand(
    int CourseId,
    int TeacherId
    ) : IRequest<Result<int>>;