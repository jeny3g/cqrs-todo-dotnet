using MediatR;
using Todo.Service.Application.TodoItems.Models;
using Todo.Service.Application.TodoItems.Queries.Get;

namespace Todo.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemController : ControllerBase
{
    protected readonly IMediator _mediator;

    public TodoItemController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<TodoItemViewModel>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<List<TodoItemViewModel>>> ListAll()
    {
        return Ok(await _mediator.Send(new ListTodoItemQuery()));
    }
}
