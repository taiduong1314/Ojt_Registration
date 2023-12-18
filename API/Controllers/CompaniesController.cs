using Application.Companies;
using Application.Companies.RequestObject;
using Application.Companies.ResponseObject;
using Application.Students;
using Application.Students.CustomizeRequestObject;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Role: Fpt Staff
        /// </summary>
        /// <param name="staffcode">Staff is return in login function</param>
        /// <returns></returns>
        [HttpGet]
        [Route("fptstaff/{staffcode}")]
        public async Task<ActionResult<List<CompanyInList>>> getListCompany(string staffcode)
        {
            return await _mediator.Send(new GetAllCompany.Query { StaffCode = staffcode });
        }

        /// <summary>
        /// Role: Fpt staff
        /// </summary>
        /// <param name="companyId">Company id is return in list of company</param>
        /// <returns></returns>
        [HttpGet]
        [Route("account/{companyId}")]
        public async Task<ActionResult<List<AccountInCompany>>> getAccountOfCompany(int companyId)
        {
            return await _mediator.Send(new GetCompanyAccount.Query { CompanyId = companyId });
        }

        /// <summary>
        /// Role: FPT staff
        /// </summary>
        /// <param name="request">All contain to create new account</param>
        /// <param name="companyId">Id is return in list company</param>
        /// <returns></returns>
        [HttpPost]
        [Route("newaccount/{companyId}")]
        public async Task<ActionResult<Unit>> CreateNewCompanyAccount(NewCompanyAccount request, int companyId)
        {
            var command = new CreateNewCompanyAccount.Command
            {
                CompanyId = companyId,
                Email = request.Email,
                Fullname = request.Fullname,
                Password = request.Password,
                Username = request.Username
            };
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <param name="request">Information of new company</param>
        /// <returns></returns>
        [HttpPost]
        [Route("newcompany")]
        public async Task<ActionResult<Unit>> CreateNewCompanyInfo(NewCompany request)
        {
            var command = new CreateNewCompanyInfo.Command
            {
                Address = request.Address,
                CompanyName = request.CompanyName,
                WebSite = request.WebSite
            };
            return await _mediator.Send(command);
        }
        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="oldpassword">Old password</param>
        /// <param name="password">New password</param>
        /// <returns></returns>
        [HttpPut]
        [Route("account/update")]
        public async Task<ActionResult<Unit>> UpdatePassword(string username, string password,string oldpassword)
        {
            var command = new UpdatePassword.Command
            {
                Username = username,
                OldPassword = oldpassword,
                NewPassword = password
            };
            return await _mediator.Send(command);
        }
        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("subcompanies")]
        public async Task<ActionResult<List<SubCompany>>> GetSubCompany()
        {
            return await _mediator.Send(new GetSubCompany.Query());
        }

        /// <summary>
        /// Role: FPT Staff
        /// </summary>
        /// <param name="companyid">Id returned in get list subcompany</param>
        /// <param name="startdate">Date student start to work</param>
        /// <param name="liststudent">List student's code of student apply to this sub company</param>
        /// <returns></returns>
        [HttpPut]
        [Route("subcompany/apply/{companyid}")]
        public async Task<ActionResult<Unit>> AddStudent(int companyid, DateTime startdate, List<StudentNotInWork> liststudent)
        {
            var command = new AddStudentToSubCompany.Command
            {
                CompanyId = companyid,
                StartDate = startdate,
                Students = liststudent
            };
            return await _mediator.Send(command);
        }
    }
}
