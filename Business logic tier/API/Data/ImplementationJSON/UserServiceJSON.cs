using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using API.Models;

namespace API.Data
{
    public class UserServiceJSON : IUserService
    {
        private string UsersFile = "users.json";
        private IList<User> AllUsers;

        public UserServiceJSON()
        {
            if (File.Exists(UsersFile))
            {
                string usersInJSON = File.ReadAllText(UsersFile);
                AllUsers = JsonSerializer.Deserialize<IList<User>>(usersInJSON);
            }
            else
            {
                Seed();
                Save();
            }
        }

        private void Seed()
        {
            User[] users =
            {
                new User
                {
                    Password = "1234",
                    UserId = 0,
                    Email = "dd"
                }
            };
            AllUsers = users.ToList();
        }

        private void Save()
        {
            string usersInJson = JsonSerializer.Serialize(AllUsers);
            File.WriteAllText(UsersFile, usersInJson);
        }

        public User ValidateUser(string email, string password)
        {
            var user = AllUsers.First(u => u.Email.Equals(email));
            if (user == null) throw new Exception("User do not exist");
            if (!user.Password.Equals(password)) throw new Exception("Password incorrect");
            return user;
        }

        public User RegisterUser(User user)
        {
            User? first = null;
            try
            {
                first = AllUsers.First(u => u.Email.Equals(user.Email));
            }
            catch (Exception e)
            {
            }

            if (first != null)
            {
                throw new Exception("This Email is already registered");
            }

            int max = AllUsers.Max(u => u.UserId);
            user.UserId = (++max);
            AllUsers.Add(user);
            Save();
            return user;
        }


        public User UpdateUser(User user, string password)
        {
            User toUpdate = AllUsers.First(u => u.UserId == user.UserId);
            if (toUpdate == null) throw new Exception("User does not exist");
            if (!toUpdate.Password.Equals(password)) throw new Exception("Password incorrect");
            toUpdate.Password = user.Password;
            toUpdate.FirstName = user.FirstName;
            toUpdate.LastName = user.LastName;
            toUpdate.Address = user.Address;
            toUpdate.PostalCode = user.PostalCode;
            toUpdate.Phone = user.Phone;
            Save();
            return toUpdate;
        }

        public User RemoveUser(User user)
        {
            User toRemove = AllUsers.First(u => u.Email == user.Email);
            if (toRemove == null) throw new Exception("User does not exist");
            if (!toRemove.Password.Equals(user.Password)) throw new Exception("Password incorrect");
            AllUsers.Remove(toRemove);
            Save();
            return toRemove;
        }
        
    }
}