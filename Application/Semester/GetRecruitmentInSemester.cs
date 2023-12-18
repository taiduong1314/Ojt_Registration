using Application.Error;
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
    public class GetRecruitmentInSemester
    {
        public class Query : IRequest<List<RecruitmentInSemester>>
        {
            public string SemesterName { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<RecruitmentInSemester>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public string getMajorName(List<Major> list, int id)
            {
                string result = "";
                foreach(Major major in list)
                {
                    if(major.Id == id)
                    {
                        result = major.MajorName;
                        break;
                    }
                }
                return result;
            }

            public async Task<List<RecruitmentInSemester>> Handle(Query request, CancellationToken cancellationToken)
            {
                //Find semester
                var semester = await _context.Semesters.FirstOrDefaultAsync(x => x.Name == request.SemesterName);

                if (semester == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No semester match with this name");
                }

                var information_list = await _context
                    .RecruitmentInformations
                    .Include(x => x.Company)
                    .Where(x => x.Deadline >= semester.StartDate && x.Deadline <= semester.EndDate)
                    .ToListAsync();

                if(information_list.Count == 0 || information_list == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No Information in this semester");
                }

                var major_list = await _context.Majors.ToListAsync();

                var result = new List<RecruitmentInSemester>();

                foreach(RecruitmentInformation recruitment in information_list)
                {
                    var information = new RecruitmentInSemester
                    {
                        Area = recruitment.Area,
                        companyName = recruitment.Company.CompanyName,
                        content = recruitment.Content,
                        Deadline = recruitment.Deadline,
                        MajorName = getMajorName(major_list, recruitment.MajorId),
                        Salary = recruitment.Salary,
                        Topic = recruitment.Topic
                    };
                    result.Add(information);
                }
                return result;
            }
        }
    }
}
