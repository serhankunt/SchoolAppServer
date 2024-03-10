﻿using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DataAccess.Repositories;
public interface IStudentRepository
{
    void Create(Student student);
    void Update(Student student);
    void DeleteById(Guid Id);
    IQueryable<Student> GetAll();
    Student? GetStudentById(Guid studentId);
    bool Any(Expression<Func<Student,bool>> predicate);
    int GetNewStudentNumber();
}
