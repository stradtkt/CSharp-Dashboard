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
    }
}