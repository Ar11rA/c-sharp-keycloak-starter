using Sample.Api.DTO;
using Sample.Api.Validators;
using FluentAssertions;

namespace Sample.Tests.Unit.Validators;

public class CreateFruitRequestValidatorTest
{
    private readonly CreateFruitRequestValidator _requestValidator;

    public CreateFruitRequestValidatorTest()
    {
        _requestValidator = new CreateFruitRequestValidator();
    }

    [Fact]
    private void Validate_CreateFruitRequest_Valid()
    {
        var request = new FruitRequest()
        {
            Name = "Test",
            Description = "Test Description",
            Class = "berries"
        };

        var result = _requestValidator.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    private void Validate_CreateFruitRequest_Invalid()
    {
        var request = new FruitRequest()
        {
            Name = "Test",
            Description =
                "Test Description to find out the validation error for create fruit request validator method.",
            Class = "Test"
        };

        var result = _requestValidator.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorMessage.Contains("Class should be one of the values: ").Should().BeTrue();
        result.Errors[1].ErrorMessage.Contains("Description should be less than 50 chars").Should().BeTrue();
    }
}