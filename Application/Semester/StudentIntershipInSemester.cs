using Application.Error;
using Application.Interface;
using Application.Semester.CsResponse;
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

namespace Application.Semester
{
    public class StudentIntershipInSemester
    {
        public class Query : IRequest<List<StudentInSemester>>
        {
            public string SemesterName { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<StudentInSemester>>
        {
            private readonly DataContext _context;
            private readonly IHasingSupport _hasingSupport;

            public Handler(DataContext context, IHasingSupport hasingSupport)
            {
                _context = context;
                _hasingSupport = hasingSupport;
            }
            public async Task<List<StudentInSemester>> Handle(Query request, CancellationToken cancellationToken)
            {
                //Find semester
                var semester = await _context.Semesters.FirstOrDefaultAsync(x => x.Name == request.SemesterName);

                if(semester == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No semester match with this name");
                }

                //Find student working or finished
                var list_student = await _context.Students.Include(x => x.Company).Include(x => x.Major).Where(x => x.WorkingStatus != "Not in work").ToListAsync();
                
                var result = new List<StudentInSemester>();
                //find student in semester
                foreach(Student student in list_student)
                {
                    var startDate = DateTime.Parse(student.StartDate);
                    if(startDate >= semester.StartDate && startDate < semester.EndDate)
                    {
                        var studentInSemester = new StudentInSemester
                        {
                            CompanyName = student.Company.CompanyName,
                            Email = student.Email,
                            EndDate = _hasingSupport.parseEndDate(student.EndDate),
                            Fullname = student.Fullname,
                            Gpa = student.Gpa,
                            MajorName = student.Major.MajorName,
                            Phone = student.Phone,
                            StartDate = student.StartDate,
                            StudentCode = student.StudentCode,
                            Term = student.Term,
                            WorkingStatus = student.WorkingStatus
                        };
                        result.Add(studentInSemester);
                    }
                }
                if(result.Count == 0)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No Student Intership in this semester");
                }
                return result;
            }
        }
    }
}
