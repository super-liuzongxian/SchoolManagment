using SqlSugar;

namespace SchoolMangment.Models
{
    [SugarTable("SubjectMapTeachers")]
    public class SubjectMapTeacher
    {
        [SugarColumn(IsPrimaryKey =true)]
        public int TeacherId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int SubjectId { get; set; }
    }
}
