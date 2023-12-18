using Application.Error;
using Application.Interface;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Application
{
    public class NewApplication
    {
        public class Command : IRequest
        {
            public string Cv { get; set; }
            public int? RecruimentInformationId { get; set; }
            public string StudentCode { get; set; }
            public string StudentName { get; set; }
            public string CoverLetter { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IPdfFileSupport _pdfFileSupport;

            public Handler(DataContext context, IPdfFileSupport pdfFileSupport)
            {
                _context = context;
                _pdfFileSupport = pdfFileSupport;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var recruitment_information = await _context.RecruitmentInformations.FirstOrDefaultAsync(x => x.Id == request.RecruimentInformationId);

                var student_apply = await _context.Students.FirstOrDefaultAsync(x => x.StudentCode == request.StudentCode);

                //Verify student
                //check student can send application
                if (!student_apply.CanSendApplication) 
                    throw new UpdateError(System.Net.HttpStatusCode.BadRequest, "You can't send the application now, please check your profile again");

                //Add new application
                var application = new RecruimentApply
                {
                    CoverLetter = request.CoverLetter,
                    Cv = request.Cv,
                    RegistrationDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Status = "Processing",
                    RecruimentInformationId = request.RecruimentInformationId,
                    StudentId = student_apply.Id,
                    RecruimentInformation = recruitment_information,
                    Student = student_apply,
                };
                _context.RecruimentApplies.Add(application);

                //Update student
                student_apply.CanSendApplication = false;
                _context.Students.Update(student_apply);

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
