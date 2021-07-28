namespace IRecommendGames.Model
{
    class User
    {
        private string login;
        private string name;
        private string password;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public User(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }
    }
}