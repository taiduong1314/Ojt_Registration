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
    public class ViewListFptStaff
    {
        public class Query : IRequest<List<StaffViewApplication>>
        {

        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<StaffViewApplication>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<StaffViewApplication>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list_application = await _context
                    .RecruimentApplies
                    .Include(x => x.Student)
                    .Include(x => x.RecruimentInformation).ThenInclude(x => x.Company)
                    .Where(x => x.RegistrationDate >= DateTime.Now.AddMonths(-4))
                    .ToListAsync();

                var result = new List<StaffViewApplication>();

                foreach(RecruimentApply apply in list_application)
                {
                    var staffViewApplication = new StaffViewApplication
                    {
                        CompanyName = apply.RecruimentInformation.Company.CompanyName,
                        Fullname = apply.Student.Fullname,
                        GPA = apply.Student.Gpa,
                        Id = apply.Id,
                        Status = apply.Status,
                        StudentCode = apply.Student.StudentCode,
                        UpdateDate = apply.UpdateDate
                    };
                    result.Add(staffViewApplication);
                }
                result.Sort(delegate (StaffViewApplication x, StaffViewApplication y)
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
