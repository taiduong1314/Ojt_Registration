using Application.Application.CustomizeResponseObject;
using Application.Error;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Application
{
    public class List
    {
        public class Query : IRequest<List<ApplicationInList>>
        {
            public string StaffCode { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<ApplicationInList>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<ApplicationInList>> Handle(Query request, CancellationToken cancellationToken)
            {
                var company_account = await _context.CompanyAccounts.Include(x => x.Company).FirstOrDefaultAsync(x => x.Code == request.StaffCode);

                var application_list = await _context
                    .RecruimentApplies
                    .Include(x => x.Student)
                    .Include(x => x.RecruimentInformation)
                    .ThenInclude(x => x.Company)
                    .Where(x => x.RegistrationDate > DateTime.UtcNow.AddMonths(-4) && x.RecruimentInformation.Company.Id == company_account.Company.Id)
                    .ToListAsync();

                if (application_list == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No application found");
                }
                var result = new List<ApplicationInList>();
                foreach (RecruimentApply listapp in application_list)
                {
                    var applicationlist = new ApplicationInList
                    {
                        Id = listapp.Id,
                        StudentCode = listapp.Student.StudentCode,
                        Fullname = listapp.Student.Fullname,
                        Position = "Intern",
                        Status = listapp.Status,
                        RegistrationDate = listapp.RegistrationDate
                    };
                    result.Add(applicationlist);
                }
                result.Sort(delegate (ApplicationInList x, ApplicationInList y)
                {
                    if (x.RegistrationDate == y.RegistrationDate) return 0;
                    if (x.RegistrationDate > y.RegistrationDate) return -1;
                    else return 1;
                });
                return result;
            }
        }
    }
}
