using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMangment.Dtos;
using SchoolMangment.MediatR;
using SchoolMangment.Models;
using SchoolMangment.Repostories;
using SchoolMangment.Utils;

namespace SchoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMediator mediator;
        public GradesController(IGradeRepository gradeRepository, IMediator mediator)
        {
            _gradeRepository = gradeRepository;
            this.mediator = mediator;
        }

        /// <summary>
        /// 获取所有年级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RequestLimit]
        public ActionResult<List<Grade>> Get()
        {
            //var result = mediator.Send(new Email() { Address = "lc1517077181@outlook.com" });
            //var result1 = mediator.Send(new PrintCommand() { CommandName = "Helloworld" });
            return Ok(_gradeRepository.GetAllGrade());
        }
        /// <summary>
        /// 新增年级
        /// </summary>
        /// <param name="dto">年级信息</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<int> Post(GradeDto dto)
        {
            Grade model = dto.MapTo<GradeDto, Grade>(); //对象映射
            return Ok(_gradeRepository.Addgrade(model));
        }

        /// <summary>
        /// 根据年级id修改年级
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<int> Put(GradeDto dto)
        {
            Grade grade=dto.MapTo<GradeDto, Grade>();
            return Ok(_gradeRepository.Updategrade(grade));
        }
    }
}
