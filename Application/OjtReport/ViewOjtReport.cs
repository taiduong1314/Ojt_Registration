using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.OjtReport.CustomizeResponseObject;
using Persistence;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Application.Error;
using Domain;

namespace Application.OjtReport
{

    public class ViewOjtReport
    {
        public class Query : IRequest<List<ReportDetailInList>>
        {

        }
        public class Handler : IRequestHandler<Query, List<ReportDetailInList>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<ReportDetailInList>> Handle(Query request, CancellationToken cancellationToken)

            {
                var report_details = await _context.OjtReports.Include(x => x.Student).Where(x => x.Public_Date >= DateTime.Now.AddMonths(-4)).ToListAsync();
                if (report_details == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No report found");
                }
                var result = new List<ReportDetailInList>();
                foreach (OjtReports report in report_details)
                {
                    var report_detail = new ReportDetailInList
                    {
                        StudentCode = report.Student.StudentCode,
                        StudentName = report.Student.Fullname,
                        Mark = report.Mark,
                        WorkSortDescription = report.WorkSortDescription,
                        PublicDate = report.Public_Date
                    };
                    result.Add(report_detail);

                }
                result.Sort(delegate (ReportDetailInList x, ReportDetailInList y)
                {
                    if (x.PublicDate == y.PublicDate) return 0;
                    if (x.PublicDate > y.PublicDate) return -1;
                    else return 1;
                });
                return result;
            }
        }
    }
}

