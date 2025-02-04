using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ContactProject.Models;
using ContactProject.Repositories.Interface;
using Npgsql;

namespace ContactProject.Repositories.Implementation
{
    public class UserRepository : IUserInterface
    {
        private readonly NpgsqlConnection _conn;

        public UserRepository(NpgsqlConnection conn)
        {
            _conn = conn;
        }

        public async Task<int> Delete(string userid)
        {
            throw new NotImplementedException();

        }

        public Task<List<t_User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<t_User?> GetOne(string userid)
        {
            throw new NotImplementedException();
        }

        public async Task<t_User> Login(vm_Login user)
        {
            t_User UserData = new t_User();
            var qry = "SELECT * FROM t_user WHERE c_email=@c_email AND c_password=@c_password";
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(qry, _conn))
                {
                    cmd.Parameters.AddWithValue("@c_email", user.c_Email);
                    cmd.Parameters.AddWithValue("@c_password", user.c_Password);
                    await _conn.OpenAsync();
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        UserData.c_UserId = (int)reader["c_userid"];
                        UserData.c_UserName = (string)reader["c_username"];
                        UserData.c_Email = (string)reader["c_email"];
                        UserData.c_Gender = (string)reader["c_gender"];
                        UserData.c_Mobile = (string)reader["c_mobile"];
                        UserData.c_Address = (string)reader["c_address"];
                        UserData.c_Image = (string)reader["c_image"];
                    }

                }
            }
            catch(Exception ex){
                Console.WriteLine("----------> Login Error: "+ex.Message);
            }
            finally{
                await _conn.CloseAsync();
            }
            return UserData;
        }

        // public async Task<int> Register(t_User user)
        // {
        //     try
        //     {
        //         NpgsqlCommand cm = new NpgsqlCommand(@"INSERT INTO t_user 
        //         (c_username,c_email,c_password,c_address,c_mobile,c_gender,c_image) VALUES (@c_username,@c_email,@c_password,@c_address,@c_mobile,@c_gender,@c_image)
        //         ", _conn);

        //         cm.Parameters.AddWithValue("@c_username", user.c_UserName);
        //         cm.Parameters.AddWithValue("@c_email", user.c_Email);
        //         cm.Parameters.AddWithValue("@c_password", user.c_Password);
        //         cm.Parameters.AddWithValue("@c_address", user.c_Address ?? (object)DBNull.Value);
        //         cm.Parameters.AddWithValue("@c_mobile", user.c_Mobile ?? (object)DBNull.Value);
        //         cm.Parameters.AddWithValue("@c_gender", user.c_Gender ?? (object)DBNull.Value);
        //         cm.Parameters.AddWithValue("@c_image", user.c_Image ?? (object)DBNull.Value);

        //         _conn.Open();
        //         cm.ExecuteNonQuery();
        //         _conn.Close();
        //         return 1;
        //     }
        //     catch (Exception ex)
        //     {
        //         return 0;
        //     }
        // }



        public async Task<int> Register(t_User data)
        {
            // Taking status variable to get different status of registration

            int status = 0;
            try
            {
                await _conn.CloseAsync();

                // Check if the user already exists
                NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT * FROM t_User WHERE c_email = @c_email;", _conn);
                comcheck.Parameters.AddWithValue("@c_email", data.c_Email);

                await _conn.OpenAsync();
                using (NpgsqlDataReader datadr = await comcheck.ExecuteReaderAsync())
                {
                    if (datadr.HasRows)
                    {
                        await _conn.CloseAsync();
                        return 0; // Returning 0 if user exists
                    }
                }
                await _conn.CloseAsync();

                // Query to add data in the database
                NpgsqlCommand com = new NpgsqlCommand(@"
            INSERT INTO t_User (c_username, c_email, c_password, c_address, c_gender, c_mobile, c_image) 
            VALUES (@c_userName, @c_email, @c_password, @c_address, @c_gender, @c_mobile, @c_image)", _conn);

                com.Parameters.AddWithValue("@c_userName", data.c_UserName);
                com.Parameters.AddWithValue("@c_email", data.c_Email);
                com.Parameters.AddWithValue("@c_password", data.c_Password);
                com.Parameters.AddWithValue("@c_address", data.c_Address);
                com.Parameters.AddWithValue("@c_gender", data.c_Gender);
                com.Parameters.AddWithValue("@c_mobile", data.c_Mobile);
                com.Parameters.AddWithValue("@c_image", data.c_Image);

                await _conn.OpenAsync();
                await com.ExecuteNonQueryAsync();
                await _conn.CloseAsync();

                return 1; // Returning 1 if user has successfully registered
            }
            catch (Exception e)
            {
                await _conn.CloseAsync();
                Console.WriteLine("Register Failed, Error: " + e.Message);
                return -1; // Returning -1 if there is an error during registration
            }
        }


        public Task<int> Update(t_User user)
        {
            throw new NotImplementedException();
        }
    }
}