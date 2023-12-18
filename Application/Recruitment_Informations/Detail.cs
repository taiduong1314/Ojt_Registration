using Application.Error;
using Application.Recruitment_Informations.CustomizeResponseObject;
using FluentValidation;
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
    public class Detail
    {
        public class Query : IRequest<InformationDetail>
        {
            public int Id { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, InformationDetail>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<InformationDetail> Handle(Query request, CancellationToken cancellationToken)
            {
                var information_detail = await _context
                    .RecruitmentInformations
                    .Include(x => x.Company)
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == false);

                if(information_detail == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No information detail matches");
                }

                var major = await _context.Majors.FirstOrDefaultAsync(x => x.Id == information_detail.MajorId);

                return new InformationDetail
                {
                    Address = information_detail.Company.Address,
                    CompanyName = information_detail.Company.CompanyName,
                    CompanyWebsite = information_detail.Company.WebSite,
                    Content = information_detail.Content,
                    Deadline = information_detail.Deadline,
                    id = information_detail.Id,
                    MajorName = major.MajorName,
                    Position = information_detail.Position,
                    Salary = information_detail.Salary
                };
            }
        }
    }
}
