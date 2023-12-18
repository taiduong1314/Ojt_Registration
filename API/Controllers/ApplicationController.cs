using Application.Application;
using Application.Application.CustomizeResponseObject;
using Application.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPdfFileSupport _pdfFileSupport;

        public ApplicationController(IMediator mediator, IPdfFileSupport pdfFileSupport)
        {
            _mediator = mediator;
            _pdfFileSupport = pdfFileSupport;
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="CompanyStaffCode">Staffcode is return in login function</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{CompanyStaffCode}")]
        public async Task<ActionResult<List<ApplicationInList>>> List(string CompanyStaffCode)
        {
            return await _mediator.Send(new List.Query { StaffCode = CompanyStaffCode});
        }

        /// <summary>
        /// Role: Student
        /// </summary>
        /// <param name="command">Application:</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> NewApplication([FromBody] NewApplication.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="id">Id is return in list application</param>
        /// <returns></returns>
        [HttpGet]
        [Route("details/{id}")]
        public async Task<DetailApplication> Details(string id)
        {
            return await _mediator.Send(new DetailApply.Query
            {
                Id = int.Parse(id)
            }); ;
        }

        /// <summary>
        /// Role: Student
        /// </summary>
        /// <param name="CvFile">File Cv</param>
        /// <param name="StudentCode">Student code</param>
        /// <returns></returns>
        [HttpPost("Cv/{StudentCode}")]
        public async Task<ActionResult<string>> SaveCvToFirebase(IFormFile CvFile, string StudentCode)
        {
            return await _pdfFileSupport.UploadFileToFirebase(CvFile, StudentCode);
        }

        /// <summary>
        /// Role: Student
        /// </summary>
        /// <param name="code">Student code is return in Login</param>
        /// <returns></returns>
        [HttpGet]
        [Route("students/{code}")]
        public async Task<ActionResult<List<SubmittedApplication>>> GetSubmittedApplication(string code)
        {
            return await _mediator.Send(new StudentApplication.Query {StudentCode = code});
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="companyCode">Code of company is returned in login function</param>
        /// <param name="applicationId">Id of application returned in list</param>
        /// <returns></returns>
        [HttpPut]
        [Route("approve/{companyCode}/{applicationId}")]
        public async Task<ActionResult<Unit>> ApproveApplication(string companyCode, int applicationId)
        {
            return await _mediator.Send(new AcceptApplication.Command 
            { 
                CompanyCode = companyCode, 
                Id = applicationId 
            });
        }  
        
        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="companyCode">Code of company is returned in login function</param>
        /// <param name="applicationId">Id of application returned in list</param>
        /// <returns></returns>
        [HttpPut]
        [Route("reject/{companyCode}/{applicationId}")]
        public async Task<ActionResult<Unit>> RejectApplication(string companyCode, int applicationId)
        {
            return await _mediator.Send(new DenideApplication.Command 
            { 
                CompanyCode = companyCode, 
                Id = applicationId 
            });
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("fptstaff")]
        public async Task<ActionResult<List<StaffViewApplication>>> StaffView()
        {
            return await _mediator.Send(new ViewListFptStaff.Query());
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <param name="id">Id of application retured in list</param>
        /// <returns></returns>
        [HttpGet]
        [Route("fptstaff/detail/{id}")]
        public async Task<ActionResult<DetailApplicationForStaff>> StaffApplicationDetail(int id)
        {
            return await _mediator.Send(new StaffDetail.Query { Id = id });
        }
    }
}
