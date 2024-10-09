using FluentValidation;
using Service.DTO.Request;

namespace Service.Validators;

public class CreateOrderEntryValidator : AbstractValidator<RequestCreateOrderEntryDTO>
{
    public CreateOrderEntryValidator()
    {
        RuleFor(o => o.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero");
        RuleFor(o => o.ProductId).NotEmpty().WithMessage("ProductId cannot be empty");
        // RuleFor(o => o.OrderId).NotEmpty().WithMessage("OrderId cannot be empty");
    }
}
    