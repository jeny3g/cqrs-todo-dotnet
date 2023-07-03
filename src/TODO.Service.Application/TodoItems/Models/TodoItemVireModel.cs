using Todo.Service.Application.Models.ViewModels;

namespace Todo.Service.Application.TodoItems.Models;

public class TodoItemViewModel : ETrackerViewModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsDone { get; set; }

    public DateTime? DueDate { get; set; }

    public bool Active { get; set; }
}
