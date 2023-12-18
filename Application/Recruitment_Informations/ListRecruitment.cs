using Application.Recruitment_Informations.CustomizeResponseObject;
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

namespace Application.Recruitment_Informations
{
    public class ListRecruitment
    {
        public class Query : IRequest<List<RecruitmentInListReturn>>
        {

        }
        public class Handler : IRequestHandler<Query, List<RecruitmentInListReturn>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            private string getMajorName(List<Major> list, int id)
            {
                foreach (Major major in list)
                {
                    if (major.Id == id)
                    {
                        return major.MajorName;
                    }
                }
                return null;
            }
            public async Task<List<RecruitmentInListReturn>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list_recruitment = await _context
                    .RecruitmentInformations
                    .Include(x => x.Company)
                    .Where(x => x.Deadline >= DateTime.Now && x.IsDeleted == false)
                    .ToListAsync();

                var list_major = await _context.Majors.ToListAsync();
                var result = new List<RecruitmentInListReturn>();

                foreach(RecruitmentInformation infor in list_recruitment)
                {
                    var recruitment = new RecruitmentInListReturn
                    {
                        Area = infor.Area,
                        CompanyName = infor.Company.CompanyName,
                        Deadline = infor.Deadline,
                        Id = infor.Id,
                        MajorName = getMajorName(list_major, infor.MajorId),
                        Salary = infor.Salary,
                        Topic = infor.Topic
                    };
                    result.Add(recruitment);
                }
                result.Sort(delegate (RecruitmentInListReturn x, RecruitmentInListReturn y)
                {
                    if (x.Deadline == y.Deadline) return 0;
                    if (x.Deadline > y.Deadline) return 1;
                    else return -1;
                });
                return result;
            }
        }
    }
}
