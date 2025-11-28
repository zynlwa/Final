using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedicalServiceId",
                table: "BasketItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "BasketItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AvailabilityId",
                table: "BasketItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AvailabilityId",
                table: "BasketItems",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_DoctorId",
                table: "BasketItems",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_MedicalServiceId",
                table: "BasketItems",
                column: "MedicalServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Availabilities_AvailabilityId",
                table: "BasketItems",
                column: "AvailabilityId",
                principalTable: "Availabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Doctors_DoctorId",
                table: "BasketItems",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_MedicalServices_MedicalServiceId",
                table: "BasketItems",
                column: "MedicalServiceId",
                principalTable: "MedicalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Availabilities_AvailabilityId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Doctors_DoctorId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_MedicalServices_MedicalServiceId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AvailabilityId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_DoctorId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_MedicalServiceId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<string>(
                name: "MedicalServiceId",
                table: "BasketItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "BasketItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AvailabilityId",
                table: "BasketItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
