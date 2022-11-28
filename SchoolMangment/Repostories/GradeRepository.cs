using SchoolMangment.Database;
using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly SqlSugarContext sqlSugarContext;

        public GradeRepository(SqlSugarContext sqlSugarContext)
        {
            this.sqlSugarContext = sqlSugarContext;
        }
        /// <summary>
        /// 插入数据返回插入数据的自增Id
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int Addgrade(Grade grade)
        {
            return sqlSugarContext.db.Insertable(grade).ExecuteReturnIdentity();
        }
        /// <summary>
        /// 获取所有的年级
        /// </summary>
        /// <returns></returns>
        public List<Grade> GetAllGrade()
        {
            return sqlSugarContext.db.Queryable<Grade>().ToList();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int Updategrade(Grade grade)
        {
            var oldGrade = sqlSugarContext.db.Queryable<Grade>().Single(x => x.Id == grade.Id);
            sqlSugarContext.db.Tracking<Grade>(oldGrade);//创建跟踪
            oldGrade.Name= grade.Name;
           var items= sqlSugarContext.db.TempItems.ToList();
            return  sqlSugarContext.db.Updateable(oldGrade).ExecuteCommand();
        }
    }
}
