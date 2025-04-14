using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreActivity1002.Migrations
{
    /// <inheritdoc />
    public partial class EncryptionMigrationStep6EncryptBackupDataIntoOriginalColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                OPEN SYMMETRIC KEY AW_ColumnKey 
                DECRYPTION BY CERTIFICATE AW_tdeCert;

                UPDATE [HumanResources].[Employee] 
                SET 
                    [NationalIDNumber] = EncryptByKey(Key_GUID('AW_ColumnKey'), CONVERT(varbinary(max), [NationalIDNumberBackup])),
                    [JobTitle] = EncryptByKey(Key_GUID('AW_ColumnKey'), CONVERT(varbinary(max), [JobTitleBackup])),
                    [BirthDate] = EncryptByKey(Key_GUID('AW_ColumnKey'), CONVERT(varbinary(max), [BirthDateBackup])),
                    [MaritalStatus] = EncryptByKey(Key_GUID('AW_ColumnKey'), CONVERT(varbinary(max), [MaritalStatusBackup])),
                    [Gender] = EncryptByKey(Key_GUID('AW_ColumnKey'), CONVERT(varbinary(max), [GenderBackup])),
                    [HireDate] = EncryptByKey(Key_GUID('AW_ColumnKey'), CONVERT(varbinary(max), [HireDateBackup]));

                CLOSE ALL SYMMETRIC KEYS;
                ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
