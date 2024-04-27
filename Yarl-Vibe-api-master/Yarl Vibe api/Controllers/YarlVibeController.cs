using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Yarl_Vibe_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YarlVibeController : ControllerBase
    {
        private IConfiguration _configuration;
        public YarlVibeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetData")]
        public JsonResult GetData() {
            string query = "select * from tbl_KitchenStaff";
            DataTable table = new DataTable();
            string sqlDatasource= _configuration.GetConnectionString("yarlVibeDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource)) {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);  
        }
        [HttpPost]
        [Route("AddData")]
        public JsonResult AddData([FromForm] string staffName, [FromForm] int kitchenId)
        {
            string query = "INSERT INTO tbl_KitchenStaff (StaffName, KitchenID) VALUES (@staffName, @kitchenId);";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("yarlVibeDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@staffName", staffName);
                    myCommand.Parameters.AddWithValue("@kitchenId",kitchenId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                    //chnageddd
                }
            }
            return new JsonResult("Added Successfully!");
        }

        [HttpDelete]
        [Route("DeleteData")]
        public JsonResult DeleteData(int id)
        {
            string query = "DELETE FROM tbl_KitchenStaff WHERE KitchenStaffID =@id;";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("yarlVibeDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id",id);
                    
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully!");
        }
    }
}
