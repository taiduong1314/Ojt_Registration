using Application.OjtReport;
using Application.OjtReport.CustomizeResponseObject;
using Application.OjtReport.RequestObj;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("fptstaff")]
        public async Task<ActionResult<List<ReportDetailInList>>> ReportDetail()
        {
            return await _mediator.Send(new ViewOjtReport.Query());
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="request">Mark: Greater than 0, Date: Greater than 30, Division: FE,BE,...</param>
        /// <returns></returns>
        [HttpPost]
        [Route("evaluate")]
        public async Task<ActionResult<Unit>> EvaluateStudent(Evaluate request)
        {
            var command = new EvaluateStudent.Command
            {
                CompanyCode = request.CompanyCode,
                Division = request.Division,
                LineManagerName = request.LineManagerName,
                Mark = request.Mark,
                OnWorkDate = request.OnWorkDate,
                StudentCode = request.StudentCode,
                WorkSortDescription = request.WorkSortDescription
            };
            return await _mediator.Send(command);
        }
        
        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="studentcode">Code of student returned in list report</param>
        /// <returns></returns>
        [HttpGet]
        [Route("details/{studentcode}")]
        public async Task<ActionResult<ReportDetail>> GetReportDetail(string studentcode)
        {
            return await _mediator.Send(new OjtReportDetail.Query { StudentCode = studentcode });
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="studentCode">Code of student in detail</param>
        /// <param name="info">Information to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("student/{studentCode}")]
        public async Task<ActionResult<Unit>> UpdateReport(string studentCode, Update info)
        {
            var command = new UpdateReport.Command
            {
                Division = info.Division,
                LineManagerName = info.LineManagerName,
                Mark = info.Mark,
                OnWorkDate = info.OnWorkDate,
                StudentCode = studentCode,
                WorkSortDescription = info.WorkSortDescription
            };
            return await _mediator.Send(command);
        }
    }
}
