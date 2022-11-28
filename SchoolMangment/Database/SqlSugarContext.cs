using SchoolMangment.Models;
using SqlSugar;

namespace SchoolMangment.Database
{
    public class SqlSugarContext
    {
        public readonly ISqlSugarClient db;
        public SqlSugarContext(ISqlSugarClient DBContext)
        {
            this.db = DBContext;
        }
        public void CreateTable()
        {
            db.DbMaintenance.CreateDatabase();//没有数据库则新建
            db.CodeFirst.SetStringDefaultLength(50).BackupTable().InitTables(new Type[]
            {
                typeof(Classes),         
               typeof(ClassMapTeacher),
               typeof(Grade),
                typeof(Student),
                typeof(Subject),
                typeof(Teacher),
                typeof(SubjectMapTeacher),
            });
        }
    }
}
