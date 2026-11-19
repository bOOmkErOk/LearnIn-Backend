using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Courses.DeleteCourse;

public record DeleteCourseRequest(
    int CourseId
    ) : IRequest<Result<int>>;