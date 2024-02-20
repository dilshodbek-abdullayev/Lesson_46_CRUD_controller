using Dapper;
using Lesson_46_CRUD_controller.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Lesson_46_CRUD_controller.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DupperController : ControllerBase
    {
        private readonly string CONNECTION_STRING = "Host=localhost;Port=5432;Database=HTTP;User Id=postgres;Password=abdullayev;";
        [HttpGet]
        public IEnumerable<Products> GetProducts()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = "select * from products";
                return connection.Query<Products>(query);
            }
        }
        [HttpPost]
        public string Post(string name, string description,string photopath)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"insert into products(name,description,photopath)values(@name,@description,@photopath);";
                int status = connection.Execute(query, new
                {
                    name = name,
                    description = description,
                    photopath = photopath
                });
                return $"Post status [=> {status}";
            }
        }
        [HttpPut]
        public string Put(int id,Products products)
        {
            using (NpgsqlConnection connection =new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"update products set name = @name,description = @description,photopath = @photopath where id = @id";
                int status = connection.Execute(query, new
                {
                    id,
                    name = products.Name,
                    description = products.Description,
                    photopath = products.PhotoPath
                });
                return $"Update Status [=> {status}";
            }    
        }
        [HttpPatch]
        public string Patch(int id,string Name)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = $"update products set name = @name where id = @id";
                int status = connection.Execute(query, new
                {
                    id,
                    name = Name
                });
                return $"Patch status [=> {status}";
            }
        }

        [HttpDelete]
        public string Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = "delete from products where id = @id";

                int status = connection.Execute(query, new
                {
                    id
                });
                return $"Delete status [=> {status}";
            }
        }

    }
}
