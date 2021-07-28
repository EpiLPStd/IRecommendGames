using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace IRecommendGames.Model
{
    class DBConnection
    {
        #region Zmienne
        MySqlConnectionStringBuilder ConnectionSB;
        MySqlConnection Connection;
        MySqlConnection Connectionn;
        public string message = string.Empty;
        private List<User> GetUsers;
        private Dictionary<string, string> LoginPassword;
        #endregion

        public DBConnection() { }

        #region Funkcje wykonujące polecenia SQL
        // Dodanie nowego użytkownika do bazy
        public void AddUser(User user)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            if (CheckUser(user))
            {
                using (Connection = new MySqlConnection(ConnectionSB.ToString()))
                {
                    MySqlCommand addUserCommand = new MySqlCommand(newUser(user), Connection);
                    Connection.Open();

                    var dataReader = addUserCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            message += dataReader.ToString();
                        }
                    }

                    Connection.Close();
                }

                message = "Poprawnie utworzono użytkownika.";
            }
            else message = "Użytkownik o podanym loginie już istnieje.";
        }

        // Dodanie preferencji użytkownika do bazy danych
        public void InsertPreference(string preference)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand toInputTableCommand = new MySqlCommand(toTablePreference(preference), Connection);
                Connection.Open();

                var dataReader = toInputTableCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message += dataReader.ToString();
                    }
                }

                Connection.Close();
            }
        }

        // Dodanie wyniku działania algorytmu do bazy danych
        public void InsertOutput(string gameName)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            int timesAlready = CheckAppearances(gameName) + 1;

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand toOutputTableCommand = new MySqlCommand(toTableGameName(timesAlready, gameName), Connection);
                Connection.Open();

                var dataReader = toOutputTableCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message += dataReader.ToString();
                    }
                }

                Connection.Close();
            }
        }

        // Wstawienie id_u, id_in oraz id_out do tabeli UserSearch
        public void InsertUserSearch(int userId, int inputId, int outputId)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand addUserSearchCommand = new MySqlCommand(setUserSearch(userId, inputId, outputId), Connection);
                Connection.Open();

                var dataReader = addUserSearchCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message += dataReader.ToString();
                    }
                }

                Connection.Close();
            }
        }

        // Zmiana loginu użytkownika o podanym id
        public void ChangeLogin(string newLogin, int userId)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand changeLoginCommand = new MySqlCommand(changeLogin(newLogin, userId), Connection);
                Connection.Open();

                var dataReader = changeLoginCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message += dataReader.ToString();
                    }
                }

                Connection.Close();
            }
        }

        // Zmiana hasła użytkownika o podanym id
        public void ChangePassword(string newPassword, int userId)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand changePasswordCommand = new MySqlCommand(changePassword(newPassword, userId), Connection);
                Connection.Open();

                var dataReader = changePasswordCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message += dataReader.ToString();
                    }
                }

                Connection.Close();
            }
        }
        
        // Usunięcie historii wyszukiwań użytkownika na podstawie id preferencji
        public void DeleteHistory(int inputId)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand deleteHistoryCommand = new MySqlCommand(deleteHistory(inputId), Connection);
                Connection.Open();

                var dataReader = deleteHistoryCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message += dataReader.ToString();
                    }
                }

                Connection.Close();
            }
        }

        // Usunięcie konta użytkownika o podanym id
        public void DeleteAccount(int userId)
        {
            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand deleteAccountCommand = new MySqlCommand(deleteAccount(userId), Connection);
                Connection.Open();

                var dataReader = deleteAccountCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message += dataReader.ToString();
                    }
                }

                Connection.Close();
            }
        }
                
        #region Funkcje pomocnicze
        // Sprawdzenie, czy użytkownik o podanym loginie istnieje już w bazie danych
        public bool CheckUser(User user)
        {
            GetUsers = new List<User>();

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand checkUserCommand = new MySqlCommand("SELECT * FROM User", Connection);
                Connection.Open();

                var dataReader = checkUserCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        GetUsers.Add(new User(dataReader["Name"].ToString(), dataReader["Login"].ToString(), dataReader["Password"].ToString()));
                    }
                }

                Connection.Close();
            }

            if (GetUsers != null)
            {
                foreach (User existingUser in GetUsers)
                {
                    if (existingUser.Login == user.Login) return false;
                }
            }

            return true;
        }

        // Sprawdzenie, czy hasło wprowadzone w kontrolce login zgadza się z tym w bazie danych
        public bool CheckPassword(User user)
        {
            LoginPassword = new Dictionary<string, string>();

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand checkPasswordCommand = new MySqlCommand("SELECT Login, Password FROM User", Connection);
                Connection.Open();

                var dataReader = checkPasswordCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        LoginPassword.Add(dataReader["Login"].ToString(), dataReader["Password"].ToString());
                    }
                }

                Connection.Close();
            }

            if (LoginPassword != null)
            {
                foreach (string login in LoginPassword.Keys)
                {
                    if (login == user.Login & LoginPassword[login] == user.Password) return true;
                }
            }

            return false;
        }

        // Sprawdzenie ilości wystąpień gry o podanym tytule w tabeli Output
        private int CheckAppearances(string gameName)
        {
            int gameAppearances;

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connectionn = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand getAppearancesCommand = new MySqlCommand(getAppearances(gameName), Connectionn);
                Connectionn.Open();

                var dataReader = getAppearancesCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message = dataReader["Appearances"].ToString();
                    }
                }

                if (message == string.Empty) gameAppearances = 0;
                else gameAppearances = int.Parse(message);

                Connectionn.Close();

                return gameAppearances;
            }
        }

        // Zwrócenie id użytkownika o podanym loginie z tabeli User
        public int GetUserId(string userLogin)
        {
            int userId;

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connectionn = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand getUserIdCommand = new MySqlCommand(getUserId(userLogin), Connectionn);
                Connectionn.Open();

                var dataReader = getUserIdCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message = dataReader["id_u"].ToString();
                    }
                }

                userId = int.Parse(message);
                Connectionn.Close();

                return userId;
            }
        }

        // Zwrócenie id wprowadzonych preferencji na podstawie treści preferencji z tabeli Input
        public int GetInputId(string inputValue)
        {
            int inputId;

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connectionn = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand getInputIdCommand = new MySqlCommand(getInputId(inputValue), Connectionn);
                Connectionn.Open();

                var dataReader = getInputIdCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message = dataReader["id_i"].ToString();
                    }
                }

                inputId = int.Parse(message);
                Connectionn.Close();

                return inputId;
            }
        }

        // Zwrócenie listy id preferencji użytkownika o podanym id z tabeli UserSearch
        public List<int> GetInputId(int userId)
        {
            List<int> userInputId = new List<int>();

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand getInputIdCommand = new MySqlCommand(getInputId(userId), Connection);
                Connection.Open();

                var dataReader = getInputIdCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        userInputId.Add(int.Parse(dataReader["id_in"].ToString()));
                    }
                }

                Connection.Close();

                return userInputId;
            }
        }

        // Zwrócenie ostatniego id z tabeli Output
        public int GetOutputId()
        {
            int outputId;

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connectionn = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand getOutputIdCommand = new MySqlCommand("SELECT id_o FROM Output", Connectionn);
                Connectionn.Open();

                var dataReader = getOutputIdCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        message = dataReader["id_o"].ToString();
                    }
                }

                if (message == string.Empty) outputId = 0;
                else outputId = int.Parse(message);

                Connectionn.Close();

                return outputId;
            }
        }

        // Zwrócenie historii wyszukiwań użytkownika o podanym id w postaci listy preferencji oraz wyszukanych tytułów
        public List<string> GetMyHistory(int userId)
        {
            List<string> myGames = new List<string>();

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand getMyHistoryCommand = new MySqlCommand(getMyHistory(userId), Connection);
                Connection.Open();

                var dataReader = getMyHistoryCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        myGames.Add(dataReader["Preferences"].ToString() + "|" + dataReader["GameInfo"].ToString());
                    }
                }

                Connection.Close();

                return myGames;
            }
        }

        // Zwrócenie listy wszystkich wyszukanych tytułów w postaci listy powtórzeń tytułu w bazie oraz tytułu
        public List<string> GetGlobalStats()
        {
            List<string> globalStats = new List<string>();

            ConnectionSB = new MySqlConnectionStringBuilder();
            ConnectionSB.UserID = "IRGAdmin";
            ConnectionSB.Password = "Admin123";
            ConnectionSB.Database = "IRecommendGames";
            ConnectionSB.Server = "localhost";

            using (Connection = new MySqlConnection(ConnectionSB.ToString()))
            {
                MySqlCommand globalStatsCommand = new MySqlCommand(getGlobalStats(), Connection);
                Connection.Open();

                var dataReader = globalStatsCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        globalStats.Add(dataReader["Appearances"].ToString() + "|" + dataReader["GameInfo"].ToString());
                    }
                }

                Connection.Close();

                return globalStats;
            }
        }
        #endregion
        #endregion

        #region Polecenia SQL
        // Dodanie nowego użytkownika do bazy
        private string newUser(User user)
        {
            return $"INSERT INTO User (Name, Login, Password) VALUES ('{user.Name}', '{user.Login}', '{user.Password}')";
        }

        // Dodanie preferencji użytkownika do bazy danych
        private string toTablePreference(string preference)
        {
            return $"INSERT INTO Input (Preferences) VALUES ('{preference}')";
        }

        // Dodanie wyniku działania algorytmu do bazy danych
        private string toTableGameName(int appearances, string gameName)
        {
            return $"INSERT INTO Output (Appearances, GameInfo) VALUES ({appearances}, '{gameName}')";
        }

        // Sprawdzenie ilości wystąpień gry o podanym tytule w tabeli Output
        private string getAppearances(string gameName)
        {
            return $"SELECT COUNT(*) Appearances FROM Output WHERE GameInfo = '{gameName}'";
        }

        // Zwrócenie id użytkownika o podanym loginie z tabeli User
        private string getUserId(string userLogin)
        {
            return $"SELECT id_u FROM User WHERE Login='{userLogin}'";
        }

        // Zwrócenie id wprowadzonej preferencji na podstawie listy preferencji z tabeli Input
        private string getInputId(string inputValue)
        {
            return $"SELECT id_i FROM Input WHERE Preferences='{inputValue}'";
        }

        // Zwrócenie listy id preferencji użytkownika o podanym id z tabeli UserSearch
        private string getInputId(int userId)
        {
            return $"SELECT DISTINCT id_in FROM UserSearch WHERE id_u={userId}";
        }

        // Wstawienie id_u, id_in oraz id_out do tabeli UserSearch
        private string setUserSearch(int userId, int inputId, int outputId)
        {
            return $"INSERT INTO UserSearch (id_u, id_in, id_out) VALUES ({userId}, {inputId}, {outputId})";
        }

        // Zwrócenie historii wyszukiwań użytkownika o podanym id w postaci listy preferencji oraz wyszukanych tytułów
        private string getMyHistory(int userId)
        {
            return $"SELECT DISTINCT Preferences, GameInfo from Input, Output, UserSearch WHERE id_i = id_in AND id_o = id_out AND id_u={userId}";
        }

        // Zwrócenie listy wszystkich wyszukanych tytułów w postaci listy powtórzeń tytułu w bazie oraz tytułu
        private string getGlobalStats()
        {
            return $"SELECT COUNT(*) Appearances, GameInfo FROM Output GROUP BY 2 ORDER BY 1 DESC";
        }

        // Zmiana loginu użytkownika o podanym id
        private string changeLogin(string newLogin, int userId)
        {
            return $"UPDATE User SET Login='{newLogin}' WHERE id_u={userId}";
        }

        // Zmiana hasła użytkownika o podanym id
        private string changePassword(string newPassword, int userId)
        {
            return $"UPDATE User SET Password='{newPassword}' WHERE id_u={userId}";
        }

        // Usunięcie historii wyszukiwań użytkownika na podstawie id preferencji
        private string deleteHistory(int inputId)
        {
            return $"DELETE FROM UserSearch WHERE id_in={inputId}; DELETE FROM Input WHERE id_i={inputId}";
        }

        // Usunięcie konta użytkownika o podanym id
        private string deleteAccount(int userId)
        {
            return $"DELETE FROM UserSearch WHERE id_u={userId}; DELETE FROM User WHERE id_u={userId}";
        }
        #endregion
    }
}