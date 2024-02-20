using Lesson_46_CRUD_controller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Globalization;

namespace Lesson_46_CRUD_controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADONetController : ControllerBase
    {
        private readonly string CONNECTION_STRING = "Host=localhost;Port=5432;Database=HTTP;User Id=postgres;Password=abdullayev;";

        [HttpGet]
        public List<Products> Get()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = "select * from products";
                connection.Open();
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
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
        public string Post(string name, string description, string PhotoPath)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"insert into products(name,description,photopath) values(@name,@description,@photopath)";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("description", description);
                command.Parameters.AddWithValue("PhotoPath", PhotoPath);
                int status = command.ExecuteNonQuery();

                return $"Post status [=> {status}";
            }
        }
        [HttpPut]

        public string Put(int id,string name,string description,string photopath)
        {
            using(NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"update products set name = @name,description = @description,photopath = @photopath where id = @id";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query , connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("description", description);
                command.Parameters.AddWithValue("photopath", photopath);
                int status = command.ExecuteNonQuery();
                return $"Put status [->{status}";
            }
        }
        [HttpPatch]

        public string Patch(int id,string name)
        {
            using (NpgsqlConnection connection= new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"update products set name =@name where id = @id";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand( query , connection);

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue ("name", name);
                int status = command.ExecuteNonQuery();
                return $"Patch status [=>{status}";
            }
        }
        [HttpDelete]
        public string Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"delete from products where id =@id";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("id", id);
                int status = command.ExecuteNonQuery();
                return $"Delete status [=>{status}";
            }
        }
    }
}
