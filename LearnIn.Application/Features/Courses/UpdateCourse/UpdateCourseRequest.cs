using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Courses.UpdateCourse;

public record UpdateCourseRequest(
    int CourseId,
    string CourseTitle,
    string CourseDescription,
    string PromoVideoUrl
) : IRequest<Result<int>>;