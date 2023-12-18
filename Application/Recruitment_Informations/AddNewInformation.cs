using Application.Error;
using Application.Recruitment_Informations.RequestObject;
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

namespace Application.Recruitment_Informations
{
    public class AddNewInformation
    {
        public class Command : IRequest
        {
            public string CompanyCode { get; set; }
            public AddNew Information { get; set; }
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
                //find company
                var company_account = await _context.CompanyAccounts.Include(x => x.Company).FirstOrDefaultAsync(x => x.Code == request.CompanyCode);
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == company_account.Company.Id);

                if(company == null)
                {
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Company code not found");
                }

                //find major
                var major = await _context.Majors.FirstOrDefaultAsync(x => x.MajorName == request.Information.MajorName);

                if(major == null)
                {
                    var newMajor = new Major { MajorName = request.Information.MajorName };
                    _context.Majors.Add(newMajor);
                    await _context.SaveChangesAsync();
                    major = await _context.Majors.FirstOrDefaultAsync(x => x.MajorName == request.Information.MajorName);
                }

                //Create new information
                var information = new RecruitmentInformation
                {
                    Area = request.Information.Area,
                    Company = company,
                    Content = request.Information.Content,
                    Deadline = request.Information.Deadline,
                    IsDeleted = false,
                    Position = "Internship",
                    Salary = request.Information.Salary,
                    Topic = request.Information.Topic,
                    MajorId = major.Id
                };

                _context.RecruitmentInformations.Add(information);
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
