using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddedGetAllStudentSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"CREATE PROC usp_GetAllStudent AS begin SELECT * FROM StudentInfo end";
            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = "DROP PROC usp_GetAllStudent";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
