using Application.Companies.ResponseObject;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Companies
{
    public class GetCompanyAccount
    {
        public class Query : IRequest<List<AccountInCompany>>
        {
            public int CompanyId { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<AccountInCompany>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<AccountInCompany>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list_account = await _context
                    .CompanyAccounts
                    .Include(x => x.Company)
                    .Where(x => x.Company.Id == request.CompanyId)
                    .ToListAsync();

                var result = new List<AccountInCompany>();

                foreach(Domain.CompanyAccount account in list_account)
                {
                    var returnAccount = new AccountInCompany
                    {
                        Code = account.Code,
                        Email = account.Email,
                        Fullname = account.Fullname,
                        Username = account.Username
                    };
                    result.Add(returnAccount);
                }

                result.Sort(delegate (AccountInCompany x, AccountInCompany y)
                {
                    return x.Username.CompareTo(y.Username);
                });

                return result;
            }
        }
    }
}
