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

        public void Update(Subscriber newSubscriber)
        {
            NotifyObservers(newSubscriber);
            AddSubscriber(newSubscriber);
        }

        private void AddSubscriber(Subscriber newSubscriber)
        {
            _dbContext.Subscribers.Add(newSubscriber);
            _dbContext.SaveChanges();
        }

        private void NotifyObservers(Subscriber newSubscriber)
        {
            var otherSubscribers = _dbContext.Subscribers.Include(u=> u.User).ToList();
            LoggingDecorator emailSender = new LoggingDecorator(_configuration);
            foreach (var user in otherSubscribers)
            {
                Console.WriteLine($"Send to {user.User.Email}: New user {newSubscriber.User.Name}");
                emailSender.SayHi(user.User.Email, newSubscriber.User.Name);
            }
        }
    }
}