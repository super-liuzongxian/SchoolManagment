using SchoolMangment.Database;
using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SqlSugarContext sqlSugarContext;

        public SubjectRepository(SqlSugarContext sqlSugarContext)
        {
            this.sqlSugarContext = sqlSugarContext;
        }

        public Task<int> AddSubject(Subject subject)
        {
            return sqlSugarContext.db.Insertable(subject).ExecuteReturnIdentityAsync() ;
        }

        public Task<List<Subject>> GetSubjectsByTeacherId(int teacherId)
        {
            return sqlSugarContext.db.Queryable<Subject>().Includes(x => x.Teachers.Where(x => x.Id == teacherId)).ToListAsync();
        }

        public async Task<int> UpdateSubject(Subject subject)
        {
            Subject oldsubject =await sqlSugarContext.db.Queryable<Subject>().SingleAsync(x => x.Id == subject.Id);
            oldsubject.SubjectName=subject.SubjectName;
            return await sqlSugarContext.db.Updateable(oldsubject).ExecuteCommandAsync();
        }

    }
}
