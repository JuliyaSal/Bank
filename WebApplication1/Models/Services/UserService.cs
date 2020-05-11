using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Services
{
    public class UserService
    {
        private string connectionString = "server=localhost;database=Service;user id=postgres;password=456rtyrty;Pooling=false";
        public User GetUser(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>(@"SELECT id, email, name, status, password 
                                        FROM users
                                        WHERE id = @id;", new { id }).FirstOrDefault();
            }
        }

        public void CreateUser(User user)
        {                    
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = @"INSERT INTO users 
                             (id, email, name, status, password) 
                             VALUES(@id, @email, @name, @status, @password);";
                db.Execute(sqlQuery, user);

                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }

        public void UpdateUser(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = @"UPDATE users 
                             SET name = @name, email = @email, 
                             status = @status, password = @password
                             WHERE id = @id;";
                db.Execute(sqlQuery, user);
            }
        }

        public void DeleteUser(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM users WHERE id = @id;";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
