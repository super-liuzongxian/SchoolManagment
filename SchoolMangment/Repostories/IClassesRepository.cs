using SchoolMangment.Models;
using System.Threading.Tasks;

namespace SchoolMangment.Repostories
{
    public interface IClassesRepository
    {
        public int InserClasses(Classes classes);
        public Classes GetClassesIncludeStudent(int id);

        public Task<int> UpdateClasses(Classes classes);
    }
}
