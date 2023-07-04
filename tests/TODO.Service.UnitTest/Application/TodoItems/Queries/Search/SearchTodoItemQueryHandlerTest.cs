using Todo.Service.Application.TodoItems.Queries.Search;
using Todo.Service.Persistence;

namespace Todo.Service.UnitTest.Application.TodoItems.Queries.Search;

public class SearchTodoItemQueryHandlerTest
{
    private static readonly int MAX_ITEMS_PER_PAGE = 50;

    private ITodoContext _context;

    private TodoItem _item;

    public SearchTodoItemQueryHandlerTest()
    {
        SetupDependencies();
        InitializeTestData();
    }

    private void SetupDependencies()
    {
        _context = Substitute.For<ITodoContext>();
    }

    private void InitializeTestData()
    {
        var set = new List<TodoItem>() { _item }.AsDbSet();

        _context.TodoItems.Returns(set);
    }

    [Fact]
    public async Task Must_Return_Search_Options()
    {
        var expectedMaxLimit = MAX_ITEMS_PER_PAGE;
        var expectedOrderFields = new List<string>
        {
            nameof(TodoItem.Title),
            nameof(TodoItem.Description),
            nameof(TodoItem.IsDone),
            nameof(TodoItem.DueDate),
            nameof(TodoItem.CreatedAt),
            nameof(TodoItem.UpdatedAt)
        };

        var searchOptions = SearchTodoItemsQueryHandler.SearchOptions;

        Assert.Equal(expectedMaxLimit, searchOptions.MaxLimit);
        Assert.Equal(expectedOrderFields.Count, searchOptions.OrderBy.Count);

        foreach (var expectedField in expectedOrderFields)
        {
            Assert.Contains(expectedField, searchOptions.OrderBy);
        }
    }
}