using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Student.Commands.DeleteStudentInfo
{
    public class DeleteStudentInfoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteStudentInfoCommandHandler : IRequestHandler<DeleteStudentInfoCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<StudentInfo> _crudStudentService;

        public DeleteStudentInfoCommandHandler(ICrudRepository<StudentInfo> crudStudentService, ApplicationDbContext context)
        {
            _crudStudentService = crudStudentService;
            _context = context;
        }
        public async Task<Response<int>> Handle(DeleteStudentInfoCommand request, CancellationToken cancellationToken)
        {
            var studentInfo = await _context.StudentInfo.FindAsync(request.Id);
            
            if (studentInfo == null)
            {
                throw new Exception("StudentInfo not found");
            }

            await _crudStudentService.Delete(request.Id, cancellationToken);
            return new Response<int>(request.Id, "Student Info deleted successfully");
        }
    }
}
