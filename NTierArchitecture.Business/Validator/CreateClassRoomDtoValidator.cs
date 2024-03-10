using FluentValidation;
using FluentValidation.Results;
using NTierArchitecture.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Validator;
public class CreateClassRoomDtoValidator : AbstractValidator<CreateClassRoomDto>
{
    public CreateClassRoomDtoValidator()
    {
        RuleFor(p=>p.Name).NotEmpty().MinimumLength(3);
    }
}
