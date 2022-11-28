using SchoolMangment.Database;
using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SqlSugarContext sqlSugarContext;
        public Task<int> AddTeacher(Teacher teacher)
        {
           return sqlSugarContext.db.Insertable(teacher).ExecuteReturnIdentityAsync();
        }
        public Task<Teacher> GetTeacherById(int teacherId)
        {
            return sqlSugarContext.db.Queryable<Teacher>()
                .Includes(x => x.Subjects)
                .Includes(x => x.Classes)
                .SingleAsync(x => x.Id == teacherId);
        }
        public Task<List<Teacher>> GetTeachers(int pageIndex, int pageCount, ref int totalCount)
        {
            return sqlSugarContext.db.Queryable<Teacher>()
                 .ToPageListAsync(pageIndex, pageCount, totalCount);
        }
        public Task<List<Teacher>> GetTeachersBySubject(int subjectId)
        {
            return sqlSugarContext.db.Queryable<Teacher>().Includes(x => x.Subjects.Single(sub => sub.Id == subjectId)).ToListAsync();
        }
        public async Task<int> UpdateTeacher(Teacher teacher)
        {
            Teacher oldTeacher =await sqlSugarContext.db.Queryable<Teacher>().SingleAsync(x => x.Id == teacher.Id);
            oldTeacher.BirthDay = teacher.BirthDay;
            oldTeacher.Address =teacher.Address;
            oldTeacher.Name = teacher.Name;
            oldTeacher.Gender = teacher.Gender;
            return await sqlSugarContext.db.Updateable(oldTeacher).ExecuteCommandAsync();
            
        }
    }
}
