using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public enum mode
    {
        easy = 0,
        hard = 1
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public mode Mode{ get; set; }
  
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
            public Builder SetMode(mode mode) {
                _user.Mode = mode;
                return this;
            }
            public User Build()
            {
                return _user;
            }
        }
    }

}
