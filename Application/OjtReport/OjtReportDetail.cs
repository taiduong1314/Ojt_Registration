using Application.Error;
using Application.OjtReport.CustomizeResponseObject;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OjtReport
{
    public class OjtReportDetail
    {
        public class Query : IRequest<ReportDetail>
        {
            public string StudentCode { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, ReportDetail>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<ReportDetail> Handle(Query request, CancellationToken cancellationToken)
            {
                var report = await _context
                    .OjtReports
                    .Include(x => x.Student)
                    .FirstOrDefaultAsync(x => x.Student.StudentCode == request.StudentCode);

                if(report == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "This student has no report yet");
                }

                return new ReportDetail
                {
                    Description = report.WorkSortDescription,
                    EndDate = report.Student.EndDate,
                    Mark = report.Mark,
                    Name = report.Student.Fullname,
                    Position = "Interm",
                    StartDate = report.Student.StartDate
                };
            }
        }
    }
}
