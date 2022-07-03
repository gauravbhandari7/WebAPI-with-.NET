using DbDataReaderMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Repositories
{
    public class StudentCrudService : ICrudRepository<StudentInfo>
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration _configuration { get; }
        public StudentCrudService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<int> Delete(int id, CancellationToken cancellationToken)
        {
            var studentInfo = await _context.StudentInfo.FindAsync(id);
            _context.StudentInfo.Remove(studentInfo);
            await _context.SaveChangesAsync(cancellationToken);
            return id;
        }

        public async Task<IEnumerable<StudentInfo>> GetAll()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("usp_GetAllStudent", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            conn.Open();
            var reader = await cmd.ExecuteReaderAsync();
            List<StudentInfo> studentInfos = new List<StudentInfo>();
            while (await reader.ReadAsync())
            {
                var studentObj = reader.MapToObject<StudentInfo>();
                studentInfos.Add(studentObj);
            }
            return studentInfos;
        }

        public async Task<int> InsertEntity(StudentInfo entity, CancellationToken cancellationToken)
        {
            await _context.StudentInfo.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<int> UpdateEntity(StudentInfo entity, CancellationToken cancellationToken)
        {
            _context.StudentInfo.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
