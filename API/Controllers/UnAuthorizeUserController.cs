using Application.User;
using Application.User.CostomizeResponseObject;
using Application.User.CustomizeRequest;
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
    [Route("unauthorizes")]
    [ApiController]
    public class UnAuthorizeUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnAuthorizeUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///Role: Student, FPT Staff
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> Login(Login.Query query)
        {
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Role: Company
        /// </summary>
        /// <param name="inputAccount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("companies/login")]
        [AllowAnonymous]
        public async Task<ActionResult<CompanyAccount>> CompanyLogin(InputAccount inputAccount)
        {
            var query = new CompanyLogin.Query
            {
                Username = inputAccount.Username,
                Password = inputAccount.Password
            };
            return await _mediator.Send(query);
        }
    }
}
