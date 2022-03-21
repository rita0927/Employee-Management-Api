using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using employee_app.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace employee_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        // public JsonResult Get()
        public string Get()
        {
            string query = @"
                    select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            // return new JsonResult(table);

            // var json = Newtonsoft.Json.JsonConvert.SerializeObject(table);
            // return json;

            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }

        [HttpPost]
        public string Post(Department dep)
        {
            string query = @"
                            insert into dbo.Department values
                            ('" + dep.DepartmentName + @"')
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject("Added Successfully");
        }

        [HttpPut]
        public string Put(Department dep)
        {
            string query = @"
                            update dbo.Department set
                            DepartmentName = '" + dep.DepartmentName + @"'
                            where DepartmentId = " + dep.DepartmentId + @"
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string query = @"
                            delete from dbo.Department
                            where DepartmentId = " + id;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject("Deleted Successfully");
        }

    }
}
