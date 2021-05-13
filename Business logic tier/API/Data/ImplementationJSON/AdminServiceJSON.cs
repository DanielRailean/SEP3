using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using API.Models;

namespace API.Data
{
    public class AdminServiceJSON : IAdminService
    {
       private string AdministratorsFile = "administrators.json";
        private IList<Administrator> AllAdministrators;

        public AdminServiceJSON()
        {
            if (File.Exists(AdministratorsFile))
            {
                string administratorsInJSON = File.ReadAllText(AdministratorsFile);
                AllAdministrators = JsonSerializer.Deserialize<IList<Administrator>>(administratorsInJSON);
            }
            else
            {
                Seed();
                Save();
            }
        }

        private void Seed()
        {
            Administrator[] administrators =
            {
                new Administrator
                {
                    Password = "1234",
                    Email = "dd"
                }
            };
            AllAdministrators = administrators.ToList();
        }

        private void Save()
        {
            string administratorsInJson = JsonSerializer.Serialize(AllAdministrators);
            File.WriteAllText(AdministratorsFile, administratorsInJson);
        }

        public Administrator ValidateAdministrator(string email, string password)
        {
            var administrator = AllAdministrators.First(u => u.Email.Equals(email));
            if (administrator == null) throw new Exception("Administrator do not exist");
            if (!administrator.Password.Equals(password)) throw new Exception("Password incorrect");
            return administrator;
        }

        public Administrator RegisterAdministrator(Administrator administrator)
        {
            Administrator? first = null;
            try
            {
                first = AllAdministrators.First(u => u.Email.Equals(administrator.Email));
            }
            catch (Exception e)
            {
            }

            if (first != null)
            {
                throw new Exception("This Email is already registered");
            }

            int max = AllAdministrators.Max(a => a.Id);
            administrator.Id = (++max);
            AllAdministrators.Add(administrator);
            Save();
            return administrator;
        }
        
        public Administrator RemoveAdministrator(Administrator administrator)
        {
            Administrator toRemove = AllAdministrators.First(u => u.Email == administrator.Email);
            if (toRemove == null) throw new Exception("Administrator does not exist");
            if (!toRemove.Password.Equals(administrator.Password)) throw new Exception("Password incorrect");
            AllAdministrators.Remove(toRemove);
            Save();
            return toRemove;
        }
    }
}