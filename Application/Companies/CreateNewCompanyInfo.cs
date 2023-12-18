using Application.Error;
using Application.Interface;
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

namespace Application.Companies
{
    public class CreateNewCompanyInfo
    {
        public class Command : IRequest
        {
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public string WebSite { get; set; }
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

                var infomation = new Company
                {
                    CompanyName = request.CompanyName,
                    Address = request.Address,
                    WebSite = request.WebSite,
                    LastInteractDate = DateTime.Now
                };

                _context.Companies.Add(infomation);

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
