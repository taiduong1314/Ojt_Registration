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
    public class DenideApplication
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
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                var company_account = await _context.CompanyAccounts.Include(x => x.Company).FirstOrDefaultAsync(x => x.Code == request.CompanyCode);

                if (application.RecruimentInformation.Company.Id != company_account.Company.Id)
                {
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Application not related to your company");
                }

                if (application.Status == "Approved")
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Can't reject the approved application");

                application.Status = "Rejected";
                application.UpdateDate = DateTime.Now;
                application.Student.WorkingStatus = "Not in work";
                application.Student.CanSendApplication = true;

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
