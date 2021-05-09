using Contact.Core.Dto;
using Contact.Core.Enumerations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Validators
{
    public class AccountMovementValidator : AbstractValidator<AccountMovementDto>
    {
        public AccountMovementValidator()
        {
            RuleFor(a => a.Account)
                .NotNull()
                .NotEmpty();


            RuleFor(a => a.Amount)
                .NotNull()
                .GreaterThan(1);

            RuleFor(a => a.MovementType)
                .NotNull()                
                .IsInEnum();
                
        }
    }
}
