using SqlSugar;

namespace SchoolMangment.Models
{
    [SugarTable("ClassMapTeachers")]
    public class ClassMapTeacher
    {
        [SugarColumn(IsPrimaryKey =true)]
        public int TeacherId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int ClassesId { get; set; }
    }
}
