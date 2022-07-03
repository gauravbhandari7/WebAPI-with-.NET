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

namespace Application.Student.Commands.UpdateStudentInfo
{
    public class UpdateStudentInfoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class UpdateStudentInfoCommandHandler : IRequestHandler<UpdateStudentInfoCommand, Response<int>>
    {
        private readonly ICrudRepository<StudentInfo> _crudStudentService;
        private readonly ApplicationDbContext _context;

       public UpdateStudentInfoCommandHandler(ICrudRepository<StudentInfo> crudStudentService, ApplicationDbContext context)
        {
            _crudStudentService = crudStudentService;
            _context = context;
        }

        public async Task<Response<int>> Handle(UpdateStudentInfoCommand request, CancellationToken cancellationToken)
        {
            var studentInfo = await _context.StudentInfo.FindAsync(request.Id);
            if(studentInfo == null)
            {
                throw new Exception("StudentInfo not found");
            }

            studentInfo.Name = request.Name;
            studentInfo.Address = request.Address;
            studentInfo.RollNo = request.RollNo;

            await _crudStudentService.UpdateEntity(studentInfo, cancellationToken);

            return new Response<int>(studentInfo.Id, "StudentInfo updated successfully");
        }
    }
}
