using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Students;
using Application.Students.CustomizeResponseObject;
using Application.Students.CustomizeRequestObject;

namespace API.Controllers
{
    [Route("students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Role: Student
        /// </summary>
        /// <param name="Code">Code is return when student login success</param>
        /// <returns></returns>
        [HttpGet]
        [Route("details/{Code}")]
        public async Task<ActionResult<StudentDetailReturn>> StudentInfo(string Code)
        {
            return await _mediator.Send(new StudentInfo.Query
            {
                StudentCode = Code
            });
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="CpCode">Code of company staff is returned in login API</param>
        /// <returns></returns>
        [HttpGet]
        [Route("company/{CpCode}")]
        public async Task<ActionResult<List<StudentInList>>> StudentInCompany(string CpCode)
        {
            return await _mediator.Send(new StudentInCompany.Query { CompanyCode = CpCode });
        }

        /// <summary>
        /// Role: FPTStaff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("import")]
        public async Task<ActionResult<Unit>> AddNewStudent(AddNewRequest request)
        {
            var command = new NewStudent.Command { Students = request.StudentList };
            return await _mediator.Send(command);
        } 

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <param name="studentStatus">0: NotInWork, 1: Working, 2: Finished</param>
        /// <returns></returns>
        [HttpGet]
        [Route("status/{studentStatus}")]
        public async Task<ActionResult<List<ExportStudent>>> GetStudentByStatus(int studentStatus)
        {
            return await _mediator.Send(new ListStudent.Query { status = studentStatus });
        }
    }
}
