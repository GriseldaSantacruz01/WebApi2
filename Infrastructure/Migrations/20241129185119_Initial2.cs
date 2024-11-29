using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInstallments_ApprovedLoans_PaymentInstallmentId",
                table: "PaymentInstallments");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentInstallmentId",
                table: "PaymentInstallments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInstallments_ApprovedLoanId",
                table: "PaymentInstallments",
                column: "ApprovedLoanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInstallments_ApprovedLoans_ApprovedLoanId",
                table: "PaymentInstallments",
                column: "ApprovedLoanId",
                principalTable: "ApprovedLoans",
                principalColumn: "ApprovedLoanId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInstallments_ApprovedLoans_ApprovedLoanId",
                table: "PaymentInstallments");

            migrationBuilder.DropIndex(
                name: "IX_PaymentInstallments_ApprovedLoanId",
                table: "PaymentInstallments");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentInstallmentId",
                table: "PaymentInstallments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInstallments_ApprovedLoans_PaymentInstallmentId",
                table: "PaymentInstallments",
                column: "PaymentInstallmentId",
                principalTable: "ApprovedLoans",
                principalColumn: "ApprovedLoanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
