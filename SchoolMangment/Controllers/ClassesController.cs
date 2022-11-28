using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMangment.Dtos;
using SchoolMangment.Models;
using SchoolMangment.Repostories;

namespace SchoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassesRepository classesRepository;

        public ClassesController(IClassesRepository classesRepository)
        {
            this.classesRepository = classesRepository;
        }

        /// <summary>
        /// 根据班级ID获取班级信息以及班级下的所有学生
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResultDto<Classes>> Get(int cId)
        {
            ResultDto<Classes> resultDto = new ResultDto<Classes>();
            resultDto.StatusCode= 200;
            resultDto.Data = classesRepository.GetClassesIncludeStudent(cId);
            return Ok(resultDto);
        }
    }
}
