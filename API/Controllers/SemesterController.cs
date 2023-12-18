using Application.Semester;
using Application.Semester.CsRequest;
using Application.Semester.CsResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("semesters")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SemesterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <param name="data">Date format: YYYY-MM-DD</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> addNewSemester(NewSemesterData data)
        {
            var command = new NewSemester.Command
            {
                EndDate = data.EndDate,
                Name = data.Name,
                StartDate = data.StartDate
            };
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<SemesterInList>>> getSemester()
        {
            return await _mediator.Send(new GetSemesters.Query());
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <param name="semesterName">Name is return when get semester list</param>
        /// <returns></returns>
        [HttpGet]
        [Route("student/{semesterName}")]
        public async Task<List<StudentInSemester>> getStudentInternshipInSemester(string semesterName)
        {
            var query = new StudentIntershipInSemester.Query { SemesterName = semesterName };
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <param name="semesterName">Name is return when get semester list</param>
        /// <returns></returns>
        [HttpGet]
        [Route("recruitment_informations/{semesterName}")]
        public async Task<List<RecruitmentInSemester>> GetRecruitmentInSemesters(string semesterName)
        {
            var query = new GetRecruitmentInSemester.Query { SemesterName = semesterName };
            return await _mediator.Send(query);
        }
    }
}
