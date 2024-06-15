using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task10.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament",
                columns: new[] { "IdMedicament", "IdPrescription" });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicament_IdPrescription",
                table: "PrescriptionMedicament",
                column: "IdPrescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionMedicament_IdPrescription",
                table: "PrescriptionMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament",
                column: "IdPrescription");
        }
    }
}
