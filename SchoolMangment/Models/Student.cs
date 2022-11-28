using SqlSugar;

namespace SchoolMangment.Models
{
    [SugarIndex("idx_name",nameof(Student.Name),OrderByType.Asc)] //普通索引
    [SugarIndex("idx_city_address",nameof(Student.City),OrderByType.Asc,nameof(Student.Address),OrderByType.Asc)]//组合索引
    [SugarIndex("unique_IdNumber",nameof(IdNumber),OrderByType.Asc,true)]// 唯一索引 true代表唯一索引
    [SugarTable("Students", TableDescription = "学生表")]
    public class Student
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [SugarColumn(IsNullable = true, ColumnDataType = "nvarchar(20)", ColumnDescription = "学生姓名")]
        public string Name { get; set; }
        [SugarColumn(IsNullable =false,ColumnDescription ="学生年龄")]
        public int Age { get; set; }
        [SugarColumn(ColumnDescription ="学生居住地址",ColumnDataType ="nvarchar(100)")]
        public string Address { get; set; }
        [SugarColumn(ColumnDescription ="学生居住城市",IsNullable =true,ColumnDataType ="nvarchar(20)")]
        public string City { get; set; }
        [SugarColumn(ColumnDescription ="学生身份证号",IsNullable =false,ColumnDataType ="varchar(20)")]
        public string IdNumber { get; set; }

        public int ClassesId { get; set; }
    }
}
