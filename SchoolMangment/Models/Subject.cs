using SqlSugar;

namespace SchoolMangment.Models
{
    [SugarTable("Subjects",TableDescription ="学科表")]
    public class Subject
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
        [SugarColumn(ColumnDescription ="学科名",ColumnDataType ="nvarchar(30)",IsNullable =false)]
        public string SubjectName { get; set; }
        [Navigate(typeof(SubjectMapTeacher),nameof(SubjectMapTeacher.SubjectId),nameof(SubjectMapTeacher.TeacherId))]
        public List<Teacher> Teachers { get; set; }
    }
}
