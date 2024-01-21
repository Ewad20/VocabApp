using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
  
        public class Builder
        {
            private readonly User _user = new User();

            public Builder SetName(string name)
            {
                _user.Name = name;
                return this;
            }

            public Builder SetEmail(string email)
            {
                _user.Email = email;
                return this;
            }

            public Builder SetPassword(string password)
            {
                _user.Password = password;
                return this;
            }
            public User Build()
            {
                return _user;
            }
        }
    }

}
