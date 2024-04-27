using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SSTMS.Model;
using System.Text;

namespace SSTMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationController : ControllerBase
    {
       

        private readonly IConfiguration _configuration;

        public OperationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("genertaeApi")]
        public IActionResult InsertApi(tableCreation requestModel)
        {
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("INSERT INTO ");
                queryBuilder.Append(requestModel.TableName); // Assuming TableName is the key for the table name
                queryBuilder.Append(" (");

                // Append column names and data types
                foreach (var column in requestModel.Columns)
                {
                    queryBuilder.Append($"{column.Key} {column.Value.DataType}, "); // Include data type
                }
                queryBuilder.Remove(queryBuilder.Length - 2, 2); // Remove the last comma and space

                queryBuilder.Append(") VALUES (");

                // Append parameter placeholders for values
                foreach (var column in requestModel.Columns)
                {
                    queryBuilder.Append($"@{column.Key}, "); // Assuming parameter names correspond to column names
                }
                queryBuilder.Remove(queryBuilder.Length - 2, 2); // Remove the last comma and space

                queryBuilder.Append(");");

                // Return the generated insert query
                return Ok(queryBuilder.ToString());
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("createTable")]
        public async Task<IActionResult> CreateTable(tableCreation requestModel)
        {
            try
            {
                // Validate requestModel and ensure it is not null
                if (requestModel == null || string.IsNullOrWhiteSpace(requestModel.TableName) || requestModel.Columns == null || requestModel.Columns.Count == 0)
                {
                    return BadRequest("Invalid request model.");
                }

                // Construct the CREATE TABLE query
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"CREATE TABLE {requestModel.TableName} (");

                // Append primary key column (standard convention: TableName + "ID")
                queryBuilder.Append($"{requestModel.TableName}ID INT PRIMARY KEY IDENTITY(1,1), ");

                // Append other columns with data types and lengths
                foreach (var column in requestModel.Columns)
                {
                    queryBuilder.Append($"{column.Key} {column.Value.DataType}");

                    // Append length if applicable
                    if (!string.IsNullOrWhiteSpace(column.Value.MaxLength))
                    {
                        if (column.Value.MaxLength.Equals("max", StringComparison.OrdinalIgnoreCase))
                        {
                            queryBuilder.Append("(max)");
                        }
                        else
                        {
                            queryBuilder.Append($"({column.Value.MaxLength})");
                        }
                    }

                    queryBuilder.Append(", ");
                }
                queryBuilder.Remove(queryBuilder.Length - 2, 2); // Remove the last comma and space
                queryBuilder.Append(");");

                // Execute the SQL command to create the table
                bool flag = await executeSQL(queryBuilder.ToString());
                if (flag)
                {
                    var model = new JsonResult(new
                    {
                        TableName = requestModel.TableName,
                        Columns = requestModel.Columns.Select(c => new
                        {
                            Key = c.Key,
                            Value = "" // Blank field for data insertion
                        }).ToList()
                    });
                    return Ok(model);
                }
                else
                {
                    return NotFound("table creation failed");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private async Task<bool> executeSQL(string query)
        {
            try
            {
                var abc = _configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(abc))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
