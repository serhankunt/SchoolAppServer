using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services;
public interface IClassRoomService
{
    string Create(CreateClassRoomDto request);
    string Update(UpdateClassRoomDto request);
    string DeleteById(Guid id);
    List<ClassRoom> GetAll();
    bool Any(Expression<Func<ClassRoom, bool>> predicate);
}
