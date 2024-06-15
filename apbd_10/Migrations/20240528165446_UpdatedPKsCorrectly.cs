using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task10.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPKsCorrectly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_IdPrescription",
                table: "PrescriptionMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_IdMedicament",
                table: "PrescriptionMedicament",
                column: "IdMedicament",
                principalTable: "Medicament",
                principalColumn: "IdMedicament",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_IdMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_IdPrescription",
                table: "PrescriptionMedicament",
                column: "IdPrescription",
                principalTable: "Medicament",
                principalColumn: "IdMedicament",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
