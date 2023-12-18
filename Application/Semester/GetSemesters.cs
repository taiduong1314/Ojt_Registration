using Application.Semester.CsResponse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Semester
{
    public class GetSemesters
    {
        public class Query : IRequest<List<SemesterInList>>
        {

        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<SemesterInList>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<SemesterInList>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list_semester = await _context.Semesters.Where(x => x.StartDate > DateTime.Now.AddYears(-3)).ToListAsync();

                var result = new List<SemesterInList>();

                foreach(Domain.Semester semester in list_semester)
                {
                    var semesterInList = new SemesterInList
                    {
                        EndDate = semester.EndDate,
                        Name = semester.Name,
                        StartDate = semester.StartDate
                    };
                    result.Add(semesterInList);
                }
                return result;
            }
        }
    }
}
