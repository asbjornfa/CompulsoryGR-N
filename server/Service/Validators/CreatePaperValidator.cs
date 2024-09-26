using DataAccess.Models;
using FluentValidation;
using Service.DTO.Request;

namespace Service.Validators;

public class CreatePaperValidator : AbstractValidator<RequestCreatePaperDTO>
{
    public CreatePaperValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Paper name cannot be empty");
        
    }
    
}