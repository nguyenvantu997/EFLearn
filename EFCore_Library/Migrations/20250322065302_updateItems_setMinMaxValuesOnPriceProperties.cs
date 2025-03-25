using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class updateItemssetMinMaxValuesOnPriceProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Items SET PurchasePrice = 0 WHERE PurchasePrice < 0");
            migrationBuilder.Sql("UPDATE Items SET CurrentOrFinalPrice = 0 WHERE CurrentOrFinalPrice < 0");
            migrationBuilder.Sql("UPDATE Items SET PurchasePrice = 25000 WHERE PurchasePrice > 25000");
            migrationBuilder.Sql("UPDATE Items SET CurrentOrFinalPrice = 25000 WHERE CurrentOrFinalPrice > 25000");

            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_PurchasePrice_Minimum')
                                    BEGIN 
                                        ALTER TABLE [dbo].[Items] ADD CONSTRAINT CK_Items_PurchasePrice_Minimum CHECK (PurchasePrice >= 0) 
                                    END 

                                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_PurchasePrice_Maximum') 
                                    BEGIN 
                                        ALTER TABLE [dbo].[Items] ADD CONSTRAINT CK_Items_PurchasePrice_Maximum CHECK (PurchasePrice <= 25000)
                                    END
                                
                                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_CurrentOrFinalPrice_Minimum')
                                    BEGIN 
                                        ALTER TABLE [dbo].[Items] ADD CONSTRAINT CK_Items_CurrentOrFinalPrice_Minimum CHECK (CurrentOrFinalPrice >= 0) 
                                    END

                                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_CurrentOrFinalPrice_Maximum') 
                                    BEGIN 
                                        ALTER TABLE [dbo].[Items] ADD CONSTRAINT CK_Items_CurrentOrFinalPrice_Maximum CHECK (CurrentOrFinalPrice <= 25000)
                                    END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_PurchasePrice_Minimum') 
                                        BEGIN 
                                            ALTER TABLE [dbo].[Items] DROP CONSTRAINT CK_Items_PurchasePrice_Minimum 
                                        END");
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_PurchasePrice_Maximum') 
                                        BEGIN 
                                            ALTER TABLE [dbo].[Items] DROP CONSTRAINT CK_Items_PurchasePrice_Maximum 
                                        END");

            migrationBuilder.Sql(@"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_CurrentOrFinalPrice_Minimum') 
                                        BEGIN 
                                            ALTER TABLE [dbo].[Items] DROP CONSTRAINT CK_Items_CurrentOrFinalPrice_Minimum 
                                        END");
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='CK_Items_CurrentOrFinalPrice_Maximum') 
                                        BEGIN 
                                            ALTER TABLE [dbo].[Items] DROP CONSTRAINT CK_Items_CurrentOrFinalPrice_Maximum 
                                        END");
        }
    }
}
