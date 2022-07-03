using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Student.Queries
{
    public class GetAllStudentQuery : IRequest<Response<IEnumerable<StudentInfo>>>
    {

    }
    public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, Response<IEnumerable<StudentInfo>>>
    {
        private readonly ICrudRepository<StudentInfo> _crudStudentService;

        public GetAllStudentQueryHandler(ICrudRepository<StudentInfo> crudStudentService)
        {
            _crudStudentService = crudStudentService;
        }
        public async Task<Response<IEnumerable<StudentInfo>>> Handle(GetAllStudentQuery query, CancellationToken cancellationToken)
        {
            return new Response<IEnumerable<StudentInfo>> (await _crudStudentService.GetAll(),"Data Fetched Successfully");
        }
     }
}
