using Microsoft.EntityFrameworkCore.Migrations;

namespace PandoraMVC.Data.Migrations
{
    public partial class IdToLowercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Workspaces",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Tasks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Epics",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "WorkspaceId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EpicId",
                table: "Tasks",
                column: "EpicId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_WorkspaceId",
                table: "Tasks",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Epics_WorkspaceId",
                table: "Epics",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkspaceId",
                table: "AspNetUsers",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Workspaces_WorkspaceId",
                table: "AspNetUsers",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Epics_Workspaces_WorkspaceId",
                table: "Epics",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Epics_EpicId",
                table: "Tasks",
                column: "EpicId",
                principalTable: "Epics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_WorkspaceId",
                table: "Tasks",
                column: "WorkspaceId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Workspaces_WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Epics_Workspaces_WorkspaceId",
                table: "Epics");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Epics_EpicId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_WorkspaceId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EpicId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_WorkspaceId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Epics_WorkspaceId",
                table: "Epics");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkspaceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workspaces",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tasks",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Epics",
                newName: "ID");
        }
    }
}
