using SqlSugar;

namespace SchoolMangment.Models
{
    [SugarTable("Teachers",TableDescription="老师表")]
    public class Teacher
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(20)",ColumnDescription ="老师名称")]
        public  string Name { get; set; }
        [Navigate(typeof(ClassMapTeacher),nameof(ClassMapTeacher.ClassesId),nameof(ClassMapTeacher.TeacherId))]
        [SugarColumn(ColumnDescription ="教师所教班级")]
        public List<Classes> Classes { get; set; }
        [Navigate(typeof(SubjectMapTeacher),nameof(SubjectMapTeacher.TeacherId),nameof(SubjectMapTeacher.SubjectId))]
        public List<Subject> Subjects { get; set; }
        [SugarColumn(ColumnDescription ="教师生日",ColumnDataType ="varchar(20)",IsNullable =true)]
        public string BirthDay { get; set; }
        [SugarColumn(ColumnDescription ="教师所住地址",IsNullable =true)]
        public string Address { get; set; }
        [SugarColumn(ColumnDescription ="教师性别（0 女 1 男）",IsNullable =false)]
        public int Gender { get; set; }
    }
}
