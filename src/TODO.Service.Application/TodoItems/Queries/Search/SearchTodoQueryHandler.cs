using Todo.Service.Application.Settings;
using Todo.Service.Application.TodoItems.Models;
using Todo.Service.Domain.Entities;

namespace Todo.Service.Application.TodoItems.Queries.Search;

public class SearchTodoItemsQueryHandler : IRequestHandler<SearchTodoItemsQuery, PageResponse<TodoItemSearchViewModel>>
{
    private static readonly int MAX_ITEMS_PER_PAGE = 50;

    private static readonly Dictionary<string, Func<bool, IQueryable<TodoItem>, IQueryable<TodoItem>>> _orders = new(StringComparer.InvariantCultureIgnoreCase)
    {
        { nameof(TodoItem.Title), (asc, query) => asc ? query.OrderBy(e => e.Title) : query.OrderByDescending(e => e.Title) },
        { nameof(TodoItem.Description), (asc, query) => asc ? query.OrderBy(e => e.Description) : query.OrderByDescending(e => e.Description) },
        { nameof(TodoItem.IsDone), (asc, query) => asc ? query.OrderBy(e => e.IsDone) : query.OrderByDescending(e => e.IsDone) },
        { nameof(TodoItem.DueDate), (asc, query) => asc ? query.OrderBy(e => e.DueDate) : query.OrderByDescending(e => e.DueDate) },
        { nameof(TodoItem.CreatedAt), (asc, query) => asc ? query.OrderBy(e => e.CreatedAt) : query.OrderByDescending(e => e.CreatedAt) },
        { nameof(TodoItem.UpdatedAt) , (asc, query) => asc ? query.OrderBy(e => e.UpdatedAt) : query.OrderByDescending(e => e.UpdatedAt) },
    };

    public static SearchOptions SearchOptions => new(MAX_ITEMS_PER_PAGE, _orders.Keys.ToList());

    private readonly ITodoContext _context;

    public SearchTodoItemsQueryHandler(ITodoContext context)
    {
        _context = context;
    }

    public async Task<PageResponse<TodoItemSearchViewModel>> Handle(SearchTodoItemsQuery request, CancellationToken cancellationToken)
    {
        request ??= new SearchTodoItemsQuery();

        var query = _context.TodoItems.AsQueryable();

        if (!request.Title.IsNullOrWhiteSpace())
        {
            query = query.Where(e => e.Title.Equals(request.Title));
        }

        if (!request.Description.IsNullOrWhiteSpace())
        {
            query = query.Where(e => e.Description.Equals(request.Description));
        }

        if (request.IsDone.HasValue)
        {
            query = query.Where(e => e.IsDone == request.IsDone);
        }

        if (request.DueDate.HasValue)
        {
            query = query.Where(e => e.DueDate == request.DueDate);
        }

        if (request.Active.HasValue)
        {
            query = query.Where(e => e.Active == request.Active);
        }

        var ordination = request.Ordination;

        var order = _orders.GetValueOrDefault(ordination.OrderBy ?? string.Empty, (asc, query) => query.OrderBy(e => e.CreatedAt));

        query = order(ordination.Asc, query);

        var pageRequest = request.PageRequest ?? PageRequest.First();

        var projection = query.ProjectToType<TodoItemSearchViewModel>();

        return await Pagination.Paginate(projection, pageRequest.WithMaxLimit(MAX_ITEMS_PER_PAGE));
    }
}