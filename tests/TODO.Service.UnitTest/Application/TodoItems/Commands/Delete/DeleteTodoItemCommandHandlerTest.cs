using Todo.Service.Application.TodoItems.Commands.Delete;
using Todo.Service.Persistence;

namespace Todo.Service.UnitTest.Application.TodoItems.Commands.Delete;

public class DeleteTodoItemCommandHandlerTest
{
    private ITodoContext _context;
    private Messages _messages;
    private DeleteTodoItemCommandHandler _sut;

    private Faker _faker;

    private DeleteTodoItemCommandBuilder _deleteTodoItemCommandBuilder;
    private TodoItemBuilder _todoItemBuilder;

    private Guid _id;
    private string _exceptionMessage;


    public DeleteTodoItemCommandHandlerTest()
    {
        Setup();
        InitializeBuilders();
        InitializeTestData();
        InitializeDependencies();
    }

    private void Setup()
    {
        _faker = new Faker();
        _exceptionMessage = "Persistence exception occurred";
    }

    private void InitializeBuilders()
    {
        _deleteTodoItemCommandBuilder = DeleteTodoItemCommandBuilder.New();
        _todoItemBuilder = TodoItemBuilder.New();
    }

    private void InitializeTestData()
    {
        _id = _faker.Random.Guid();
    }

    private void InitializeDependencies()
    {
        _context = Substitute.For<ITodoContext>();
        _messages = MessagesConfig.Build();
        _sut = new DeleteTodoItemCommandHandler(_context, _messages);
    }

    [Fact]
    public async Task Must_Throw_NotFoundException_When_Not_Found_Entity()
    {
        var request = _deleteTodoItemCommandBuilder.Build();
        var set = new List<TodoItem>().AsDbSet();

        _context.TodoItems.Returns(set);

        var ex = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(request, CancellationToken.None));
        _context.TodoItems.Received();

        Assert.Equal(_messages.GetMessage(Messages.Entities.TODO_ITEM), ex.Name);
        Assert.Equal(request.Id, ex.Key);
    }

    [Fact]
    public async Task Must_Throw_NotFoundException_When_Entity_Is_Inactive()
    {
        var request = _deleteTodoItemCommandBuilder.WithId(_id).Build();
        CreateTodoItemsSet(_id, false);

        var ex = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(request, CancellationToken.None));

        _context.TodoItems.Received();

        Assert.Equal(_messages.GetMessage(Messages.Entities.TODO_ITEM), ex.Name);
        Assert.Equal(request.Id, ex.Key);
    }

    [Fact]
    public async Task Must_Save()
    {
        var request = _deleteTodoItemCommandBuilder.WithId(_id).Build();
        CreateTodoItemsSet(_id);

        await _sut.Handle(request, CancellationToken.None);

        await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());

        Assert.False(_context.TodoItems.FirstOrDefault().Active);
    }

    [Fact]
    public async Task Must_Throw_PersistenceException_When_Ocurr_Error_In_SaveChangesAsync()
    {
        var request = _deleteTodoItemCommandBuilder.WithId(_id).Build();
        CreateTodoItemsSet(_id);

        _context.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(x => Task.FromException<int>(new Exception(_exceptionMessage)));

        var ex = await Assert.ThrowsAsync<PersistenceException>(() => _sut.Handle(request, CancellationToken.None));

        await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());

        Assert.Equal(_exceptionMessage, ex.InnerException.Message);
    }

    private void CreateTodoItemsSet(Guid id, bool active = true)
    {
        var todoItem = _todoItemBuilder.WithId(id).WithActive(active).Build();

        var todoItemSet = new List<TodoItem>
        {
            todoItem
        }.AsDbSet();

        _context.TodoItems.Returns(todoItemSet);
    }
}