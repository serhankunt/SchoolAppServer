﻿using FluentValidation;
using NTierArchitecture.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Validator;
public sealed class CreatStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreatStudentDtoValidator()
    {
        RuleFor(p=>p.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(p=>p.LastName).NotEmpty().MinimumLength(3);
        RuleFor(p=>p.IdentityNumber).NotEmpty().MinimumLength(11).MaximumLength(11).Matches("[0-9]");
    }
}
