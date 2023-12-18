using Application.Error;
using Application.Interface;
using Application.User.CostomizeResponseObject;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class CompanyLogin
    {
        public class Query : IRequest<CompanyAccount>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, CompanyAccount>
        {
            private readonly DataContext _context;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IHasingSupport _hasingSupport;

            public Handler(DataContext context, IJwtGenerator jwtGenerator, IHasingSupport hasingSupport)
            {
                _context = context;
                _jwtGenerator = jwtGenerator;
                _hasingSupport = hasingSupport;
            }
            public async Task<CompanyAccount> Handle(Query request, CancellationToken cancellationToken)
            {
                var company_account = await _context.CompanyAccounts.Include(x => x.Company).FirstOrDefaultAsync(x => x.Username == request.Username);

                if(company_account == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.BadRequest, "Invalid Username/Password");
                }

                //hasing password
                string hassedPassword = _hasingSupport.encriptSHA256(request.Password);

                if (company_account.Password == hassedPassword)
                {
                    company_account.Company.LastInteractDate = DateTime.Now;
                    _context.CompanyAccounts.Update(company_account);
                    await _context.SaveChangesAsync();

                    return new CompanyAccount
                    {
                        Code = company_account.Code,
                        CompanyName = company_account.Company.CompanyName,
                        Name = company_account.Fullname,
                        Role = 2,
                        Token = _jwtGenerator.CreateToken(company_account.Email, company_account.Fullname)
                    };
                }

                throw new SearchResultException(System.Net.HttpStatusCode.BadRequest, "Invalid Username/Password");
            }
        }
    }
}
