using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnIn.Application.Features.Courses;
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

    
}