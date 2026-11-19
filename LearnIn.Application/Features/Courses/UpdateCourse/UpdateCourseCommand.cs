
using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Courses.UpdateCourse;

public record UpdateCourseCommand(
    int CourseId,
    string CourseTitle,
    string CourseDescription,
    string PromoVideoUrl,
    int TeacherId
    ) : IRequest<Result<int>>;