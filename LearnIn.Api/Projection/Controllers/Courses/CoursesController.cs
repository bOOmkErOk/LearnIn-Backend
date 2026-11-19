using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnIn.Application.Features.Courses.CreateCourse;
using LearnIn.Application.Features.Courses.DeleteCourse;
using LearnIn.Application.Features.Courses.UpdateCourse;

namespace LearnIn.Api.Projection.Controllers.Courses;

[Authorize] 
public class CoursesController : BaseController
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request)
    {
        var teacherId = GetCurrentUserId();

        var command = new CreateCourseCommand(
            Title: request.Title,
            Description: request.Description,
            PromoVideoUrl: request.PromoVideoUrl,
            TeacherId: teacherId
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCourse(DeleteCourseRequest req)
    {
        var teacherId = GetCurrentUserId();

        var command = new DeleteCourseCommand(
            CourseId: req.CourseId,
            TeacherId: teacherId
        );

        var result = await _mediator.Send(command);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("update")]

    public async Task<IActionResult> UpdateCourse(UpdateCourseRequest req)
    {
        var teacherId = GetCurrentUserId();

        var command = new UpdateCourseCommand(
            CourseId: req.CourseId,
            CourseTitle: req.CourseTitle,
            CourseDescription: req.CourseDescription,
            PromoVideoUrl: req.PromoVideoUrl,
            TeacherId: teacherId
        );
        
        var result = await _mediator.Send(command);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

    }

    
}