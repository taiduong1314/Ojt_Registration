using Application.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OjtReport
{
    public class UpdateReport
    {
        public class Command : IRequest
        {
            public string WorkSortDescription { get; set; }
            [Required]
            public string StudentCode { get; set; }
            public double? Mark { get; set; }
            public int? OnWorkDate { get; set; }
            public string Division { get; set; }
            public string LineManagerName { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //Valid Data
                if (request.Mark <= 0)
                {
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest,"Please input the mark is greater than 0");
                }
                if (request.OnWorkDate < 30)
                {
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Work date is greater than 30 days");
                }

                //find report
                var report = await _context
                    .OjtReports
                    .Include(x => x.Student)
                    .FirstOrDefaultAsync(x => x.Student.StudentCode == request.StudentCode);
                
                //Update report
                //Update line manager
                if(request.LineManagerName != null && request.LineManagerName.Length > 0)
                {
                    report.LineManagerName = request.LineManagerName;
                }
                //Update Work sort description
                if (request.WorkSortDescription != null && request.WorkSortDescription.Length > 0)
                {
                    report.WorkSortDescription = request.WorkSortDescription;
                }
                //Update Mark
                if (request.Mark != null)
                {
                    report.Mark = request.Mark;
                }
                //Update On Work Date
                if (request.OnWorkDate != null)
                {
                    report.OnWorkDate = request.OnWorkDate;
                    var startDate = DateTime.Parse(report.Student.StartDate);
                    var endDate = startDate.AddDays((double)request.OnWorkDate);
                    report.Student.EndDate = endDate.ToString();
                }

                _context.OjtReports.Update(report);
                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    return Unit.Value;
                }
                throw new Exception("Problem save changes");
            }
        }
    }
}
