using Todo.Service.Application.Settings.FluentValidations;

namespace Todo.Service.Application.TodoItems.Commands.Create;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator(Messages messages, IBaseValidationService validationService)
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .WithMessage(Messages.Validations.NOT_EMPTY)
            .MaximumLength(100)
            .WithMessage(Messages.Validations.MAX_LENGTH)
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
        

        RuleFor(command => command.Description)
            .MaximumLength(500)
            .WithMessage(Messages.Validations.MAX_LENGTH)
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);


        RuleFor(command => command.DueDate)
            .Must(BeAValidDueDate)
            .WithMessage(Messages.Validations.INVALID_DATE);
    }

    private bool BeAValidDueDate(DateTime? dueDate)
    {
        if (!dueDate.HasValue)
            return true; // Optional due date, so it's valid if not specified

        return dueDate.Value >= DateTime.Today; // Due date must be today or in the future
    }
}
