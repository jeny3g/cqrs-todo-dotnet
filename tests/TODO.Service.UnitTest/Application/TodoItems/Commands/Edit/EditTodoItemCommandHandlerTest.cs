using Todo.Service.Application.TodoItems.Commands.Edit;
using Todo.Service.Persistence;

namespace Todo.Service.UnitTest.Application.TodoItems.Commands.Edit;

public class EditTodoItemCommandHandlerTest
{
    private ITodoContext _context;
    private EditTodoItemCommandHandler _sut;
    private Faker _faker;

    private EditTodoItemCommandBuilder _editTodoItemCommandBuilder;
    private TodoItemBuilder _todoItemBuilder;

    private Guid _id;
    private string _exceptionMessage;

    public EditTodoItemCommandHandlerTest()
    {
        Setup();
        InitializeBuilders();
        InitializeTestData();
        InitializeDependencies();
        SetupDB();
    }

    private void Setup()
    {
        _faker = new Faker();
    }

    private void InitializeBuilders()
    {
        _editTodoItemCommandBuilder = EditTodoItemCommandBuilder.New();
        _todoItemBuilder = TodoItemBuilder.New();
    }

    private void InitializeTestData()
    {
        _id = _faker.Random.Guid();
        _exceptionMessage = "Persistence exception occurred";
    }

    private void InitializeDependencies()
    {
        _context = Substitute.For<ITodoContext>();
        _sut = new EditTodoItemCommandHandler(_context);
    }

    private void SetupDB()
    {
        var set = new List<TodoItem>()
        {
            _todoItemBuilder.WithId(_id).Build()
        }.AsDbSet();
        _context.TodoItems.Returns(set);
    }

    [Fact]
    public async Task Must_Save()
    {
        var request = _editTodoItemCommandBuilder.WithId(_id).Build();

        await _sut.Handle(request, CancellationToken.None);

        await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Must_Do_Nothing_When_Not_Found_Entity()
    {
        var request = _editTodoItemCommandBuilder.WithId(_id).Build();

        var set = new List<TodoItem>().AsDbSet();
        _context.TodoItems.Returns(set);

        await _sut.Handle(request, CancellationToken.None);

        await _context.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Must_Throw_PersistenceException_When_Ocurr_Error_In_SaveChangesAsync()
    {
        var request = _editTodoItemCommandBuilder.WithId(_id).Build();

        _context.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(x => Task.FromException<int>(new Exception(_exceptionMessage)));

        var ex = await Assert.ThrowsAsync<PersistenceException>(() => _sut.Handle(request, CancellationToken.None));

        await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());

        Assert.Equal(_exceptionMessage, ex.InnerException.Message);
    }
}
