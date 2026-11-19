using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Courses.CreateCourse;

public record CreateCourseCommand(string Title, string Description, string PromoVideoUrl, int TeacherId) : IRequest<Result<int>>;