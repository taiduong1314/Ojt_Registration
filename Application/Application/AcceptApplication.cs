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
    public class AcceptApplication
    {
        public class Command : IRequest
        {
            public string CompanyCode { get; set; }
            public int Id { get; set; }
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
                var application = await _context
                    .RecruimentApplies
                    .Include(x => x.Student)
                    .Include(x => x.RecruimentInformation)
                    .ThenInclude(x => x.Company)
                    .ThenInclude(x => x.CompanyAccounts)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                var company_Account = await _context.CompanyAccounts.Include(x => x.Company).FirstOrDefaultAsync(x => x.Code == request.CompanyCode);

                if (company_Account.Company.Id != application.RecruimentInformation.Company.Id)
                {
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Application not related to your company");
                }

                if (application.Status == "Rejected")
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Can't approve the rejected application");

                application.Status = "Approved";
                application.UpdateDate = DateTime.Now;
                application.Student.WorkingStatus = "Working";

                _context.RecruimentApplies.Update(application);

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
