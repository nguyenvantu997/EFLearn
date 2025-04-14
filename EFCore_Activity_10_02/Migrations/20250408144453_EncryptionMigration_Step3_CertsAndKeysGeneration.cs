using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreActivity1002.Migrations
{
    /// <inheritdoc />
    public partial class EncryptionMigrationStep3CertsAndKeysGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE symmetric_key_id = 101)
                BEGIN
                    CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Password#123'
                END");

            migrationBuilder.Sql(@"
                CREATE CERTIFICATE AW_tdeCert
                WITH SUBJECT = 'AdventureWorks TDE Certificate'");

            migrationBuilder.Sql(@"
                BACKUP CERTIFICATE AW_tdeCert 
                TO FILE = '/var/opt/mssql/keys/AW_tdeCert.crt'
                WITH PRIVATE KEY (
                    FILE = '/var/opt/mssql/keys/AW_tdeCert_PrivateKey.crt',
                    ENCRYPTION BY PASSWORD = 'Password#123'
                )");

            migrationBuilder.Sql(@"
                CREATE SYMMETRIC KEY AW_ColumnKey
                WITH ALGORITHM = AES_256
                ENCRYPTION BY CERTIFICATE AW_tdeCert;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
