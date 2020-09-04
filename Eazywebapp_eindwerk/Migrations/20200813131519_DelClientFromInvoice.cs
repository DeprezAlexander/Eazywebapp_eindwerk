using Microsoft.EntityFrameworkCore.Migrations;

namespace Eazywebapp_eindwerk.Migrations
{
    public partial class DelClientFromInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Client_ClientID",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Project_ProjectID",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Client_ClientID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ClientID",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Invoice");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Project",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Invoice",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Project_ProjectID",
                table: "Invoice",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Client_ClientID",
                table: "Project",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Project_ProjectID",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Client_ClientID",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Project",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Invoice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ClientID",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ClientID",
                table: "Invoice",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Client_ClientID",
                table: "Invoice",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Project_ProjectID",
                table: "Invoice",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Client_ClientID",
                table: "Project",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
