using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddedGetAllTeacherSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"CREATE PROC usp_GetAllTeacher AS begin SELECT * FROM TeacherInfo end";
            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = "DROP PROC usp_GetAllTeacher";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
