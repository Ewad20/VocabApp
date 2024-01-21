using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System;
using System.Runtime.Serialization;

namespace ZTPAPP.Models
{
    public class SubscribeOberver
    {
        private readonly WDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public SubscribeOberver(WDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public void Update(Subscriber newUser)
        {
            NotifyObservers(newUser);
            AddUser(newUser);
        }

        private void AddUser(Subscriber newUser)
        {
            _dbContext.Subscribers.Add(newUser);
            _dbContext.SaveChanges();
        }

        private void NotifyObservers(Subscriber newUser)
        {
            var otherSubscribers = _dbContext.Subscribers.Include(u=> u.User).ToList();
            EmailSender emailSender = new EmailSender(_configuration);
            foreach (var user in otherSubscribers)
            {
                Console.WriteLine($"Send to {user.User.Email}: New user {newUser.User.Name}");
                emailSender.SayHi(user.User.Email, newUser.User.Name);
            }
        }
    }
}