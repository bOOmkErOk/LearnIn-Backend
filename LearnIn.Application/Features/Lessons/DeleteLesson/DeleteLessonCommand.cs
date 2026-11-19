using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Lessons.DeleteLesson;

public record DeleteLessonCommand(
    int LessonId,
    int TeacherId
    ) : IRequest<Result<int>>;