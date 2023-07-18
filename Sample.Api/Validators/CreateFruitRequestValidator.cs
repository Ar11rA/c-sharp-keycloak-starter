using FluentValidation;
using Sample.Api.DTO;

namespace Sample.Api.Validators;

public class CreateFruitRequestValidator : AbstractValidator<FruitRequest>
{
    private static readonly List<string> ValidClasses = new()
    {
        "berries", "pits", "core", "citrus", "melons", "tropical"
    };

    public CreateFruitRequestValidator()
    {
        RuleFor(x => x.Class)
            .Must(cls => ValidClasses.Contains(cls))
            .WithMessage("Class should be one of the values: " + string.Join(",", ValidClasses));
        RuleFor(x => x.Description)
            .NotNull()
            .MaximumLength(50)
            .WithMessage("Description should be less than 50 chars");
    }
}
