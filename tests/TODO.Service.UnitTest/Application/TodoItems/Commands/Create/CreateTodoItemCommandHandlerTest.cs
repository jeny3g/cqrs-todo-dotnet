using NSubstitute.ExceptionExtensions;
using Todo.Service.Application.TodoItems.Commands.Create;
using Todo.Service.Persistence;

namespace Todo.Service.UnitTest.Application.TodoItems.Commands.Create;

public class CreateTodoItemCommandHandlerTest
{
    private ITodoContext _context;
    private CreateTodoItemCommandHandler _sut;

    private Faker _faker;

    private CreateTodoItemCommandBuilder _createTodoItemCommandBuilder;

    private string _exceptionMessage;


    public CreateTodoItemCommandHandlerTest()
    {
        Setup();
        InitializeBuilders();
        InitializeTestData();
        InitializeDependencies();
    }

    private void Setup()
    {
        _faker = new Faker();
    }

    private void InitializeBuilders()
    {
        _createTodoItemCommandBuilder = CreateTodoItemCommandBuilder.New();
    }

    private void InitializeTestData()
    {
        _exceptionMessage = "Persistence exception occurred";
    }

    private void InitializeDependencies()
    {
        _context = Substitute.For<ITodoContext>();
        _sut = new CreateTodoItemCommandHandler(_context);
    }

    [Fact]
    public async Task Must_Save()
    {
        var command = _createTodoItemCommandBuilder.Build();

        await _sut.Handle(command, CancellationToken.None);

        _context.TodoItems.Received().Add(Arg.Any<TodoItem>());
        await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Must_Throw_PersistenceException_When_Ocurr_Error_In_SaveChangesAsync()
    {
        var command = _createTodoItemCommandBuilder.Build();
        _context.SaveChangesAsync(default).ThrowsForAnyArgs(new Exception(_exceptionMessage));

        var ex = await Assert.ThrowsAsync<PersistenceException>(() => _sut.Handle(command, CancellationToken.None));

        _context.TodoItems.Received().Add(Arg.Any<TodoItem>());
        await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());

        Assert.Equal(_exceptionMessage, ex.Message);
    }
}
