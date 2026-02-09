using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "MedicalServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MedicalServiceId",
                table: "Availabilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvailabilityId1",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorBreaks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRecurringWeekly = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorBreaks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorBreaks_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorUnavailabilities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorUnavailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorUnavailabilities_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorWorkSchedules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorWorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorWorkSchedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_MedicalServiceId",
                table: "Availabilities",
                column: "MedicalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AvailabilityId1",
                table: "Appointments",
                column: "AvailabilityId1");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorBreaks_DoctorId",
                table: "DoctorBreaks",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorUnavailabilities_DoctorId",
                table: "DoctorUnavailabilities",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorWorkSchedules_DoctorId",
                table: "DoctorWorkSchedules",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Availabilities_AvailabilityId1",
                table: "Appointments",
                column: "AvailabilityId1",
                principalTable: "Availabilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_MedicalServices_MedicalServiceId",
                table: "Availabilities",
                column: "MedicalServiceId",
                principalTable: "MedicalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Availabilities_AvailabilityId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_MedicalServices_MedicalServiceId",
                table: "Availabilities");

            migrationBuilder.DropTable(
                name: "DoctorBreaks");

            migrationBuilder.DropTable(
                name: "DoctorUnavailabilities");

            migrationBuilder.DropTable(
                name: "DoctorWorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_MedicalServiceId",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AvailabilityId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "MedicalServices");

            migrationBuilder.DropColumn(
                name: "MedicalServiceId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "AvailabilityId1",
                table: "Appointments");
        }
    }
}
