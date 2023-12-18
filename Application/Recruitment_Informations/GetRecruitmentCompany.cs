using Application.Error;
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
    public class GetRecruitmentCompany
    {
        public class Query : IRequest<List<RecruitmentInListReturn>>
        {
            public string CompanyCode { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<RecruitmentInListReturn>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            private string getMajorName(List<Major> list, int id)
            {
                foreach(Major major in list)
                {
                    if(major.Id == id)
                    {
                        return major.MajorName;
                    }
                }
                return null;
            }
            public async Task<List<RecruitmentInListReturn>> Handle(Query request, CancellationToken cancellationToken)
            {
                var company_account = await _context.CompanyAccounts.Include(x => x.Company).FirstOrDefaultAsync(x => x.Code == request.CompanyCode);
                //get recruitment information of company
                var list_recruitment = await _context
                    .RecruitmentInformations
                    .Include(x => x.Company)
                    .Where(x => x.Company.Id == company_account.Company.Id && x.Deadline > DateTime.UtcNow.AddDays(-1) && x.IsDeleted == false)
                    .ToListAsync();
                if(list_recruitment == null || list_recruitment.Count == 0)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "Your company have no post yet!");
                }
                //get list major:
                var major_list = await _context.Majors.ToListAsync();
                var result = new List<RecruitmentInListReturn>();

                foreach(RecruitmentInformation recruitment in list_recruitment)
                {
                    var recruitmentInList = new RecruitmentInListReturn
                    {
                        Area = recruitment.Area,
                        CompanyName = recruitment.Company.CompanyName,
                        Deadline = recruitment.Deadline,
                        Id = recruitment.Id,
                        MajorName = getMajorName(major_list, recruitment.MajorId),
                        Salary = recruitment.Salary,
                        Topic = recruitment.Topic,
                    };
                    result.Add(recruitmentInList);
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
