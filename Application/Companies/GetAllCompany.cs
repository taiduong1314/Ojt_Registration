using Application.Companies.ResponseObject;
using Application.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Application.Companies
{
    public class GetAllCompany
    {
        public class Query : IRequest<List<CompanyInList>>
        {
            public string StaffCode { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<CompanyInList>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<CompanyInList>> Handle(Query request, CancellationToken cancellationToken)
            {
                var staff = await _context.FptStaffs.FirstOrDefaultAsync(x => x.Code == request.StaffCode);

                if(staff == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.BadRequest, "Only Fpt staff can get it");
                }

                var companies = await _context.Companies.Where(x => x.LastInteractDate > DateTime.Now.AddMonths(-6)).ToListAsync();

                var result = new List<CompanyInList>();

                foreach(Company company in companies)
                {
                    var returnCompany = new CompanyInList
                    {
                        Address = company.Address,
                        CompanyName = company.CompanyName,
                        Id = company.Id,
                        WebSite = company.WebSite
                    };
                    result.Add(returnCompany);
                }

                result.Sort(delegate (CompanyInList x, CompanyInList y)
                {
                    return x.CompanyName.CompareTo(y.CompanyName);
                });

                return result;
            }
        }
    }
}
