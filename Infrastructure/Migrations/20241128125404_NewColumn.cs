using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInstallments_Installments_InstallmentId",
                table: "PaymentInstallments");

            migrationBuilder.DropIndex(
                name: "IX_PaymentInstallments_InstallmentId",
                table: "PaymentInstallments");

            migrationBuilder.RenameColumn(
                name: "InstallmentId",
                table: "PaymentInstallments",
                newName: "NumberOfInstallmentsToPay");

            migrationBuilder.RenameColumn(
                name: "InstallmentAmount",
                table: "PaymentInstallments",
                newName: "InstallmentTotal");

            migrationBuilder.AddColumn<int>(
                name: "PaymentInstallmentId",
                table: "Installments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installments_PaymentInstallmentId",
                table: "Installments",
                column: "PaymentInstallmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_PaymentInstallments_PaymentInstallmentId",
                table: "Installments",
                column: "PaymentInstallmentId",
                principalTable: "PaymentInstallments",
                principalColumn: "PaymentInstallmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_PaymentInstallments_PaymentInstallmentId",
                table: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_Installments_PaymentInstallmentId",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "PaymentInstallmentId",
                table: "Installments");

            migrationBuilder.RenameColumn(
                name: "NumberOfInstallmentsToPay",
                table: "PaymentInstallments",
                newName: "InstallmentId");

            migrationBuilder.RenameColumn(
                name: "InstallmentTotal",
                table: "PaymentInstallments",
                newName: "InstallmentAmount");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInstallments_InstallmentId",
                table: "PaymentInstallments",
                column: "InstallmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInstallments_Installments_InstallmentId",
                table: "PaymentInstallments",
                column: "InstallmentId",
                principalTable: "Installments",
                principalColumn: "InstallmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
