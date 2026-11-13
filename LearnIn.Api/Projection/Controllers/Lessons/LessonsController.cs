using LearnIn.Application.Features.Lessons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnIn.Api.Projection.Controllers.Lessons;

[Authorize]
public class LessonsController : BaseController
{
    private readonly IMediator _mediator;

    public LessonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddLesson(AddLessonRequest request)
    {
        var teacherId = GetCurrentUserId();

        var command = new AddLessonCommand(
            CourseId: request.CourseId,
            Title: request.Title,
            Description: request.Description,
            VideoUrl: request.VideoUrl,
            DurationSeconds: request.DurationSeconds,
            SequenceNumber: request.SequenceNumber,
            TeacherId: teacherId);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}