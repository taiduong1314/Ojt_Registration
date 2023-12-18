using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Students.CustomizeRequestObject;
using Persistence;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Application.Error;
using Domain;

namespace Application.Students
{
    public class NewStudent
    {
        public class Command : IRequest
        {
            public List<AddNewStudent> Students;
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public int GetMajorId(List<Major> majors, string majorName)
            {
                foreach (Major major in majors)
                {
                    if (majorName.ToUpper() == major.MajorName)
                    {
                        return major.Id;
                    }
                }
                return -1;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var listmajor = await _context.Majors.ToListAsync();

                foreach (AddNewStudent student in request.Students)
                {
                    var StudentinDomain = new Student
                    {
                        Phone = student.Phone,
                        Birthday = student.Birthday,
                        Term = student.Term,
                        Credit = student.Credit,
                        Gpa = student.Gpa,
                        StudentCode = student.StudentCode,
                        Email = student.Email,
                        Fullname = student.Fullname,
                        Gender = student.Gender,
                        MajorId = GetMajorId(listmajor, student.MajorName),
                        WorkingStatus = "Not in work",
                        CanSendApplication = true,

                    };

                    if(StudentinDomain.MajorId == -1)
                    {
                        throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Invalid MajorName "+student.MajorName+" of student: "+StudentinDomain.StudentCode);
                    }

                    //Check exist studentCode
                    var student_list = await _context.Students.ToListAsync();

                    foreach(Student checkStudent in student_list)
                    {
                        foreach (Student studentInList in student_list)
                        {
                            if (student.StudentCode == checkStudent.StudentCode)
                            {
                                throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "Student code: " + checkStudent.StudentCode + " is duplicated");
                            }
                        }
                    }

                    _context.Students.Add(StudentinDomain);
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