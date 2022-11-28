using SqlSugar;

namespace SchoolMangment.Models
{
    [SugarTable("Classes",TableDescription ="班级表")]
    public class Classes
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
        [SugarColumn(IsNullable =false,ColumnDataType ="nvarchar(30)",ColumnDescription ="班级名")]
        public int ClassName { get; set; }
        [SugarColumn(IsNullable =false,ColumnDescription = "班级号")]
        public long ClassesId { get; set; }
        [SugarColumn(IsNullable =false,ColumnDescription ="班级类别，0 文科、1 理科")]
        public int ClassType { get; set; }
        [Navigate(typeof(ClassMapTeacher),nameof(ClassMapTeacher.TeacherId),nameof(ClassMapTeacher.TeacherId))]
        [SugarColumn(ColumnDescription ="这个班级由哪些教师教")]
        public List<Teacher> Teachers { get; set; }

        public int GradeId { get; set; }
        [Navigate(NavigateType.OneToMany,nameof(Student.ClassesId))]
        public List<Student> Students { get; set; }
    }
}
