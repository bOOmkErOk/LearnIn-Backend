using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Lessons.DeleteLesson;

public record DeleteLessonRequest(
    int LessonId
) : IRequest<Result<int>>;