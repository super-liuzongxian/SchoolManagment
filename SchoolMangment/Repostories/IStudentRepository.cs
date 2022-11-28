using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public interface IStudentRepository
    {
        List<Student> GetPagingStudent(int pageIndex, int pageCount,ref int totalCount);
        int AddStudent(Student student);
        Task<int> UpdateStudent(Student student);
        Task<List<Student>> GetStudentsByClassesId(int classesId,int pageIndex,int pageCount,ref int totalCount);
    }
}
