using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using SchoolMangment.Dtos;
using SchoolMangment.Models;
using SchoolMangment.Repostories;
using System.Collections.Generic;

namespace SchoolMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        /// <summary>
        /// 获取所有学生信息，并分页
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageCount">一页展示多少条数据</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResultDto<List<Student>>> Get(int pageIndex, int pageCount)
        {
            ResultDto<List<Student>> result = new ResultDto<List<Student>>();
            int totalCount = 0;
            result.Data = studentRepository.GetPagingStudent(pageIndex, pageCount, ref totalCount);
            result.TotalCount = totalCount;
            result.StatusCode = 200;
            return Ok(result);
        }
        /// <summary>
        /// 根据学生ID修改学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ResultDto<int>> Post(Student student)
        {
            ResultDto<int> result = new ResultDto<int>();
            result.Data = studentRepository.AddStudent(student);
            result.StatusCode = 200;
            return Ok(result);
        }
        /// <summary>
        /// 根据班级Id查找对应班级下的学生信息并分页
        /// </summary>
        /// <param name="classesId">班级Id</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageCount">一页展示多少条数据</param>
        /// <returns></returns>
        [HttpGet("{classesId}")]
        public async Task<ActionResult<ResultDto<List<Student>>>> Get(int classesId, int pageIndex,int pageCount)
        {
            ResultDto<List<Student>> resultDto= new ResultDto<List<Student>>();
            int totalCount = 0;
            resultDto.Data =await studentRepository.GetStudentsByClassesId(classesId, pageIndex, pageCount,ref totalCount);
            resultDto.TotalCount = totalCount;
            resultDto.StatusCode = 200;
            return Ok(resultDto);
         }
    }
}
