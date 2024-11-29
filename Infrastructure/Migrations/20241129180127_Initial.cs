using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "TermIRs",
                columns: table => new
                {
                    TermId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Months = table.Column<int>(type: "integer", nullable: false),
                    InterestRate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermIRs", x => x.TermId);
                    table.UniqueConstraint("AK_TermIRs_Months", x => x.Months);
                });

            migrationBuilder.CreateTable(
                name: "LoanRequests",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    RejectionReason = table.Column<string>(type: "text", nullable: true),
                    RequestStatus = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Months = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequests", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_LoanRequests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanRequests_TermIRs_Months",
                        column: x => x.Months,
                        principalTable: "TermIRs",
                        principalColumn: "Months",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovedLoans",
                columns: table => new
                {
                    ApprovedLoanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InterestRate = table.Column<float>(type: "real", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PendingAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Months = table.Column<int>(type: "integer", nullable: false),
                    LoanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedLoans", x => x.ApprovedLoanId);
                    table.ForeignKey(
                        name: "FK_ApprovedLoans_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovedLoans_LoanRequests_LoanId",
                        column: x => x.LoanId,
                        principalTable: "LoanRequests",
                        principalColumn: "LoanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInstallments",
                columns: table => new
                {
                    PaymentInstallmentId = table.Column<int>(type: "integer", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NextDueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumberOfInstallmentsToPay = table.Column<int>(type: "integer", nullable: false),
                    InstallmentTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    ApprovedLoanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInstallments", x => x.PaymentInstallmentId);
                    table.ForeignKey(
                        name: "FK_PaymentInstallments_ApprovedLoans_PaymentInstallmentId",
                        column: x => x.PaymentInstallmentId,
                        principalTable: "ApprovedLoans",
                        principalColumn: "ApprovedLoanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    InstallmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CapitalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    InstallmentTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    InterestAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    InstallmentStatus = table.Column<string>(type: "text", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentInstallmentId = table.Column<int>(type: "integer", nullable: true),
                    ApprovedLoanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.InstallmentId);
                    table.ForeignKey(
                        name: "FK_Installments_ApprovedLoans_ApprovedLoanId",
                        column: x => x.ApprovedLoanId,
                        principalTable: "ApprovedLoans",
                        principalColumn: "ApprovedLoanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Installments_PaymentInstallments_PaymentInstallmentId",
                        column: x => x.PaymentInstallmentId,
                        principalTable: "PaymentInstallments",
                        principalColumn: "PaymentInstallmentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedLoans_CustomerId",
                table: "ApprovedLoans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedLoans_LoanId",
                table: "ApprovedLoans",
                column: "LoanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installments_ApprovedLoanId",
                table: "Installments",
                column: "ApprovedLoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_PaymentInstallmentId",
                table: "Installments",
                column: "PaymentInstallmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequests_CustomerId",
                table: "LoanRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequests_Months",
                table: "LoanRequests",
                column: "Months");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "PaymentInstallments");

            migrationBuilder.DropTable(
                name: "ApprovedLoans");

            migrationBuilder.DropTable(
                name: "LoanRequests");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "TermIRs");
        }
    }
}
