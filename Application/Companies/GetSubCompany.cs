using Application.Companies.ResponseObject;
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

namespace Application.Companies
{
    public class GetSubCompany
    {
        public class Query : IRequest<List<SubCompany>>
        {

        }
        //get access to db context
        public class Handler : IRequestHandler<Query, List<SubCompany>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public string getMajorName(List<Major> major_list, int id)
            {
                foreach(Major major in major_list)
                {
                    if (major.Id == id) return major.MajorName;
                }
                return null;
            }

            public async Task<List<SubCompany>> Handle(Query request, CancellationToken cancellationToken)
            {
                var listcompany = await _context.Companies.Where(x => x.IsSubCompany == true).ToListAsync();

                var result = new List<SubCompany>();
                foreach (Company company in listcompany)
                {
                    var companyMajors = await _context.MajorCompanies.Include(x => x.Majors).Where(x => x.CompanyId == company.Id).ToListAsync();

                    var major_list = await _context.Majors.ToListAsync();

                    var majorNames = new List<string>();

                    foreach(MajorCompany mc in companyMajors)
                    {
                        majorNames.Add(getMajorName(major_list, mc.MajorId));
                    };

                    var returnCompany = new SubCompany
                    {
                        Address = company.Address,
                        CompanyName = company.CompanyName,
                        Id = company.Id,
                        Website = company.WebSite,
                        MajorName = majorNames
                    };
                    result.Add(returnCompany);
                }
                result.Sort(delegate (SubCompany x, SubCompany y)
                {
                    return x.CompanyName.CompareTo(y.CompanyName);
                });
                return result;
            }
        }
    }
}

