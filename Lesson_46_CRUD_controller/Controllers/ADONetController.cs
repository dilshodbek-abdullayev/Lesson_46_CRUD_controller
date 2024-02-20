using Lesson_46_CRUD_controller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Lesson_46_CRUD_controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADONetController : ControllerBase
    {
        private readonly string CONNECTION_STRING;
        public ADONetController()
        {
            CONNECTION_STRING = "Host:=localhost;port=5432;Database=HTTP;User Id=postgres;Password=abdullayev;";
        }
        [HttpGet]
        public List<string> Get()
        {
            using(NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = "select * from products";
                connection.Open();
                NpgsqlCommand npgsqlCommand = connection.CreateCommand();
                var res = npgsqlCommand.ExecuteReader();
                List<Products> products = new List<Products>();
                while (res.Read())
                {
                    products.Add(new Products
                    {
                        Id = (int)res[0],
                        Name = (string)res[1],
                        Description = (string)res[2],
                        PhotoPath = (string)res[3]
                    });

                }
                return products;
            }
        }
        [HttpPost]
        public string Post(string name,string description,string PhotoPath) 
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"insert into products(Name,Description,Photopath)" +
                    $"values(@Name,@Description,@Photopath)";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query,connection);

            }
        }
    }
}
