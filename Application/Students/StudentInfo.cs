using Application.Error;
using Application.Students.CustomizeResponseObject;
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

namespace Application.Students
{
    public class StudentInfo
    {
        public class Query : IRequest<StudentDetailReturn>
        {
            public string StudentCode { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, StudentDetailReturn>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<StudentDetailReturn> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _context.Students.Include(x => x.Major).FirstOrDefaultAsync(x => x.StudentCode == request.StudentCode);

                if(student == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No student matches with the student code "+request.StudentCode);
                }

                return new StudentDetailReturn
                {
                    Major = student.Major.MajorName,
                    BirthDate = student.Birthday,
                    Email = student.Email,
                    FullName = student.Fullname,
                    Gender = student.Gender,
                    Phone = student.Phone,
                    StuCode = student.StudentCode,
                    Term = student.Term,
                    Gpa = student.Gpa
                };
            }
        }
    }
}
