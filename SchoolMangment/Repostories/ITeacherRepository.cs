using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public interface ITeacherRepository
    {
        Task<int> AddTeacher(Teacher teacher);
        Task<int> UpdateTeacher(Teacher teacher);
        /// <summary>
        /// 获取所有教师并分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        Task<List<Teacher>> GetTeachers(int pageIndex,int pageCount,ref int totalCount);
        /// <summary>
        /// 根据学科ID获取所教该学科的老师
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Task<List<Teacher>> GetTeachersBySubject(int subjectId);
        /// <summary>
        /// 根据老师ID获取该老师的详细信息
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        Task<Teacher> GetTeacherById(int teacherId);
    }
}
