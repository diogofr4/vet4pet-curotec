using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessageAndLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Appointments_AppointmentId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AppointmentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Messages");

            // Seed Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "Dr. Vet", "vet@example.com", "password", 1 },
                    { 2, "John Doe", "john@example.com", "password", 2 }
                });

            // Seed Animals
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Name", "Species", "Breed", "Age", "OwnerId", "VetId" },
                values: new object[,]
                {
                    { 1, "Rex", "Dog", "German Shepherd", 3, 2, 1 }
                });

            // Seed Appointments
            for (int i = 1; i <= 20; i++)
            {
                migrationBuilder.InsertData(
                    table: "Appointments",
                    columns: new[] { "AnimalId", "VetId", "OwnerId", "Date", "Description" },
                    values: new object[] { 1, 1, 2, DateTime.Now.AddDays(i), $"Consulta número {i}" });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove Appointments
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AnimalId",
                keyValue: 1);

            // Remove Animals
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1);

            // Remove Users
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AppointmentId",
                table: "Messages",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Appointments_AppointmentId",
                table: "Messages",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
