using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend_AgendaProApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "IdRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Usuario" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "IdRol",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rol",
                keyColumn: "IdRol",
                keyValue: 2);
        }
    }
}
