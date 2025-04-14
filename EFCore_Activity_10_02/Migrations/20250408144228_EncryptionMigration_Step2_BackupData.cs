using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreActivity1002.Migrations
{
    /// <inheritdoc />
    public partial class EncryptionMigrationStep2BackupData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE [HumanResources].[Employee] SET [NationalIDNumberBackup] = [NationalIDNumber] ,
                        [JobTitleBackup] = [JobTitle] ,
                        [BirthDateBackup] = [BirthDate] ,
                        [MaritalStatusBackup] = [MaritalStatus] ,
                        [GenderBackup] = [Gender] ,
                        [HireDateBackup] = [HireDate]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
