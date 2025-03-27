using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace EFCore_Library.Scripts
{
    public static class MigrationBuilderSqlResource
    {
        public static OperationBuilder<SqlOperation> SqlResource(this MigrationBuilder mb, string relativeFileName)
        {
            // Open the embedded resource stream using the relative file name
            using (var stream = Assembly.GetAssembly(typeof(MigrationBuilderSqlResource)).GetManifestResourceStream(relativeFileName))
            {
                // Use a memory stream to copy the data from the resource stream
                using (var ms = new MemoryStream())
                {
                    // Copy the stream data to the memory stream
                    stream.CopyTo(ms);

                    // Convert the memory stream's data into a byte array
                    var data = ms.ToArray();

                    // Decode the byte array into a string, ignoring the BOM (Byte Order Mark)
                    var text = Encoding.UTF8.GetString(data, 0, data.Length);

                    // Use the SQL script text in the MigrationBuilder
                    return mb.Sql(text);
                }
            }
        }
    }
}
