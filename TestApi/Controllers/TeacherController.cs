using Application.DTO;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using Infrastructure.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudRepository<TeacherInfo> _teacherCrudService;
        public TeacherController(ApplicationDbContext context, ICrudRepository<TeacherInfo> teacherCrudService)
        {
            _context = context;
            _teacherCrudService = teacherCrudService;

        }

        [HttpPost]
        public async Task<ActionResult<Response<int>>> Create(TeacherInfoDTO teacherInfoDTO)
        {
            TeacherInfo teacherInfo = new TeacherInfo
            {
                Name = teacherInfoDTO.Name,
                Address = teacherInfoDTO.Address,
                Salary = teacherInfoDTO.Salary
            };

            CancellationToken cancellationToken = new CancellationToken();

            var entityId = await _teacherCrudService.InsertEntity(teacherInfo, cancellationToken);

            return new Response<int>(entityId, "TeacherInfo created successfully");
        }

        [HttpPut]
        public async Task<ActionResult<Response<int>>> Update(int id, TeacherInfoDTO teacherInfoDTO)
        {
            var teacherInfo = await _context.TeacherInfo.FindAsync(id);

            if (teacherInfo == null)
            {
                return NotFound();
            }

            teacherInfo.Name = teacherInfoDTO.Name;
            teacherInfo.Address = teacherInfoDTO.Address;
            teacherInfo.Salary = teacherInfoDTO.Salary;

            CancellationToken cancellationToken = new CancellationToken();

            await _teacherCrudService.UpdateEntity(teacherInfo, cancellationToken);
            return new Response<int>(id, "TeacherInfo updated successfully");
        }

        [HttpDelete]
        public async Task<ActionResult<Response<int>>> Delete(int id)
        {
            var teacherInfo = await _context.TeacherInfo.FindAsync(id);

            if (teacherInfo == null)
            {
                return NotFound();
            }

            CancellationToken cancellationToken = new CancellationToken();

            await _teacherCrudService.Delete(id, cancellationToken);

            return new Response<int>(id,"TeacherInfo deleted successfully");
        }

        [HttpGet("all")]
        public async Task<ActionResult<Response<IEnumerable<TeacherInfo>>>> GetAllTeacher()
        {
            return new Response<IEnumerable<TeacherInfo>>(await _teacherCrudService.GetAll(),"Data fetched successfully");
        }
    }
}
