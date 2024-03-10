using AutoMapper;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateStudentDto, Student>();
        CreateMap<CreateStudentDto, Student>();

        CreateMap<CreateClassRoomDto,ClassRoom>();
        CreateMap<UpdateClassRoomDto, ClassRoom>();

    }
}
