using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.Models;
using System.Linq.Expressions;

namespace NTierArchitecture.DataAccess.Repositories;

public sealed class ClassRoomRepository(ApplicationDbContext context) : IClassRoomRepository
{
    public void Create(ClassRoom classRoom)
    {
        context.Add(classRoom);
        context.SaveChanges();
    }

    public void DeleteById(Guid Id)
    {
        ClassRoom? classRoom = GetClassRoomById(Id);
        if (classRoom is not null)
        {
            context.Remove(classRoom);
            context.SaveChanges();
        }
    }

    public List<ClassRoom> GetAll()
    {
        return context.ClassRooms.ToList();
    }

    public ClassRoom? GetClassRoomById(Guid classRoomId)
    {
        return context.ClassRooms.Where(p=>p.Id == classRoomId && !p.IsDeleted).FirstOrDefault();
    }

    public void Update(ClassRoom classRoom)
    {
        context.Update(classRoom);
        context.SaveChanges();
    }

    public bool Any(Expression<Func<ClassRoom, bool>> predicate)
    {
        return context.ClassRooms.Any(predicate);
    }

}