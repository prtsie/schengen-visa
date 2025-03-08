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
                name: "PlaceOfWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Address_Country = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false),
                    Address_City = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false),
                    Address_Street = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Address_Building = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    PhoneNum = table.Column<string>(type: "character varying(13)", unicode: false, maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOfWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", unicode: false, maxLength: 254, nullable: false),
                    Password = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_FirstName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Name_Surname = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Name_Patronymic = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Passport_Number = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Passport_Issuer = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Passport_IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Passport_ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CountryOfBirth = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false),
                    CityOfBirth = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false),
                    Citizenship = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    CitizenshipByBirth = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    MaritalStatus = table.Column<int>(type: "integer", nullable: false),
                    FatherName_FirstName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    FatherName_Surname = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    FatherName_Patronymic = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    MotherName_FirstName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    MotherName_Surname = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    MotherName_Patronymic = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    JobTitle = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    PlaceOfWorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsNonResident = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicant_PlaceOfWork_PlaceOfWorkId",
                        column: x => x.PlaceOfWorkId,
                        principalTable: "PlaceOfWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applicant_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisaApplication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ReentryPermit_Number = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: true),
                    ReentryPermit_ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DestinationCountry = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false),
                    PermissionToDestCountry_ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PermissionToDestCountry_Issuer = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    VisaCategory = table.Column<int>(type: "integer", nullable: false),
                    ForGroup = table.Column<bool>(type: "boolean", nullable: false),
                    RequestedNumberOfEntries = table.Column<int>(type: "integer", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidDaysRequested = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisaApplication_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PastVisa",
                columns: table => new
                {
                    VisaApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastVisa", x => new { x.VisaApplicationId, x.Id });
                    table.ForeignKey(
                        name: "FK_PastVisa_VisaApplication_VisaApplicationId",
                        column: x => x.VisaApplicationId,
                        principalTable: "VisaApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PastVisit",
                columns: table => new
                {
                    VisaApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DestinationCountry = table.Column<string>(type: "character varying(70)", unicode: false, maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastVisit", x => new { x.VisaApplicationId, x.Id });
                    table.ForeignKey(
                        name: "FK_PastVisit_VisaApplication_VisaApplicationId",
                        column: x => x.VisaApplicationId,
                        principalTable: "VisaApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_PlaceOfWorkId",
                table: "Applicant",
                column: "PlaceOfWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_UserId",
                table: "Applicant",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisaApplication_ApplicantId",
                table: "VisaApplication",
                column: "ApplicantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PastVisa");

            migrationBuilder.DropTable(
                name: "PastVisit");

            migrationBuilder.DropTable(
                name: "VisaApplication");

            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "PlaceOfWork");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
