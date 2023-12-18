using Application.Error;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Semester
{
    public class NewSemester
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
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
                //valid Data
                if(request.EndDate < request.StartDate)
                {
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "End date must be the day after start date");
                }
                //create new semester
                var semester = new Domain.Semester
                {
                    Name = request.Name,
                    EndDate = request.EndDate,
                    StartDate = request.StartDate
                };
                _context.Semesters.Add(semester);
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
