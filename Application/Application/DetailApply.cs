using Application.Application.CustomizeResponseObject;
using Application.Error;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Application
{
    public class DetailApply
    {
        public class Query : IRequest<DetailApplication>
        {
            public int Id { get; set; }
        }
        //get access to db context
        public class Handler : IRequestHandler<Query, DetailApplication>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<DetailApplication> Handle(Query request, CancellationToken cancellationToken)
            {
                var apply_detail = await _context.RecruimentApplies.Include(x => x.Student).FirstOrDefaultAsync(x => x.Id == request.Id);

                if (apply_detail == null)
                {
                    throw new SearchResultException(System.Net.HttpStatusCode.NotFound, "No application detail matches");
                }
                return new DetailApplication
                {
                    Id = request.Id,
                    Fullname = apply_detail.Student.Fullname,
                    StudentCode = apply_detail.Student.StudentCode,
                    Email = apply_detail.Student.Email,
                    Position = "Interm",
                    Gpa = apply_detail.Student.Gpa,
                    Cv = apply_detail.Cv
                };
            }
        }
    }
}
