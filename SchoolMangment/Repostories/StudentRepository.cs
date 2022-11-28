using SchoolMangment.Database;
using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SqlSugarContext sqlSugarContext;
        public StudentRepository(SqlSugarContext sqlSugarContext)
        {
            this.sqlSugarContext = sqlSugarContext;
        }
        public int AddStudent(Student student)
        {
            return sqlSugarContext.db.Insertable(student).ExecuteReturnIdentity();
        }
        public List<Student> GetPagingStudent(int pageIndex, int pageCount, ref int totalCount)
        {
            return  sqlSugarContext.db.Queryable<Student>().ToPageList(pageIndex, pageCount, ref totalCount);
        }
        public Task<List<Student>> GetStudentsByClassesId(int classesId,int pageIndex,int pageCount,ref int totalCount)
        {
            return sqlSugarContext.db.Queryable<Student>().Where(x => x.ClassesId == classesId).ToPageListAsync(pageIndex,pageCount, totalCount);
        }
        public async Task<int> UpdateStudent(Student student)
        {
            Student oldStudent =await sqlSugarContext.db.Queryable<Student>().SingleAsync(x => x.Id == student.Id);
            oldStudent.Name= student.Name;
            oldStudent.IdNumber= student.IdNumber;
            oldStudent.Address= student.Address;
            oldStudent.City= student.City;
            oldStudent.Age= student.Age;
            return await sqlSugarContext.db.Updateable(oldStudent).ExecuteCommandAsync();
        }

       
    }
}
