using DomainModel.Models;
using FlightManagementWebAPI.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagementWebAPI.Repositories
{
    public class UserRepository
    {
        private readonly AirportSystemContext _airportSystemContext;
        public UserRepository(AirportSystemContext airportSystemContext)
        {
            _airportSystemContext = airportSystemContext;
        }

        public List<User> GetUsers()
        {
            return _airportSystemContext.Users.ToList();
        }

        public void InsertUser(User user)
        {
            _airportSystemContext.Users.Add(user);
            _airportSystemContext.SaveChanges();
        }

        public User GetUser(int userId)
        {
            return _airportSystemContext.Users.FirstOrDefault(user => user.Id.Equals(userId));
        }

        public void UpdateUser(User user)
        {
            var userForUpdate = GetUser(user.Id);
            if (userForUpdate != null)
            {
                userForUpdate.Name = user.Name;
                userForUpdate.Username = user.Username;
                userForUpdate.Email = user.Email;
                userForUpdate.Password = user.Password;
                userForUpdate.Role = user.Role;

                _airportSystemContext.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                _airportSystemContext.Users.Remove(user);
                _airportSystemContext.SaveChanges();
            }
        }
        public User LoginUser(string username, string password)
        {
            User user1 = _airportSystemContext.Users.FirstOrDefault(user => user.Username.Equals(username));
            User user2 = _airportSystemContext.Users.FirstOrDefault(user => user.Email.Equals(username));
            User user = new User();

            user.Username = username;
            user.Password = password;
            user.Role = null;

            if (user1!= null && user1.Password == password) 
            {
                user = user1;
            }
            if (user2!= null && user2.Password == password)
            {
                user = user2;
            }
            return user;
        }

        public override bool Equals(object obj)
        {
            return obj is UserRepository repository&&
                   EqualityComparer<AirportSystemContext>.Default.Equals(_airportSystemContext, repository._airportSystemContext);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_airportSystemContext);
        }
    }
}
