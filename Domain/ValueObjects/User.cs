namespace TribalWarsBot.Services
{
    public class User
    {
        public string Name{ get; }
        public string Password { get; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}