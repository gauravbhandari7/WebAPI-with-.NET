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

namespace Application.Student.Commands.CreateStudentInfo
{
    public class CreateStudentInfoCommand : IRequest<Response<int>>
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreateStudentInfoCommandHandler : IRequestHandler<CreateStudentInfoCommand, Response<int>>
    {
        private readonly ICrudRepository<StudentInfo> _crudStudentService;

        public CreateStudentInfoCommandHandler(ICrudRepository<StudentInfo> crudStudentService)
        {
            _crudStudentService = crudStudentService;
        }
        public async Task<Response<int>> Handle(CreateStudentInfoCommand request, CancellationToken cancellationToken)
        {
            var entity = new StudentInfo
            {
                RollNo = request.RollNo,
                Name = request.Name,
                Address = request.Address
            };

            var entityId = await _crudStudentService.InsertEntity(entity, cancellationToken);

            return new Response<int>(entityId, "Student Info added successfully");
        }
    }
}
