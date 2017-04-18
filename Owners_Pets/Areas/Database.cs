using System;
using System.Collections.Generic;
using System.Linq;
using Owners_Pets.Models;
using System.Data.SQLite;

namespace Owners_Pets.Areas
{
    public class Database
    {
        public static string connectionString = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\DBTestTask.sqlite";

        // Table("Users")
        public IEnumerable<User> GetAllUsers()
        {
            List<User> userList = new List<User>();

            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Users ORDER BY id";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        User user = new User();

                        user.Id = (int)dataReader["Id"];
                        user.Name = (string)dataReader["Name"];
                        user.PetsCount = (int)dataReader["PetsCount"];

                        userList.Add(user);
                    }
                }
                
                foreach (var user in userList)
                {
                    user.PetsCount = GetUserInfoByUserId(user.Id).Count();

                    // Update PetsCount for all users
                    EditUser(user.Id, user);
                }
                
                connection.Close();
            }
            
            return userList;
        }

        public User GetUserById(int userId)
        {
            User user = new User();

            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Users WHERE Users.id = " + userId;
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.Id = (int)dataReader["Id"];
                        user.Name = (string)dataReader["Name"];
                        user.PetsCount = (int)dataReader["PetsCount"];
                    }
                }

                connection.Close();
            }

            return user;
        }

        public void AddUser(User user)
        {
            List<User> userList = new List<User>();
            userList = GetAllUsers().ToList();

            // For unique id
            foreach (var existingUser in userList)
            {
                if (user.Id == existingUser.Id)
                {
                    user.Id++;
                }
            }

            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                
                command.CommandText = "INSERT INTO Users (Id, Name, PetsCount) VALUES (" + user.Id + ", " + "'" + user.Name + "'" + ", " + user.PetsCount + ")";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditUser(int userId, User newUser)
        {
            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "UPDATE Users SET Id = " + newUser.Id + ", Name = " + "'" + newUser.Name + "'" + ", PetsCount = " + newUser.PetsCount + " WHERE Users.Id = " + userId;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteUser(int userId)
        {
            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "DELETE FROM Users WHERE Users.Id =" + userId;
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM UserInfo WHERE UserInfo.UserId =" + userId;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        // Table("UserInfo")
        public IEnumerable<UserInfo> GetAllUserInfo()
        {
            List<UserInfo> userInfoList = new List<UserInfo>();

            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM UserInfo ORDER BY id";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        UserInfo userInfo = new UserInfo();

                        userInfo.Id = (int)dataReader["Id"];
                        userInfo.UserId = (int)dataReader["UserId"];
                        userInfo.PetName = (string)dataReader["PetName"];

                        userInfoList.Add(userInfo);
                    }
                }

                connection.Close();
            }

            return userInfoList;
        }
                
        public IEnumerable<UserInfo> GetUserInfoByUserId(int userId)
        {
            List<UserInfo> userInfoList = new List<UserInfo>();

            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM UserInfo WHERE UserInfo.userId = " + userId;
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        UserInfo userInfo = new UserInfo();

                        userInfo.Id = (int)dataReader["Id"];
                        userInfo.UserId = (int)dataReader["UserId"];
                        userInfo.PetName = (string)dataReader["PetName"];

                        userInfoList.Add(userInfo);
                    }
                }

                connection.Close();
            }

            return userInfoList;
        }

        public void AddUserInfo(UserInfo userInfo)
        {
            List<UserInfo> userInfoList = new List<UserInfo>();
            userInfoList = GetAllUserInfo().ToList();

            // For unique id
            foreach (var existingUser in userInfoList)
            {
                if (userInfo.Id == existingUser.Id)
                {
                    userInfo.Id++;
                }
            }

            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                
                command.CommandText = "INSERT INTO UserInfo (Id, UserId, PetName) VALUES (" + userInfo.Id + ", " + userInfo.UserId + ", " + "'" + userInfo.PetName + "'" + ")";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditUserInfo(int userInfoId, UserInfo newUserInfo)
        {
            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "UPDATE UserInfo SET Id = " + newUserInfo.Id + ", UserId = " + newUserInfo.UserId + ", PetName = " + "'" + newUserInfo.PetName + "'" + " WHERE UserInfo.Id = " + userInfoId;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteUserInfo(int userInfoId)
        {
            using (var connection = new SQLiteConnection("Data Source=" + connectionString + ";Version=3"))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "DELETE FROM UserInfo WHERE UserInfo.Id =" + userInfoId;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}