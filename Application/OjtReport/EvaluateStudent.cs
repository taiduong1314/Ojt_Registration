using Domain;
using FluentValidation;
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
    public class EvaluateStudent
    {
        public class Command : IRequest
        {
            public string WorkSortDescription { get; set; }
            public string StudentCode { get; set; }
            public string CompanyCode { get; set; }
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
                //validate data
                if(request.Mark < 0)
                {
                    throw new Exception("Please input the mark is greater than 0");
                }
                if(request.OnWorkDate < 30)
                {
                    throw new Exception("Work date is greater than 30 days");
                }

                //Find student
                var student = await _context.Students
                    .FirstOrDefaultAsync(x => x.StudentCode == request.StudentCode);

                //Find company
                var company_account = await _context.CompanyAccounts.Include(x => x.Company).FirstOrDefaultAsync(x => x.Code == request.CompanyCode);
                var company = await _context.Companies
                    .FirstOrDefaultAsync(x => x.Id == company_account.Company.Id);

                //Create Report
                var report = new OjtReports
                {
                    Company = company,
                    Division = request.Division,
                    LineManagerName = request.LineManagerName,
                    Mark = request.Mark,
                    OnWorkDate = request.OnWorkDate,
                    Public_Date = DateTime.Now,
                    Student = student,
                    WorkSortDescription = request.WorkSortDescription
                };

                _context.OjtReports.Add(report);

                //Update endate of student
                student.EndDate = DateTime.Now.ToString();
                student.WorkingStatus = "Finished";
                _context.Students.Update(student);

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
