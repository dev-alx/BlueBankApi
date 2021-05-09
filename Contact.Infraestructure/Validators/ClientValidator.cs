using Contact.Core.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Validators
{
    public class ClientValidator : AbstractValidator<ClientDto>
    {
        public ClientValidator()
        {
            RuleFor(c => c.FullName)
                .NotNull()
                .NotEmpty()
                .Length(4, 100);

            RuleFor(c => c.SocialNumber)
                .NotNull()
                .NotEmpty()
                .Length(5, 15);

            RuleFor(c => c.InitialBalance)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
