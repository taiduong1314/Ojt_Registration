using Application.Error;
using Application.Students.CustomizeRequestObject;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Students
{
    public class AddStudentToSubCompany
    {
        public class Command : IRequest
        {
            public List<StudentNotInWork> Students;
            public int CompanyId;
            public DateTime StartDate;
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
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId);

                foreach(StudentNotInWork student in request.Students)
                {
                    var studentnotinwork = await _context.Students.FirstOrDefaultAsync(x => x.StudentCode == student.StudentCode);
                    if(studentnotinwork.WorkingStatus != "Not in work")
                    {
                        throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Student "+ studentnotinwork.StudentCode +" is working or finish" );
                    }
                    studentnotinwork.Company = company;
                    studentnotinwork.WorkingStatus = "Working";
                    studentnotinwork.StartDate = request.StartDate.ToString();
                    _context.Students.Update(studentnotinwork);
                }

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
