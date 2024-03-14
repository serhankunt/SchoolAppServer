using EntityFrameworkCorePagination.Nuget.Pagination;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services;
public interface IStudentService
{
    string Create(CreateStudentDto request);
    string Update(UpdateStudentDto request);
    string DeleteById(Guid id);
    List<Student> GetAll();
    bool Any(Expression<Func<Student, bool>> predicate);
    Task<PaginationResult<Student>> GetAllByClassRoomIdAsync(PaginationRequestDto request);
}
