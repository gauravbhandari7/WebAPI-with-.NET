using DbDataReaderMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Repositories
{
    public class TeacherCrudService : ICrudRepository<TeacherInfo>
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration _configuration { get; }

        public TeacherCrudService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<int> Delete(int id, CancellationToken cancellationToken)
        {
            var teacherInfo = await _context.TeacherInfo.FindAsync(id);
            _context.TeacherInfo.Remove(teacherInfo);
            await _context.SaveChangesAsync(cancellationToken);
            return id;
        }

        public async Task<IEnumerable<TeacherInfo>> GetAll()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("usp_GetAllTeacher", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            conn.Open();
            var reader = await cmd.ExecuteReaderAsync();
            List<TeacherInfo> teacherInfos = new List<TeacherInfo>();

            while (await reader.ReadAsync())
            {
                var teacherObj = reader.MapToObject<TeacherInfo>();
                teacherInfos.Add(teacherObj);
            }

            return teacherInfos;
        }

        public async Task<int> InsertEntity(TeacherInfo entity, CancellationToken cancellationToken)
        {
            await _context.TeacherInfo.AddAsync(entity);
            
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<int> UpdateEntity(TeacherInfo entity, CancellationToken cancellationToken)
        {
            _context.TeacherInfo.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
