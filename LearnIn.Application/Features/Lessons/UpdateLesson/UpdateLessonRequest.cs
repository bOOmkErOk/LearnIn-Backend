using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Lessons.UpdateLesson;

public record UpdateLessonRequest(
    int LessonId,
    string Title,
    string Description,
    string VideoUrl
) : IRequest<Result<int>>;