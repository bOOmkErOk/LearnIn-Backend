using LearnIn.Application.Features.Users.BecomeTeacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace LearnIn.Api.Projection.Controllers.Users;

[Authorize]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("become-teacher")]
    public async Task<IActionResult> BecomeTeacher()
    {
        var userId = GetCurrentUserId();

        var command = new BecomeTeacherCommand(userId);

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.Error!.Status, result.Error);
    }
}