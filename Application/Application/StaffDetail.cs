using Application.Application.CustomizeResponseObject;
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

namespace Application.Application
{
    public class StaffDetail
    {
        public class Query : IRequest<DetailApplicationForStaff>
        {
            public int Id { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, DetailApplicationForStaff>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<DetailApplicationForStaff> Handle(Query request, CancellationToken cancellationToken)
            {
                var application = await _context
                    .RecruimentApplies
                    .Include(x => x.Student).ThenInclude(x => x.Major)
                    .Include(x => x.RecruimentInformation).ThenInclude(x => x.Company)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if(application == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No matched application with id: " + request.Id);
                }

                return new DetailApplicationForStaff
                {
                    CompanyName = application.RecruimentInformation.Company.CompanyName,
                    Cv = application.Cv,
                    Email = application.Student.Email,
                    Fullname = application.Student.Fullname,
                    Gpa = application.Student.Gpa,
                    MajorName = application.Student.Major.MajorName,
                    StudentCode = application.Student.StudentCode,
                };
            }
        }
    }
}
