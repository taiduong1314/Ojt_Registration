using Application.Error;
using Application.Students.CustomizeResponseObject;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Students
{
    public class ListStudent
    {
        public class Query : IRequest<List<ExportStudent>>
        {
            public int status { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<ExportStudent>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<ExportStudent>> Handle(Query request, CancellationToken cancellationToken)
            {
                var student_list = new List<Student>();
                //get not in work student
                if (request.status == 0)
                {
                    student_list = await _context.Students.Where(x => x.WorkingStatus.Trim() == "Not in work").ToListAsync();
                }
                //get working student
                else if (request.status == 1)
                {
                    student_list = await _context.Students.Where(x => x.WorkingStatus.Trim() == "Working").ToListAsync();
                }
                //get finished student
                else if (request.status == 2)
                {
                    student_list = await _context.Students.Where(x => x.WorkingStatus.Trim() == "Finished").ToListAsync();
                }

                if(student_list.Count == 0 || student_list == null)
                {
                    throw new SearchResultException(HttpStatusCode.NotFound, "No student matched with this status");
                }

                var result = new List<ExportStudent>();

                foreach(Student student in student_list)
                {
                    var exportStudent = new ExportStudent
                    {
                        Birthday = student.Birthday,
                        Credit = (int)student.Credit,
                        Email = student.Email,
                        Fullname = student.Fullname,
                        Gender = student.Gender,
                        Gpa = student.Gpa,
                        Id = student.Id,
                        Phone = student.Phone,
                        StudentCode = student.StudentCode,
                        Term = (int)student.Term,
                        WorkingStatus = student.WorkingStatus
                    };
                    result.Add(exportStudent);
                }
                return result;
            }
        }
    }
}
