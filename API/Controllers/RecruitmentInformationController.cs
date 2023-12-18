using Application.Recruitment_Informations;
using Application.Recruitment_Informations.CustomizeResponseObject;
using Application.Recruitment_Informations.RequestObject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("recruitment_informations")]
    [ApiController]
    public class RecruitmentInformationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecruitmentInformationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Role: Student
        /// </summary>
        /// <returns>list of recruitment</returns>
        [HttpGet]
        public async Task<List<RecruitmentInListReturn>> ListRecruitment()
        {
            return await _mediator.Send(new ListRecruitment.Query());
        }

        /// <summary>
        /// Role: Student, Company (get detail of recruitment)
        /// </summary>
        /// <param name="id">Id is returned in get list function (id NOT companyId)</param>
        /// <returns>Details of recruitment information follow the input id</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<ActionResult<InformationDetail>> Details(string id)
        {
            return await _mediator.Send(new Detail.Query
            {
                Id = int.Parse(id)
            }); ;
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="Code">Code of employee is returned when company login success</param>
        /// <returns></returns>
        [HttpGet("company/{Code}")]
        public async Task<ActionResult<List<RecruitmentInListReturn>>> RecruitmentOfComany(string Code)
        {
            return await _mediator.Send(new GetRecruitmentCompany.Query
            {
                CompanyCode = Code
            });
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="id">Id of recruitment information returned in detail</param>
        /// <param name="companyCode">Code of company returned in login</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/{companyCode}")]
        public async Task<ActionResult<Unit>> DeleteRecruitmentInformation(int id, string companyCode)
        {
            return await _mediator.Send(new DeleteById.Command {Id = id, CompanyCode = companyCode});
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="information">All information to create new recruitment information</param>
        /// <param name="companyCode">Code of company returned in login</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{companyCode}")]
        public async Task<ActionResult<Unit>> AddNewRecruitmentInformation(AddNew information, string companyCode)
        {
            var command = new AddNewInformation.Command();
            command.Information = information;
            command.CompanyCode = companyCode;
            return await _mediator.Send(command);
        }
    }
}
