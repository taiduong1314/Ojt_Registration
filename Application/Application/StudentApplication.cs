using Application.Application.CustomizeResponseObject;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Application
{
    public class StudentApplication
    {
        public class Query : IRequest<List<SubmittedApplication>>
        {
            public string StudentCode { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<SubmittedApplication>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<SubmittedApplication>> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _context
                    .Students
                    .Include(x => x.RecruimentApplies)
                    .ThenInclude(x => x.RecruimentInformation)
                    .ThenInclude(x => x.Company)
                    .FirstOrDefaultAsync(x => x.StudentCode == request.StudentCode);

                var result = new List<SubmittedApplication>();

                foreach(RecruimentApply application in student.RecruimentApplies)
                {
                    var submettedApplication = new SubmittedApplication
                    {
                        CompanyName = application.RecruimentInformation.Company.CompanyName,
                        Status = application.Status,
                        StudentCode = student.StudentCode,
                        StudentName = student.Fullname,
                        UpdateDate = application.UpdateDate,
                        Topic = application.RecruimentInformation.Topic
                    };
                    result.Add(submettedApplication);
                }
                result.Sort(delegate (SubmittedApplication x, SubmittedApplication y)
                {
                    if (x.UpdateDate == y.UpdateDate) return 0;
                    if (x.UpdateDate > y.UpdateDate) return -1;
                    else return 1;
                });
                return result;
            }
        }
    }
}
