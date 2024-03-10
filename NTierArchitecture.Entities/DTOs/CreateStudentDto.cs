using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Entities.DTOs;
public sealed record CreateStudentDto(
    string FirstName,
    string LastName,
    string IdentityNumber,
    int StudentNumber,
    Guid ClassRoomId
   );
