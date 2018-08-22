using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using LoginPlus.Models;
using Microsoft.Extensions.Options;
using Login.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginPlus.Factory
{
    public class UserFactory
    {
        private MySqlOptions _options;
        public UserFactory(IOptions<MySqlOptions> config)
        {
            _options = config.Value;
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(_options.ConnectionString);
            }
        }

        public void RegisterUser(User user)
        {
            using(IDbConnection dbConnection = Connection)
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string pw_hashed = hasher.HashPassword(user, user.password);
                var query = @"
                    INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) 
                    VALUES (@first_name, @last_name, @email, @pw_hashed, NOW(), NOW())
                ";
                dbConnection.Open();
                dbConnection.Execute(query, user);
            }
        }
        public User GetUserByEmail(string Email)
        {
            using(IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT email FROM users WHERE users.email = {Email}";
                dbConnection.Open();
                var user = dbConnection.Query<User>(query, Email).First();
                return user;
            }
        }
        public bool EmailIsUnique(string email)
        {
            using(IDbConnection dbConnection = Connection)
            {
                var query = "SELECT id FROM users WHERE email = @EMAIL";
                object param = new {EMAIL = email};
                IEnumerable<User> result = dbConnection.Query<User>(query, param);
                return result.Count() == 0;
            }
        }
        public User GetUserById(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT * FROM users WHERE id = @USERID";
                object myParam = new {USERID = id};
                return dbConnection.Query<User>(query).First();
            }
        }
    }
}