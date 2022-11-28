using Microsoft.AspNetCore.Mvc;
using SchoolMangment.Dtos;
using SchoolMangment.Models;
using SchoolMangment.Repostories;

namespace SchoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController:ControllerBase
    {
        private readonly ITeacherRepository teacherRepository;

        public TeachersController(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ResultDto<List<Teacher>>>> Get(int pageIndex,int pageCount)
        {
            int totalCount = 0;
            ResultDto<List<Teacher>> resultDto = new ResultDto<List<Teacher>>();
            resultDto.Data =await teacherRepository.GetTeachers(pageIndex, pageCount, ref totalCount);
            resultDto.TotalCount = pageCount;
            resultDto.StatusCode= 200;
            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<ActionResult<ResultDto<int>>> Post(Teacher teacher)
        {
            ResultDto<int> resultDto= new ResultDto<int>();
            resultDto.Data=await teacherRepository.AddTeacher(teacher);
            resultDto.StatusCode= 200;
            return Ok(resultDto);
        }
        [HttpGet("{subjectId}")]
        public async Task<ActionResult<ResultDto<List<Teacher>>>> Get(int subjectId)
        {
            ResultDto<List<Teacher>> resultDto = new ResultDto<List<Teacher>>();
            resultDto.Data = await teacherRepository.GetTeachersBySubject(subjectId);
            resultDto.StatusCode = 200;
            return Ok(resultDto);
        }
    }
}
