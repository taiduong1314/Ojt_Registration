using Application.Error;
using Application.Interface;
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
    public class UpdatePassword
    {
        public class Command : IRequest
        {
            public string Username { get; set; }
            public string NewPassword { get; set; }
            public string OldPassword { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IHasingSupport _hasingSupport;

            public Handler(DataContext context,IHasingSupport hasingSupport)
            {

                _context = context;
                _hasingSupport = hasingSupport;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var account = await _context.CompanyAccounts.FirstOrDefaultAsync(x => x.Username == request.Username);

                string hasingOldPassword = _hasingSupport.encriptSHA256(request.OldPassword);
                if (account.Password != hasingOldPassword)
                {
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Invalid Password");
                }

                if(request.NewPassword != null && request.NewPassword.Length > 0)
                {
                    string hasingNewPassword = _hasingSupport.encriptSHA256(request.NewPassword);
                    account.Password = hasingNewPassword;
                }
                
                _context.CompanyAccounts.Update(account);
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
