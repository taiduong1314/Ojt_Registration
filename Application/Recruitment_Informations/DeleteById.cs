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

namespace Application.Recruitment_Informations
{
    public class DeleteById
    {
        public class Command : IRequest
        {
            [Required]
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
                var information = await _context.RecruitmentInformations.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == request.Id);

                if (information == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No matches recruitment to delete");
                }

                //get company account
                var company_account = await _context
                    .CompanyAccounts
                    .Include(x => x.Company)
                    .FirstOrDefaultAsync(x => x.Code == request.CompanyCode);
                if (information.Company.Id != company_account.Company.Id)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.BadRequest, "It's not your company recruitment information");
                }

                information.IsDeleted = true;

                _context.RecruitmentInformations.Update(information);

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
