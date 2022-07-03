using Application.Student.Commands.CreateStudentInfo;
using Application.Student.Commands.DeleteStudentInfo;
using Application.Student.Commands.UpdateStudentInfo;
using Application.Student.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [Authorize]
    public class StudentController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateStudent(CreateStudentInfoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("All")]
        public async Task<ActionResult> GetAllStudent()
        {
            return Ok(await Mediator.Send(new GetAllStudentQuery()));
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateStudentInfoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteStudentInfoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
