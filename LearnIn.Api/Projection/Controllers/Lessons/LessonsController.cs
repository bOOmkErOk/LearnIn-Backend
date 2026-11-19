using LearnIn.Application.Features.Lessons.AddLesson;
using LearnIn.Application.Features.Lessons.DeleteLesson;
using LearnIn.Application.Features.Lessons.UpdateLesson;
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
    public async Task<IActionResult> AddLesson(AddLessonRequest req)
    {
        var teacherId = GetCurrentUserId();

        var command = new AddLessonCommand(
            CourseId: req.CourseId,
            Title: req.Title,
            Description: req.Description,
            VideoUrl: req.VideoUrl,
            DurationSeconds: req.DurationSeconds,
            SequenceNumber: req.SequenceNumber,
            TeacherId: teacherId);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteLesson(DeleteLessonRequest req)
    {
        var teacherId = GetCurrentUserId();

        var command = new DeleteLessonCommand(
            LessonId: req.LessonId,
            TeacherId: teacherId
        );

        var result = await _mediator.Send(command);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("update")]

    public async Task<IActionResult> UpdateLesson(UpdateLessonRequest req)
    {
        var teacherId = GetCurrentUserId();

        var command = new UpdateLessonCommand(
            LessonId: req.LessonId,
            Title: req.Title,
            Description: req.Description,
            VideoUrl: req.VideoUrl,
            TeacherId: teacherId
        );
        
        var result = await _mediator.Send(command);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

    }
}