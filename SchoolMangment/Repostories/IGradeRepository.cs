using SchoolMangment.Models;

namespace SchoolMangment.Repostories
{
    public interface IGradeRepository
    {
        public List<Grade> GetAllGrade();
        public int Addgrade(Grade grade);
        public int Updategrade(Grade grade);
    }
}
