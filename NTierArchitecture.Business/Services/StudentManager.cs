using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using NTierArchitecture.Business.Constants;
using NTierArchitecture.Business.Validator;
using NTierArchitecture.DataAccess.Repositories;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System.Linq.Expressions;

namespace NTierArchitecture.Business.Services;

public sealed class StudentManager(IStudentRepository studentRepository,
    IMapper mapper) : IStudentService
{
    public bool Any(Expression<Func<Student, bool>> predicate)
    {
        var result = studentRepository.Any(predicate);
        return result;
    }

    public string Create(CreateStudentDto request)
    {
        CreatStudentDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if(!result.IsValid)
        {
            throw new Exception(string.Join(", ", result.Errors));
        }

        bool isIdentityNumberExists =  studentRepository.Any(p=>p.IdentityNumber == request.IdentityNumber);

        if(isIdentityNumberExists)
        {
            return MessageConstants.NameAlreadyExists;
        }

        int studentNumber = studentRepository.GetNewStudentNumber();

        Student student = mapper.Map<Student>(request);
        student.StudentNumber = studentNumber;
        student.CreatedBy = "Admin";
        student.CreatedDate = DateTime.UtcNow;


        studentRepository.Create(student);

        return MessageConstants.CreateIsSuccessfully;
        //Mapper sayesinde kurtulduğumuz kodlar
        //new()
        //{
        //    FirstName = request.FirstName,
        //    LastName = request.LastName,
        //    StudentNumber = studentNumber,
        //    IdentityNumber = request.IdentityNumber,
        //    ClassRoomId = request.ClassRoomId,
        //    CreatedBy = "Admin",
        //    CreatedDate = DateTime.Now
        //};
    }

    public string DeleteById(Guid id)
    {
        studentRepository.DeleteById(id);
        return MessageConstants.DeleteIsSuccessfully;
    }

    public List<Student> GetAll()
    {
        List<Student> students = 
            studentRepository.GetAll()
            .Where(p=>!p.IsDeleted)
            .OrderBy(p=>p.ClassRoomId)
            .ThenBy(p=>p.FirstName + " "+ p.LastName)
            .ToList();

        return students;
    }

    public List<Student> GetAllByClassRoomId(Guid classRoomId)
    {
        List<Student> students =
           studentRepository.GetAll()
           .Where(p => p.ClassRoomId == classRoomId)
           .OrderBy(p => p.FirstName)
           .ToList();

        return students;
    }

    public string Update(UpdateStudentDto request)
    {
        UpdateStudentDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if(!result.IsValid)
        {
            throw new ValidationException(string.Join(", ", result.Errors.Select(s=>s.ErrorMessage).ToList()));  
        }

        Student? student = studentRepository.GetStudentById(request.Id);
        if(student == null)
        {
            return MessageConstants.DataNotFound;
        }

        if(student.IdentityNumber != request.IdentityNumber)
        {
            bool isIdentityNumberExists = studentRepository.Any(p => p.IdentityNumber == request.IdentityNumber );
            if(isIdentityNumberExists)
            {
                return MessageConstants.NameAlreadyExists;
            }
        }

        mapper.Map(request, student);
        student.UpdatedDate = DateTime.Now;
        student.UpdatedBy = "Admin";

        //student.IdentityNumber = request.IdentityNumber;
        //student.FirstName = request.FirstName;
        //student.LastName = request.LastName;
        //student.ClassRoomId = request.ClassRoomId;  
        //studentRepository.Update(student);

        return MessageConstants.UpdateIsSuccessfully;
    }
}
