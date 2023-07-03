using Todo.Service.Application.TodoItems.Models;
using Todo.Service.Application.TodoItems.Queries.Get;
using Todo.Service.Application.TodoItems.Queries.Search;

namespace Todo.Service.Api.Controllers;

public class TodoItemController : BaseController
{
    public TodoItemController(IMediator mediator) : base(mediator) { }


    [HttpGet("searchOptions")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchOptions))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public ActionResult<SearchOptions> GetOrderOptions() => Ok(SearchTodoItemsQueryHandler.SearchOptions);

    [HttpGet("search")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PageResponse<TodoItemSearchViewModel>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PageResponse<TodoItemSearchViewModel>>> Search(
    [FromQuery] SearchTodoItems model,
    [FromQuery] PageRequestDto request,
    [FromQuery] OrdinationDto ordination)
    {
        var query = model.Adapt<SearchTodoItemsQuery>();
        query.PageRequest = PageRequest.Of(request.Number, request.Limit);
        query.Ordination = ordination.Adapt<Ordination>();

        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TodoItemViewModel))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<TodoItemViewModel>> Get(Guid id)
    {
        return Ok(await _mediator.Send(new GetTodoItemQuery()
        {
            Id = id
        }));
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
