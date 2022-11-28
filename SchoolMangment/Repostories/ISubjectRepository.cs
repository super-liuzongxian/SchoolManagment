using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public interface ISubjectRepository
    {
        public Task<int> AddSubject(Subject subject);
        public Task<int> UpdateSubject(Subject subject);
        public Task<List<Subject>> GetSubjectsByTeacherId(int teacherId);
    }
}
