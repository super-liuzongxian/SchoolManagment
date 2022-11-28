using SqlSugar;

namespace SchoolMangment.Models
{
    [SugarTable("Grades",TableDescription ="年级表")]
    public class Grade
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
        [SugarColumn(ColumnDescription = "年级名", ColumnDataType = "nvarchar(30)")]
        public string Name { get; set; }
        [Navigate(NavigateType.OneToMany,nameof(Models.Classes.GradeId))]
        public List<Classes> Classes { get; set; }
    }
}
