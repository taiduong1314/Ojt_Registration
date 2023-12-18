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
    public class StudentInCompany
    {
        public class Query : IRequest<List<StudentInList>>
        {
            public string CompanyCode { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<StudentInList>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<StudentInList>> Handle(Query request, CancellationToken cancellationToken)
            {
                //Find Company
                var company_account = await _context
                    .CompanyAccounts
                    .Include(x => x.Company)
                    .FirstOrDefaultAsync(x => x.Code == request.CompanyCode);
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == company_account.Company.Id);
                if (company == null) throw new Exception("Company not found");
                //Find Student
                var student_list = await _context
                    .Students
                    .Where(x => x.CompanyId == company.Id).ToListAsync();

                var result = new List<StudentInList>();
                foreach(Student student in student_list)
                {
                    var one_student = new StudentInList
                    {
                        Position = "internship",
                        StartDate = student.StartDate,
                        EndDate = student.EndDate,
                        Status = student.WorkingStatus,
                        StudentCode = student.StudentCode,
                        StudentName = student.Fullname
                    };
                    result.Add(one_student);
                }
                result.Sort(delegate (StudentInList x, StudentInList y)
                {
                    var StartDateX = DateTime.Parse(x.StartDate);
                    var StartDateY = DateTime.Parse(y.StartDate);
                    if (StartDateX == StartDateY) return 0;
                    if (StartDateX > StartDateY) return -1;
                    else return 1;
                });
                return result;
            }
        }
    }
}
