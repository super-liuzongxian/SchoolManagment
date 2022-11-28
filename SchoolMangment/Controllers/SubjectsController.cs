using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using SchoolMangment.Dtos;
using SchoolMangment.Models;
using SchoolMangment.Repostories;

namespace SchoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }
        /// <summary>
        /// 查询老师所教所有科目
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet("{teacherId}")]
        public async Task<ActionResult<ResultDto<List<Subject>>>> Get(int teacherId)
        {
            ResultDto<List<Subject>> resultDto = new ResultDto<List<Subject>>();
            resultDto.Data=await subjectRepository.GetSubjectsByTeacherId(teacherId);
            resultDto.StatusCode= 200;
            return Ok(resultDto); 
        }
        /// <summary>
        /// 新增科目
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="teacherIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResultDto<int>>> Post(Subject subject)
        {
            ResultDto<int> resultDto= new ResultDto<int>(); 
            resultDto.Data=await subjectRepository.AddSubject(subject);
            resultDto.StatusCode= 200;
            return Ok(resultDto);
        }

    }
}
