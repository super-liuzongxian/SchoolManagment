using SchoolMangment.Database;
using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly SqlSugarContext sqlSugarContext;

        public ClassesRepository(SqlSugarContext sqlSugarContext)
        {
            this.sqlSugarContext = sqlSugarContext;
        }

        public Classes GetClassesIncludeStudent(int id)
        {
            return sqlSugarContext.db.Queryable<Classes>().Includes(x => x.Students).First();
        }

        public int InserClasses(Classes classes)
        {
            return sqlSugarContext.db.Insertable(classes).ExecuteReturnIdentity();
        }

        public async  Task<int> UpdateClasses(Classes classes)
        {
            Classes oldClasses =await sqlSugarContext.db.Queryable<Classes>().SingleAsync(x=>x.Id==classes.Id);
            oldClasses.ClassName= classes.ClassName;
            oldClasses.ClassType = classes.ClassType;
            oldClasses.ClassesId = classes.ClassesId;
            return await sqlSugarContext.db.Updateable(oldClasses).ExecuteCommandAsync();
        }
    }
}
