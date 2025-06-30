using ProjectName.Application.Common.Security;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Seeds;
public static class DataSeed
{
    public static void SeedData(MigrationBuilder migrationBuilder)
    {
        SeedRoleData(migrationBuilder);
        SeedUserData(migrationBuilder);
    }

    public static void SeedRoleData(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
                       table: "Roles",
                                  columns: new[] { "Id", "Name", "Description", "IsDeleted", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                                             values: new object[] { 1, "SuperAdmin", "Super Admin", false, new DateTime(2024, 7, 6), null, new DateTime(2024, 7, 6), null });

        migrationBuilder.InsertData(
                       table: "Roles",
                                  columns: new[] { "Id", "Name", "Description", "IsDeleted", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                                             values: new object[] { 2, "Admin", "Admin", false, new DateTime(2024, 7, 6), null, new DateTime(2024, 7, 6), null });

        migrationBuilder.InsertData(
                       table: "Roles",
                                  columns: new[] { "Id", "Name", "Description", "IsDeleted", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                                             values: new object[] { 3, "User", "User", false, new DateTime(2024, 7, 6), null, new DateTime(2024, 7, 6), null });
    }

    public static void SeedUserData(MigrationBuilder migrationBuilder)
    {


        migrationBuilder.InsertData(
            table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Phone1", "Phone2", "IsSuperUser", "IsDeleted", "UserRoleId", "TenantId", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "Super", "Admin", null, null, true, false, 1, null, new DateTime(2024, 7, 6), null, new DateTime(2024, 7, 6), null });

        PasswordHasher passwordHasher = new PasswordHasher(Options.Create(new HashingOptions { Iterations = 10000 }));
        migrationBuilder.InsertData(
            table: "Authentications",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordExpiryDate", "LastLoginTime", "WrongRetriesRemaining", "isLocked", "IsActive", "EmailConfirmed", "UserID", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "superadmin@keensap.com", passwordHasher.Hash("click123"), new DateTime(2024, 7, 6), new DateTime(2024, 7, 6), 5, false, true, true, 1, new DateTime(2024, 7, 6), null, new DateTime(2024, 7, 6), null });

    }
}
