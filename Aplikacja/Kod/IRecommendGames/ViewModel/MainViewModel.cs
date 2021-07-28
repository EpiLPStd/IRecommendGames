using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IRecommendGames.Model;
using IRecommendGames.Model.Range;
using IRecommendGames.Model.Static;
using IRecommendGames.ViewModel.BaseClass;

namespace IRecommendGames.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        #region Zmienne
        private Games Game;
        private DatabaseFix FixSteam;
        private Model.IRecommendGames GetRecommendGames;
        private DBConnection connection;

        private string preferences;

        private Date GameDate;
        private Gametime GameTime;
        private Price GamePrice;
        private Reviews GameReviews;

        private string category;
        private string categoryWeight;
        private string gameCategoryMessage;
        private string developer;
        private string developerWeight;
        private string gameDeveloperMessage;
        private string genre;
        private string genreWeight;
        private string gameGenreMessage;
        private string platform;
        private string platformWeight;
        private string gamePlatformMessage;
        private string publisher;
        private string publisherWeight;
        private string gamePublisherMessage;
        private string tag;
        private string tagWeight;
        private string gameTagMessage;
        #endregion

        public MainViewModel()
        {
            Game = new Games();
            NewForm();
            LogIn();
        }

        #region Elementy kontrolek
        #region LogIn
        #region Status
        private string statusLogin = "Witaj w programie IRecommendGames.";
        public string StatusLogin
        {
            get => statusLogin;
            set
            {
                if (statusLogin != value)
                {
                    statusLogin = value;
                    onPropertyChange(nameof(StatusLogin));
                }
            }
        }
        #endregion

        #region Login TextBlock
        private string loginTextBlock;
        public string LoginTextBlock
        {
            get => loginTextBlock;
            set
            {
                if (value == null)
                {
                    loginTextBlock = "";
                }
                else loginTextBlock = value;

                onPropertyChange(nameof(LoginTextBlock));
            }
        }
        #endregion

        #region Hasło PasswordBox
        private string passwordBlock;
        public string PasswordBlock
        {
            get => passwordBlock;
            set
            {
                if (value == null)
                {
                    passwordBlock = "";
                }
                else passwordBlock = value;

                onPropertyChange(nameof(PasswordBlock));
            }
        }

        #endregion
        #endregion

        #region SignIn
        #region Status
        private string statusSignIn;
        public string StatusSignIn
        {
            get => statusSignIn;
            set
            {
                if (statusSignIn != value)
                {
                    statusSignIn = value;
                    onPropertyChange(nameof(StatusSignIn));
                }
            }
        }
        #endregion

        #region Imię TextBox
        private string nameTextBlock;
        public string NameTextBlock
        {
            get => nameTextBlock;
            set
            {
                if (value == null)
                {
                    nameTextBlock = "";
                }
                else nameTextBlock = value;

                onPropertyChange(nameof(NameTextBlock));
            }
        }
        #endregion

        #region Nazwisko TextBox
        private string surnameTextBlock;
        public string SurnameTextBlock
        {
            get => surnameTextBlock;
            set
            {
                if (value == null)
                {
                    surnameTextBlock = "";
                }
                else surnameTextBlock = value;

                onPropertyChange(nameof(SurnameTextBlock));
            }
        }
        #endregion

        #region Login TextBox
        private string loginTextBlock2;
        public string LoginTextBlock2
        {
            get => loginTextBlock2;
            set
            {
                if (value == null)
                {
                    loginTextBlock2 = "";
                }
                else loginTextBlock2 = value;

                onPropertyChange(nameof(LoginTextBlock2));
            }
        }
        #endregion

        #region Hasło TextBox
        private string signinPasswordTextBlock;
        public string SigninPasswordTextBlock
        {
            get => signinPasswordTextBlock;
            set
            {
                if (value == null)
                {
                    signinPasswordTextBlock = "";
                }
                else signinPasswordTextBlock = value;

                onPropertyChange(nameof(SigninPasswordTextBlock));
            }
        }
        #endregion
        #endregion

        #region Start
        #region Status
        private string statusMenu;
        public string StatusMenu
        {
            get => statusMenu;
            set
            {
                if (statusMenu != value)
                {
                    statusMenu = value;
                    onPropertyChange(nameof(StatusMenu));
                }
            }
        }
        #endregion
        #endregion

        #region DBFix
        #region Status
        private string statusDatabaseFix;
        public string StatusDatabaseFix
        {
            get => statusDatabaseFix;
            set
            {
                if (statusDatabaseFix != value)
                {
                    statusDatabaseFix = value;
                    onPropertyChange(nameof(StatusDatabaseFix));
                }
            }
        }
        #endregion
        #endregion

        #region PreferenceForm
        #region ParametersList
        private void ParametersLists()
        {
            List<string> parameters = new List<string> { "categories", "developer", "genres", "platforms", "publisher", "steamspy_tags" };
            int parameterId = 0;

            Categories.Clear();
            Developers.Clear();
            Genres.Clear();
            Platforms.Clear();
            Publishers.Clear();
            Tags.Clear();

            foreach (string parameter in parameters)
            {
                int id = 1;

                if (Game.Attributes[parameter] == null) continue;

                switch (parameterId)
                {
                    case 0:
                        foreach (string category in Game.Attributes[parameter])
                        {
                            Categories.Add(new Categories { Id = id, GameCategories = category });
                            id++;
                        }
                        break;

                    case 1:
                        foreach (string developer in Game.Attributes[parameter])
                        {
                            Developers.Add(new Developer { Id = id, GameDeveloper = developer });
                            id++;
                        }
                        break;

                    case 2:
                        foreach (string genre in Game.Attributes[parameter])
                        {
                            Genres.Add(new Genres { Id = id, GameGenres = genre });
                            id++;
                        }
                        break;

                    case 3:
                        foreach (string platform in Game.Attributes[parameter])
                        {
                            Platforms.Add(new Platforms { Id = id, GamePlatforms = platform });
                            id++;
                        }
                        break;

                    case 4:
                        foreach (string publisher in Game.Attributes[parameter])
                        {
                            Publishers.Add(new Publisher { Id = id, GamePublisher = publisher });
                            id++;
                        }
                        break;

                    case 5:
                        foreach (string tag in Game.Attributes[parameter])
                        {
                            Tags.Add(new Tags { Id = id, GameTags = tag });
                            id++;
                        }
                        break;
                }

                parameterId++;
            }
        }
        #endregion

        #region Status
        private string status = "Wprowadź preferencje.";
        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    onPropertyChange(nameof(Status));
                }
            }
        }
        #endregion

        #region Parametry w formularzu
        // Przedział
        #region Date
        private DateTime fromDatePicker = new DateTime(1998, 11, 8);
        public DateTime FromDatePicker
        {
            get { return fromDatePicker; }
            set
            {
                fromDatePicker = value;
                onPropertyChange(nameof(FromDatePicker));
            }
        }

        private DateTime toDatePicker = new DateTime(2019, 4, 18);
        public DateTime ToDatePicker
        {
            get
            {
                return toDatePicker;
            }
            set
            {
                toDatePicker = value;
                onPropertyChange(nameof(ToDatePicker));
            }
        }

        private int gameDateWeight;
        public int GameDateWeight
        {
            get
            {
                if (!int.TryParse(gameDateWeight.ToString(), out int weight)) return gameDateWeight = 0;
                else if (weight < 0) return gameDateWeight = 0;
                else if (weight > 100) return gameDateWeight = 100;
                else GameDate.DateWeight = weight;

                return gameDateWeight;
            }
            set
            {
                gameDateWeight = value;
                onPropertyChange(nameof(GameDateWeight));
            }
        }

        private void GameDateMessage()
        {
            if (toDatePicker != null)
            {
                GameDate.ReleaseDate = $"Data:{fromDatePicker.ToString().Split(' ')[0]}-{toDatePicker.ToString().Split(' ')[0]};Waga:{gameDateWeight.ToString()}";
            }
            else if (fromDatePicker != null)
            {
                GameDate.ReleaseDate = $"Data:{fromDatePicker.ToString().Split(' ')[0]}-y;Waga:{gameDateWeight.ToString()}";
            }
            else
            {
                GameDate.ReleaseDate = $"Data:x-y;Waga:{gameDateWeight.ToString()}";
            }

            if (fromDatePicker != null)
            {
                GameDate.ReleaseDate = $"Data:{fromDatePicker.ToString().Split(' ')[0]}-{toDatePicker.ToString().Split(' ')[0]};Waga:{gameDateWeight.ToString()}";
            }
            else if (toDatePicker != null)
            {
                GameDate.ReleaseDate = $"Data:x-{toDatePicker.ToString().Split(' ')[0]};Waga:{gameDateWeight.ToString()}";
            }
            else
            {
                GameDate.ReleaseDate = $"Data:x-y;Waga:{gameDateWeight.ToString()}";
            }
        }

        private bool dateClickable = true;
        public bool DateClickable
        {
            get { return dateClickable; }
            set
            {
                dateClickable = value;
                onPropertyChange(nameof(DateClickable));
            }
        }
        #endregion

        #region Gametime
        private int fromGametime;
        public int FromGametime
        {
            get { return fromGametime; }
            set
            {
                fromGametime = value;
                onPropertyChange(nameof(FromGametime));
            }
        }

        private int toGametime;
        public int ToGametime
        {
            get { return toGametime; }
            set
            {
                toGametime = value;
                onPropertyChange(nameof(ToGametime));
            }
        }

        private int gameGametimeWeight;
        public int GameGametimeWeight
        {
            get
            {
                if (!int.TryParse(gameGametimeWeight.ToString(), out int weight)) return gameGametimeWeight = 0;
                else if (weight < 0) return gameGametimeWeight = 0;
                else if (weight > 100) return gameGametimeWeight = 100;
                else GameTime.AverageGametimeWeight = weight;

                return gameGametimeWeight;
            }
            set
            {
                gameGametimeWeight = value;
                onPropertyChange(nameof(GameGametimeWeight));
            }
        }

        private void GameGametimeMessage()
        {
            if (toGametime.ToString() != "")
            {
                GameTime.AverageGametime = $"Sredni_czas_gry:{fromGametime.ToString().Split(' ')[0]}-{toGametime.ToString().Split(' ')[0]};Waga:{gameGametimeWeight.ToString()}";
            }
            else if (fromGametime.ToString() != "")
            {
                GameTime.AverageGametime = $"Sredni_czas_gry:{fromGametime.ToString().Split(' ')[0]}-y;Waga:{gameGametimeWeight.ToString()}";
            }
            else
            {
                GameTime.AverageGametime = $"Sredni_czas_gry:x-y;Waga:{gameGametimeWeight.ToString()}";
            }

            if (fromGametime.ToString() != "")
            {
                GameTime.AverageGametime = $"Sredni_czas_gry:{fromGametime.ToString().Split(' ')[0]}-{toGametime.ToString().Split(' ')[0]};Waga:{gameGametimeWeight.ToString()}";
            }
            else if (toGametime.ToString() != "")
            {
                GameTime.AverageGametime = $"Sredni_czas_gry:x-{toGametime.ToString().Split(' ')[0]};Waga:{gameGametimeWeight.ToString()}";
            }
            else
            {
                GameTime.AverageGametime = $"Sredni_czas_gry:x-y;Waga:{gameGametimeWeight.ToString()}";
            }
        }

        private bool gametimeClickable = true;
        public bool GametimeClickable
        {
            get { return gametimeClickable; }
            set
            {
                gametimeClickable = value;
                onPropertyChange(nameof(GametimeClickable));
            }
        }
        #endregion

        #region Price
        private float fromPrice;
        public float FromPrice
        {
            get { return fromPrice; }
            set
            {
                if (!string.Equals(fromPrice, value))
                {
                    fromPrice = value;
                    onPropertyChange(nameof(FromPrice));
                }
            }
        }

        private float toPrice;
        public float ToPrice
        {
            get { return toPrice; }
            set
            {
                if (!string.Equals(toPrice, value))
                {
                    toPrice = value;
                    onPropertyChange(nameof(ToPrice));
                }
            }
        }

        private int gamePriceWeight;
        public int GamePriceWeight
        {
            get
            {
                if (gamePriceWeight.ToString() == "") GamePrice.PriceWeight = 0;
                else if (!int.TryParse(gamePriceWeight.ToString(), out int weight)) return gamePriceWeight = 0;
                else if (weight < 0) return gamePriceWeight = 0;
                else if (weight > 100) return gamePriceWeight = 100;
                else GamePrice.PriceWeight = weight;

                return gamePriceWeight;
            }
            set
            {
                gamePriceWeight = value;
                onPropertyChange(nameof(GamePriceWeight));
            }
        }

        private void GamePriceMessage()
        {
            if (toPrice.ToString() != "")
            {
                GamePrice.GamePrice = $"Cena:{fromPrice.ToString().Split(' ')[0]}-{toPrice.ToString().Split(' ')[0]};Waga:{gamePriceWeight.ToString()}";
            }
            else if (fromPrice.ToString() != "")
            {
                GamePrice.GamePrice = $"Cena:{fromPrice.ToString().Split(' ')[0]}-y;Waga:{gamePriceWeight.ToString()}";
            }
            else
            {
                GamePrice.GamePrice = $"Cena:x-y;Waga:{gamePriceWeight.ToString()}";
            }

            if (fromPrice.ToString() != "")
            {
                GamePrice.GamePrice = $"Cena:{fromPrice.ToString().Split(' ')[0]}-{toPrice.ToString().Split(' ')[0]};Waga:{gamePriceWeight.ToString()}";
            }
            else if (toPrice.ToString() != "")
            {
                GamePrice.GamePrice = $"Cena:x-{toPrice.ToString().Split(' ')[0]};Waga:{gamePriceWeight.ToString()}";
            }
            else
            {
                GamePrice.GamePrice = $"Cena:x-y;Waga:{gamePriceWeight.ToString()}";
            }
        }

        private bool priceClickable = true;
        public bool PriceClickable
        {
            get { return priceClickable; }
            set
            {
                priceClickable = value;
                onPropertyChange(nameof(PriceClickable));
            }
        }
        #endregion

        #region Reviews
        private int fromReviews;
        public int FromReviews
        {
            get { return fromReviews; }
            set
            {
                if (!string.Equals(fromReviews, value))
                {
                    fromReviews = value;
                    onPropertyChange(nameof(FromReviews));
                }
            }
        }

        private int toReviews;
        public int ToReviews
        {
            get { return toReviews; }
            set
            {
                if (!string.Equals(toReviews, value))
                {
                    toReviews = value;
                    onPropertyChange(nameof(ToReviews));
                }
            }
        }

        private int gameReviewsWeight;
        public int GameReviewsWeight
        {
            get
            {
                if (gameReviewsWeight.ToString() == "") GameReviews.PositiveReviewsPercentageWeight = 0;
                else if (!int.TryParse(gameReviewsWeight.ToString(), out int weight)) return gameReviewsWeight = 0;
                else if (weight < 0) return gameReviewsWeight = 0;
                else if (weight > 100) return gameReviewsWeight = 100;
                else GameReviews.PositiveReviewsPercentageWeight = weight;

                return gameReviewsWeight;
            }
            set
            {
                gameReviewsWeight = value;
                onPropertyChange(nameof(GameReviewsWeight));
            }
        }

        private void GameReviewsMessage()
        {
            if (toReviews.ToString() != "")
            {
                GameReviews.PositiveReviews = $"%_Pozytywnych_recenzji:{fromReviews.ToString().Split(' ')[0]}-{toReviews.ToString().Split(' ')[0]};Waga:{gameReviewsWeight.ToString()}";
            }
            else if (fromReviews.ToString() != "")
            {
                GameReviews.PositiveReviews = $"%_Pozytywnych_recenzji:{fromReviews.ToString().Split(' ')[0]}-y;Waga:{gameReviewsWeight.ToString()}";
            }
            else
            {
                GameReviews.PositiveReviews = $"%_Pozytywnych_recenzji:x-y;Waga:{gameReviewsWeight.ToString()}";
            }

            if (fromReviews.ToString() != "")
            {
                GameReviews.PositiveReviews = $"%_Pozytywnych_recenzji:{fromReviews.ToString().Split(' ')[0]}-{toReviews.ToString().Split(' ')[0]};Waga:{gameReviewsWeight.ToString()}";
            }
            else if (toReviews.ToString() != "")
            {
                GameReviews.PositiveReviews = $"%_Pozytywnych_recenzji:x-{toReviews.ToString().Split(' ')[0]};Waga:{gameReviewsWeight.ToString()}";
            }
            else
            {
                GameReviews.PositiveReviews = $"%_Pozytywnych_recenzji:x-y;Waga:{gameReviewsWeight.ToString()}";
            }
        }

        private bool reviewsClickable = true;
        public bool ReviewsClickable
        {
            get { return reviewsClickable; }
            set
            {
                reviewsClickable = value;
                onPropertyChange(nameof(ReviewsClickable));
            }
        }
        #endregion

        // Stałe
        #region Category
        private ObservableCollection<Categories> categories;
        public ObservableCollection<Categories> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        private Categories gameCategory;
        public Categories GameCategory
        {
            get { return gameCategory; }
            set
            {
                if (value == null)
                {
                    gameCategory = value;
                    category = "";
                }
                else
                {
                    gameCategory = value;
                    category = value.GameCategories;
                }

                onPropertyChange(nameof(GameCategory));
            }
        }

        private int gameCategoryWeight;
        public int GameCategoryWeight
        {
            get
            {
                if (gameCategoryWeight.ToString() == "") return gameCategoryWeight = 0;
                else if (!int.TryParse(gameCategoryWeight.ToString(), out int weight)) return gameCategoryWeight = 0;
                else if (weight < 0) return gameCategoryWeight = 0;
                else if (weight > 100) return gameCategoryWeight = 100;
                else return gameCategoryWeight = weight;
            }
            set
            {
                gameCategoryWeight = value;
                categoryWeight = value.ToString();
                onPropertyChange(nameof(GameCategoryWeight));
            }
        }

        private void GameCategoryMessage(string category, string categoryWeight)
        {
            if (category == null & categoryWeight == null) gameCategoryMessage = "Kategoria:x;Waga:y";
            else if (category == null) gameCategoryMessage = $"Kategoria:x;Waga:{categoryWeight}";
            else if (categoryWeight == null) gameCategoryMessage = $"Kategoria:{category};Waga:y";
            else gameCategoryMessage = $"Kategoria:{category};Waga:{categoryWeight}";
        }

        private bool categoryClicked = true;
        #endregion

        #region Developer
        private ObservableCollection<Developer> developers;
        public ObservableCollection<Developer> Developers
        {
            get { return developers; }
            set { developers = value; }
        }

        private Developer gameDeveloper;
        public Developer GameDeveloper
        {
            get { return gameDeveloper; }
            set
            {
                if (value == null)
                {
                    gameDeveloper = value;
                    developer = "";
                }
                else
                {
                    gameDeveloper = value;
                    developer = value.GameDeveloper;
                }

                onPropertyChange(nameof(GameDeveloper));
            }
        }

        private int gameDeveloperWeight;
        public int GameDeveloperWeight
        {
            get
            {
                if (gameDeveloperWeight.ToString() == "") return gameDeveloperWeight = 0;
                else if (!int.TryParse(gameDeveloperWeight.ToString(), out int weight)) return gameDeveloperWeight = 0;
                else if (weight < 0) return gameDeveloperWeight = 0;
                else if (weight > 100) return gameDeveloperWeight = 100;
                else return gameDeveloperWeight = weight;
            }
            set
            {
                gameDeveloperWeight = value;
                developerWeight = value.ToString();
                onPropertyChange(nameof(GameDeveloperWeight));
            }
        }

        private void GameDeveloperMessage(string developer, string developerWeight)
        {
            if (developer == null & developerWeight == null) gameDeveloperMessage = "Developer:x;Waga:y";
            else if (developer == null) gameDeveloperMessage = $"Developer:x;Waga:{developerWeight}";
            else if (developerWeight == null) gameDeveloperMessage = $"Developer:{developer};Waga:y";
            else gameDeveloperMessage = $"Developer:{developer};Waga:{developerWeight}";
        }

        private bool developerClicked = true;
        #endregion

        #region Genres
        private ObservableCollection<Genres> genres;
        public ObservableCollection<Genres> Genres
        {
            get { return genres; }
            set { genres = value; }
        }

        private Genres gameGenres;
        public Genres GameGenres
        {
            get { return gameGenres; }
            set
            {
                if (value == null)
                {
                    gameGenres = value;
                    genre = "";
                }
                else
                {
                    gameGenres = value;
                    genre = value.GameGenres;
                }

                onPropertyChange(nameof(GameGenres));
            }
        }

        private int gameGenreWeight;
        public int GameGenreWeight
        {
            get
            {
                if (gameGenreWeight.ToString() == "") return gameGenreWeight = 0;
                else if (!int.TryParse(gameGenreWeight.ToString(), out int weight)) return gameGenreWeight = 0;
                else if (weight < 0) return gameGenreWeight = 0;
                else if (weight > 100) return gameGenreWeight = 100;
                else return gameGenreWeight = weight;
            }
            set
            {
                gameGenreWeight = value;
                genreWeight = value.ToString();
                onPropertyChange(nameof(GameGenreWeight));
            }
        }

        private void GameGenreMessage(string genre, string genreWeight)
        {
            if (genre == null & genreWeight == null) gameGenreMessage = "Typ:x;Waga:y";
            else if (genre == null) gameGenreMessage = $"Typ:x;Waga:{genreWeight}";
            else if (genreWeight == null) gameGenreMessage = $"Typ:{genre};Waga:y";
            else gameGenreMessage = $"Typ:{genre};Waga:{genreWeight}";
        }

        private bool genreClicked = true;
        #endregion

        #region Platforms
        private ObservableCollection<Platforms> platforms;
        public ObservableCollection<Platforms> Platforms
        {
            get { return platforms; }
            set { platforms = value; }
        }

        private Platforms gamePlatform;
        public Platforms GamePlatform
        {
            get { return gamePlatform; }
            set
            {
                if (value == null)
                {
                    gamePlatform = value;
                    platform = "";
                }
                else
                {
                    gamePlatform = value;
                    platform = value.GamePlatforms;
                }

                onPropertyChange(nameof(GamePlatform));
            }
        }

        private int gamePlatformWeight;
        public int GamePlatformWeight
        {
            get
            {
                if (gamePlatformWeight.ToString() == "") return gamePlatformWeight = 0;
                else if (!int.TryParse(gamePlatformWeight.ToString(), out int weight)) return gamePlatformWeight = 0;
                else if (weight < 0) return gamePlatformWeight = 0;
                else if (weight > 100) return gamePlatformWeight = 100;
                else return gamePlatformWeight = weight;
            }
            set
            {
                gamePlatformWeight = value;
                platformWeight = value.ToString();
                onPropertyChange(nameof(GamePlatformWeight));
            }
        }

        private void GamePlatformMessage(string platform, string platformWeight)
        {
            if (platform == null & platformWeight == null) gamePlatformMessage = "Platforma:x;Waga:y";
            else if (platform == null) gamePlatformMessage = $"Platforma:x;Waga:{platformWeight}";
            else if (platformWeight == null) gamePlatformMessage = $"Platforma:{platform};Waga:y";
            else gamePlatformMessage = $"Platforma:{platform};Waga:{platformWeight}";
        }

        private bool platformClicked = true;
        #endregion

        #region Publisher
        private ObservableCollection<Publisher> publishers;
        public ObservableCollection<Publisher> Publishers
        {
            get { return publishers; }
            set { publishers = value; }
        }

        private Publisher gamePublisher;
        public Publisher GamePublisher
        {
            get { return gamePublisher; }
            set
            {
                if (value == null)
                {
                    gamePublisher = value;
                    publisher = "";
                }
                else
                {
                    gamePublisher = value;
                    publisher = value.GamePublisher;
                }

                onPropertyChange(nameof(GamePublisher));
            }
        }

        private int gamePublisherWeight;
        public int GamePublisherWeight
        {
            get
            {
                if (gamePublisherWeight.ToString() == "") return gamePublisherWeight = 0;
                else if (!int.TryParse(gamePublisherWeight.ToString(), out int weight)) return gamePublisherWeight = 0;
                else if (weight < 0) return gamePublisherWeight = 0;
                else if (weight > 100) return gamePublisherWeight = 100;
                else return gamePublisherWeight = weight;
            }
            set
            {
                gamePublisherWeight = value;
                publisherWeight = value.ToString();
                onPropertyChange(nameof(GamePublisherWeight));
            }
        }

        private void GamePublisherMessage(string publisher, string publisherWeight)
        {
            if (publisher == null & publisherWeight == null) gamePublisherMessage = "Wydawca:x;Waga:y";
            else if (publisher == null) gamePublisherMessage = $"Wydawca:x;Waga:{publisherWeight}";
            else if (publisherWeight == null) gamePublisherMessage = $"Wydawca:{publisher};Waga:y";
            else gamePublisherMessage = $"Wydawca:{publisher};Waga:{publisherWeight}";
        }

        private bool publisherClicked = true;
        #endregion

        #region Tags
        private ObservableCollection<Tags> tags;
        public ObservableCollection<Tags> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        private Tags gameTags;
        public Tags GameTags
        {
            get { return gameTags; }
            set
            {
                if (value == null)
                {
                    gameTags = null;
                    tag = "";
                }
                else
                {
                    gameTags = value;
                    tag = value.GameTags;
                }

                onPropertyChange(nameof(GameTags));
            }
        }

        private int gameTagsWeight;
        public int GameTagsWeight
        {
            get
            {
                if (gameTagsWeight.ToString() == "") return gameTagsWeight = 0;
                else if (!int.TryParse(gameTagsWeight.ToString(), out int weight)) return gameTagsWeight = 0;
                else if (weight < 0) return gameTagsWeight = 0;
                else if (weight > 100) return gameTagsWeight = 100;
                else return gameTagsWeight = weight;
            }
            set
            {
                gameTagsWeight = value;
                tagWeight = value.ToString();
                onPropertyChange(nameof(GameTagsWeight));
            }
        }

        private void GameTagMessage(string tag, string tagWeight)
        {
            if (tag == null & tagWeight == null) gameTagMessage = "Tag:x;Waga:y";
            else if (tag == null) gameTagMessage = $"Tag:x;Waga:{tagWeight}";
            else if (tagWeight == null) gameTagMessage = $"Tag:{tag};Waga:y";
            else gameTagMessage = $"Tag:{tag};Waga:{tagWeight}";
        }

        private bool tagClicked = true;
        #endregion
        #endregion

        #region PreferencesList
        private ObservableCollection<string> preferencesList = new ObservableCollection<string>();
        public ObservableCollection<string> PreferencesList
        {
            get
            {
                return preferencesList;
            }
            set
            {
                preferencesList = value;
                onPropertyChange(nameof(PreferencesList));
            }
        }
        #endregion

        private void ClearWindow()
        {
            if (!DateClickable)
            {
                FromDatePicker = new DateTime(1998, 11, 8);
                ToDatePicker = new DateTime(2019, 4, 18);
                GameDateWeight = 0;
                DateClickable = true;
            }

            if (!GametimeClickable)
            {
                FromGametime = 0;
                ToGametime = 0;
                GameGametimeWeight = 0;
                GametimeClickable = true;
            }

            if (!PriceClickable)
            {
                FromPrice = 0;
                ToPrice = 0;
                GamePriceWeight = 0;
                PriceClickable = true;
            }

            if (!ReviewsClickable)
            {
                FromReviews = 0;
                ToReviews = 0;
                GameReviewsWeight = 0;
                ReviewsClickable = true;
            }

            if (!categoryClicked)
            {
                GameCategory = null;
                GameCategoryWeight = 0;
                categoryClicked = true;
            }

            if (!developerClicked)
            {
                GameDeveloper = null;
                GameDeveloperWeight = 0;
                developerClicked = true;
            }

            if (!genreClicked)
            {
                GameGenres = null;
                GameGenreWeight = 0;
                genreClicked = true;
            }

            if (!platformClicked)
            {
                GamePlatform = null;
                GamePlatformWeight = 0;
                platformClicked = true;
            }

            if (!publisherClicked)
            {
                GamePublisher = null;
                GamePublisherWeight = 0;
                publisherClicked = true;
            }

            if (!tagClicked)
            {
                GameTags = null;
                GameTagsWeight = 0;
                tagClicked = true;
            }

            PreferencesList = new ObservableCollection<string>();
            Status = "Wprowadź preferencje.";
        }
        #endregion

        #region IRecommendGames
        #region Status
        private string statusIRecommendGames;
        public string StatusIRecommendGames
        {
            get => statusIRecommendGames;
            set
            {
                if (statusIRecommendGames != value)
                {
                    statusIRecommendGames = value;
                    onPropertyChange(nameof(StatusIRecommendGames));
                }
            }
        }
        #endregion
        #endregion

        #region History
        #region Status
        private string statusHistory;
        public string StatusHistory
        {
            get => statusHistory;
            set
            {
                if (statusHistory != value)
                {
                    statusHistory = value;
                    onPropertyChange(nameof(StatusHistory));
                }
            }
        }
        #endregion

        #region Etykieta lewej kolumny 
        private string leftColumnName;
        public string LeftColumnName
        {
            get => leftColumnName;
            set
            {
                leftColumnName = value;
                onPropertyChange(nameof(LeftColumnName));
            }
        }
        #endregion

        #region Etykieta prawej kolumny  
        private string rightColumnName;
        public string RightColumnName
        {
            get => rightColumnName;
            set
            {
                rightColumnName = value;
                onPropertyChange(nameof(RightColumnName));
            }
        }
        #endregion

        #region Szerokość lewej kolumny  
        private string leftColumnWidth = "485";
        public string LeftColumnWidth
        {
            get => leftColumnWidth;
            set
            {
                leftColumnWidth = value;
                onPropertyChange(nameof(LeftColumnWidth));
            }
        }
        #endregion

        #region Szerokość prawej kolumny   
        private string rightColumnWidth = "485";
        public string RightColumnWidth
        {
            get => rightColumnWidth;
            set
            {
                rightColumnWidth = value;
                onPropertyChange(nameof(RightColumnWidth));
            }
        }

        #endregion

        #region Lista elementów w lewej kolumnie 
        private ObservableCollection<string> leftColumnList;
        public ObservableCollection<string> LeftColumnList
        {
            get
            {
                return leftColumnList;
            }
            set
            {
                leftColumnList = value;
                onPropertyChange(nameof(LeftColumnList));
            }
        }
        #endregion

        #region Lista elementów w prawej kolumnie  
        private ObservableCollection<string> rightColumnList;
        public ObservableCollection<string> RightColumnList
        {
            get
            {
                return rightColumnList;
            }
            set
            {
                rightColumnList = value;
                onPropertyChange(nameof(RightColumnList));
            }
        }
        #endregion

        #region Lista obiektów lewej i prawej kolumny 
        private List<HistoryModel> historyList = new List<HistoryModel>();
        public List<HistoryModel> HistoryList
        {
            get { return historyList; }
            set
            {
                historyList = value;
                onPropertyChange(nameof(HistoryList));
            }
        }
        #endregion
        #endregion

        #region Settings 
        #region Status
        private string settingsStatus;
        public string SettingsStatus
        {
            get => settingsStatus;
            set
            {
                settingsStatus = value;
                onPropertyChange(nameof(SettingsStatus));
            }
        }
        #endregion

        #region Nowy login TextBox
        private string loginChange;
        public string LoginChange
        {
            get => loginChange;
            set
            {
                loginChange = value;
                onPropertyChange(nameof(LoginChange));
            }
        }
        #endregion

        #region Nowe hasło TextBox
        private string passwordChange;
        public string PasswordChange
        {
            get => passwordChange;
            set
            {
                passwordChange = value;
                onPropertyChange(nameof(PasswordChange));
            }
        }
        #endregion
        #endregion
        #endregion

        #region Widoczność kontrolek
        #region Kontrolka LogIn
        private Visibility loginVisibility;
        public Visibility LoginVisibility
        {
            get { return loginVisibility; }
            private set
            {
                loginVisibility = value;
                onPropertyChange(nameof(LoginVisibility));
            }
        }

        private void LogIn()
        {
            StartVisibility = Visibility.Hidden;
            FormVisibility = Visibility.Hidden;
            DbFixVisibility = Visibility.Hidden;
            RecommendGamesVisibility = Visibility.Hidden;
            LoginVisibility = Visibility.Visible;
            SignInVisibility = Visibility.Hidden;
            HistoryVisibility = Visibility.Hidden;
            SettingsVisibility = Visibility.Hidden;
        }
        #endregion

        #region Kontrolka SignIn
        private Visibility signInVisibility;
        public Visibility SignInVisibility
        {
            get { return signInVisibility; }
            private set
            {
                signInVisibility = value;
                onPropertyChange(nameof(SignInVisibility));
            }
        }

        private void SignInVisibilitySet()
        {
            LoginVisibility = Visibility.Hidden;
            SignInVisibility = Visibility.Visible;
        }
        #endregion

        #region Kontrolka Start
        private Visibility startVisibility;
        public Visibility StartVisibility
        {
            get { return startVisibility; }
            private set
            {
                startVisibility = value;
                onPropertyChange(nameof(StartVisibility));
            }
        }

        private void Start()
        {
            StatusMenu = $"Witaj, {LoginTextBlock}!";

            StartVisibility = Visibility.Visible;
            FormVisibility = Visibility.Hidden;
            DbFixVisibility = Visibility.Hidden;
            RecommendGamesVisibility = Visibility.Hidden;
            LoginVisibility = Visibility.Hidden;
            SignInVisibility = Visibility.Hidden;
            HistoryVisibility = Visibility.Hidden;
            SettingsVisibility = Visibility.Hidden;
        }
        #endregion

        #region Kontrolka Formularza
        private Visibility formVisibility;
        public Visibility FormVisibility
        {
            get { return formVisibility; }
            private set
            {
                formVisibility = value;
                onPropertyChange(nameof(FormVisibility));
            }
        }

        private void FormControl()
        {
            StartVisibility = Visibility.Hidden;
            FormVisibility = Visibility.Visible;
        }
        #endregion

        #region Kontrolka filtrowania bazy danych
        private Visibility dbFixVisibility;
        public Visibility DbFixVisibility
        {
            get { return dbFixVisibility; }
            private set
            {
                dbFixVisibility = value;
                onPropertyChange(nameof(DbFixVisibility));
            }
        }

        private void DbFixControl()
        {
            StartVisibility = Visibility.Hidden;
            DbFixVisibility = Visibility.Visible;
            DbFixButtonVisibility = Visibility.Visible;
        }
        #endregion

        #region Kontrolka rekomendacji gier
        private Visibility recommendGamesVisibility;
        public Visibility RecommendGamesVisibility
        {
            get { return recommendGamesVisibility; }
            private set
            {
                recommendGamesVisibility = value;
                onPropertyChange(nameof(RecommendGamesVisibility));
            }
        }

        private void RecommendGamesControl()
        {
            StartVisibility = Visibility.Hidden;
            RecommendGamesVisibility = Visibility.Visible;
            RecommendButtonVisibility = Visibility.Visible;
        }
        #endregion

        #region Kontrolka historii wyszukiwań
        private Visibility historyVisibility;
        public Visibility HistoryVisibility
        {
            get { return historyVisibility; }
            private set
            {
                historyVisibility = value;
                onPropertyChange(nameof(HistoryVisibility));
            }
        }

        private void HistoryVisibilitySet()
        {
            StartVisibility = Visibility.Hidden;
            HistoryVisibility = Visibility.Visible;
        }
        #endregion

        #region Kontrolka ustawień
        private Visibility settingsVisibility;
        public Visibility SettingsVisibility
        {
            get { return settingsVisibility; }
            set
            {
                settingsVisibility = value;
                onPropertyChange(nameof(SettingsVisibility));
            }
        }

        private void SettingsVisibilitySet()
        {
            StartVisibility = Visibility.Hidden;
            SettingsVisibility = Visibility.Visible;
        }
        #endregion
        #endregion

        #region Obsługa Buttonów
        #region Kontrolka LogIn
        #region Zaloguj
        private ICommand zaloguj;
        public ICommand Zaloguj
        {
            get
            {
                return zaloguj ?? (zaloguj = new RelayCommand(LoggedIn, o => true));
            }
        }

        private void LoggedIn(object o)
        {
            if (LoginTextBlock == null || LoginTextBlock == "") StatusLogin = "Wpisz login.";
            else if (PasswordBlock == null || PasswordBlock == "") StatusLogin = "Wpisz hasło.";
            else
            {
                connection = new DBConnection();
                User user = new User("Name", LoginTextBlock.ToString(), PasswordBlock.ToString());

                if (connection.CheckUser(user)) StatusLogin = "Nieprawidłowy login.";
                else if (!connection.CheckPassword(user)) StatusLogin = "Nieprawidłowe hasło.";
                else
                {
                    if (!ButtonsEnabled) ButtonsEnabled = true;
                    Start();
                }
            }
        }
        #endregion

        #region Zarejestruj się
        private ICommand signIn;
        public ICommand SignIn
        {
            get
            {
                return signIn ?? (signIn = new RelayCommand(SignInControlShow, o => true));
            }
        }

        private void SignInControlShow(object o)
        {
            StatusSignIn = "Utwórz konto użytkownika.";
            NameTextBlock = string.Empty;
            SurnameTextBlock = string.Empty;
            LoginTextBlock2 = string.Empty;
            SigninPasswordTextBlock = string.Empty;

            SignInVisibilitySet();
        }
        #endregion

        #region Zamknięcie aplikacji
        private ICommand appClose;
        public ICommand AppClose
        {
            get 
            {
                if (preferences != null) preferences = null;
                
                return appClose ?? (appClose = new RelayCommand(o => App.Current.MainWindow.Close(), o => true)); 
            }
        }
        #endregion
        #endregion

        #region Kontrolka SignIn
        #region Dodaj użytkownika
        private ICommand addUser;
        public ICommand AddUser
        {
            get
            {
                return addUser ?? (addUser = new RelayCommand(UserAdd, o => true));
            }
        }

        private void UserAdd(object o)
        {
            if (NameTextBlock == null || NameTextBlock == "") StatusSignIn = "Pole 'Imię' nie może być puste.";
            else if (SurnameTextBlock == null || SurnameTextBlock == "") StatusSignIn = "Pole 'Nazwisko' nie może być puste.";
            else if (LoginTextBlock2 == null || LoginTextBlock2 == "") StatusSignIn = "Pole 'Login' nie może być puste.";
            else if (LoginTextBlock2.Length > 20) StatusSignIn = "Login nie może być dłuższy, niż 20 znaków";
            else if (SigninPasswordTextBlock == null || SigninPasswordTextBlock == "") StatusSignIn = "Pole 'Hasło' nie może być puste.";
            else if (SigninPasswordTextBlock.Length > 20) StatusSignIn = "Hasło nie może być dłuższe, niż 20 znaków";
            else
            {
                connection = new DBConnection();
                User user = new User(NameTextBlock.ToString() + "-" + SurnameTextBlock.ToString(), LoginTextBlock2.ToString(), SigninPasswordTextBlock.ToString());
                if (!connection.CheckUser(user)) StatusSignIn = "Użytkownik o podanym loginie już istnieje.";
                else
                {
                    connection.AddUser(user);
                    StatusSignIn = connection.message;
                }
            }
        }
        #endregion
        #endregion

        #region Kontrolka Start
        #region Nowe filtrowanie bazy danych
        private ICommand showDbFix;
        public ICommand ShowDbFix
        {
            get
            {
                return showDbFix ?? (showDbFix = new RelayCommand(DbFixView, o => true));
            }
        }

        private void DbFixView(object o)
        {
            FixSteam = new DatabaseFix();
            StatusDatabaseFix = FixSteam.message;
            DbFixControl();
        }
        #endregion

        #region Nowy formularz preferencji
        private ICommand newPrefs;
        public ICommand NewPrefs
        {
            get
            {
                return newPrefs ?? (newPrefs = new RelayCommand(OpenForm, o => true));
            }
        }

        private void OpenForm(object o)
        {
            Game = new Games();
            Game.LoadAttributes();
            ParametersLists();
            ClearWindow();
            FormControl();
        }

        private void NewForm()
        {
            GameDate = new Date(new DateTime(1998, 11, 8), new DateTime(2019, 4, 18), 0);
            GameTime = new Gametime(0, 100, 0);
            GamePrice = new Price(0, 100, 0);
            GameReviews = new Reviews(0, 100, 0);

            Categories = new ObservableCollection<Categories>();
            Developers = new ObservableCollection<Developer>();
            Genres = new ObservableCollection<Genres>();
            Platforms = new ObservableCollection<Platforms>();
            Publishers = new ObservableCollection<Publisher>();
            Tags = new ObservableCollection<Tags>();
        }
        #endregion

        #region Nowe rekomendacje
        private ICommand showRecommendGames;
        public ICommand ShowRecommendGames
        {
            get
            {
                return showRecommendGames ?? (showRecommendGames = new RelayCommand(RecommendGamesView, o => true));
            }
        }

        private void RecommendGamesView(object o)
        {
            GetRecommendGames = new Model.IRecommendGames();
            StatusIRecommendGames = GetRecommendGames.message;
            RecommendGamesControl();
        }
        #endregion

        #region Pokaż historię wyszukiwań
        private ICommand historyShow;
        public ICommand HistoryShow
        {
            get
            {
                return historyShow ?? (historyShow = new RelayCommand(HistoryControlShow, o => true));
            }
        }

        private void HistoryControlShow(object o)
        {
            StatusHistory = "Historia wyszukiwań.";
            LeftColumnName = "";
            RightColumnName = "";
            LeftColumnWidth = "485";
            RightColumnWidth = "485";
            LeftColumnList = new ObservableCollection<string>();
            RightColumnList = new ObservableCollection<string>();
            HistoryList = new List<HistoryModel>();

            HistoryVisibilitySet();
        }
        #endregion

        #region Pokaż ustawienia
        private ICommand showSettings;
        public ICommand ShowSettings
        {
            get
            {
                return showSettings ?? (showSettings = new RelayCommand(SettingsControlShow, o => true));
            }
        }

        private void SettingsControlShow(object o)
        {
            SettingsStatus = "Ustawienia.";
            LoginChange = LoginTextBlock;
            PasswordChange = PasswordBlock.ToString();

            SettingsVisibilitySet();
        }
        #endregion

        #region Wyloguj
        private ICommand backToLogin;
        public ICommand BackToLogin
        {
            get
            {
                return backToLogin ?? (backToLogin = new RelayCommand(LoginView, o => true));
            }
        }

        private void LoginView(object o)
        {
            StatusLogin = "Witaj w programie IRecommendGames.";
            LoginTextBlock = "";
            PasswordBlock = "";

            LogIn();
        }
        #endregion
        #endregion

        #region Kontrolka filtrowania bazy danych
        #region Rozpoczęcie filtrowania bazy danych
        private ICommand steamFix;
        public ICommand SteamFix
        {
            get
            {
                return steamFix ?? (steamFix = new RelayCommand(DatabaseFix, o => true));
            }
        }

        private void DatabaseFix(object o)
        {
            FixSteam = new DatabaseFix();
            FixSteam.DoFixDatabase();

            StatusDatabaseFix = FixSteam.message;
            DbFixButtonVisibility = Visibility.Hidden;
        }
        #endregion

        #region Widoczność przycisku Rozpocznij
        private Visibility dbFixButtonVisibility;
        public Visibility DbFixButtonVisibility
        {
            get { return dbFixButtonVisibility; }
            set
            {
                dbFixButtonVisibility = value;
                onPropertyChange(nameof(DbFixButtonVisibility));
            }
        }
        #endregion
        #endregion

        #region Kontrolka formularza preferencji
        #region Zapisanie preferencji do pliku
        private ICommand toFile;
        public ICommand ToFile
        {
            get
            {
                return toFile ?? (toFile = new RelayCommand(SavePrefs, o => true));
            }
        }

        private void SavePrefs(object o)
        {
            ObservableCollection<string> PreferencesToFile = new ObservableCollection<string>();
            connection = new DBConnection();
            preferences = string.Empty;

            foreach (string preference in PreferencesList)
            {
                if (preference.Substring(0, 4) == "Data")
                {
                    PreferencesToFile.Add(preference.Replace(".", "/"));
                    preferences += preference.Replace(".", "/") + " ";
                }
                else
                {
                    PreferencesToFile.Add(preference);
                    preferences += preference + " ";
                }
            }

            if (preferences != string.Empty)
            {
                connection.InsertPreference(preferences);
                Game.SavePrefs(PreferencesToFile);
                Status = "Preferencje zapisane pomyślnie.";
            }
            else Status = "Lista preferencji jest pusta, wprowadź preferencje.";
        }
        #endregion

        #region Wyczyszczenie listy preferencji
        private ICommand removePreferences;
        public ICommand RemovePreferences
        {
            get
            {
                return removePreferences ?? (removePreferences = new RelayCommand(ClearPreferences, o => true));
            }
        }

        private void ClearPreferences(object o)
        {
            ClearWindow();
            Status = "Preferencje wyczyszczone.";
        }
        #endregion

        #region Dodanie daty do listy preferencji
        private ICommand addDate;
        public ICommand AddDate
        {
            get
            {
                return addDate ?? (addDate = new RelayCommand(DateToList, o => true));
            }
        }

        private void DateToList(object o)
        {
            if (fromDatePicker > toDatePicker)
            {
                FromDatePicker = new DateTime(1998, 11, 8);
                ToDatePicker = new DateTime(2019, 4, 18);
                GameDateWeight = 0;

                Status = "Niepoprawnie wprowadzony przedział dat wydania gry.";
            }
            else
            {
                GameDateMessage();

                preferencesList.Add(GameDate.ReleaseDate);
                DateClickable = false;
                Status = "Przedział dat wydania gry dodany do listy.";
            }
        }
        #endregion

        #region Dodanie czasu gry do listy preferencji
        private ICommand addGametime;
        public ICommand AddGametime
        {
            get
            {
                return addGametime ?? (addGametime = new RelayCommand(GametimeToList, o => true));
            }
        }

        private void GametimeToList(object o)
        {
            bool proper = true;

            if (fromGametime < 0)
            {
                proper = false;
                fromGametime = 0;

                Status = "Czas gry musi być większy od 0.";
            }

            if (toGametime < 0)
            {
                proper = false;
                toGametime = 0;

                Status = "Czas gry musi być większy od 0.";
            }

            if (fromGametime > toGametime)
            {
                proper = false;
                fromGametime = 0;
                toGametime = 0;
                GameGametimeWeight = 0;

                Status = "Niepoprawnie wprowadzony czas gry.";
            }

            GameGametimeMessage();

            if (proper)
            {
                preferencesList.Add(GameTime.AverageGametime);
                GametimeClickable = false;
                Status = "Czas gry dodany do listy.";
            }
        }
        #endregion

        #region Dodanie ceny do listy preferencji
        private ICommand addPrice;
        public ICommand AddPrice
        {
            get
            {
                return addPrice ?? (addPrice = new RelayCommand(PriceToList, o => true));
            }
        }

        private void PriceToList(object o)
        {
            bool proper = true;

            if (fromPrice < 0)
            {
                proper = false;
                fromPrice = 0;

                Status = "Cena gry nie może być mniejsza od 0.";
            }

            if (toPrice < 0)
            {
                proper = false;
                toPrice = 0;

                Status = "Cena gry nie może być mniejsza od 0.";
            }

            if (fromPrice > toPrice)
            {
                proper = false;
                fromPrice = 0;
                toPrice = 0;
                GamePriceWeight = 0;

                Status = "Niepoprawnie wprowadzona cena gry.";
            }

            GamePriceMessage();

            if (proper)
            {
                preferencesList.Add(GamePrice.GamePrice);
                PriceClickable = false;
                Status = "Cena gry dodana do listy.";
            }
        }
        #endregion

        #region Dodanie pozytywnych recenzji do listy preferencji
        private ICommand addReviews;
        public ICommand AddReviews
        {
            get
            {
                return addReviews ?? (addReviews = new RelayCommand(ReviewsToList, o => true));
            }
        }

        private void ReviewsToList(object o)
        {
            bool proper = true;

            if (fromReviews < 0)
            {
                proper = false;
                fromReviews = 0;

                Status = "Procent pozytywnych recenzji nie może być mniejszy od 0.";
            }

            if (toReviews < 0)
            {
                proper = false;
                toReviews = 0;

                Status = "Procent pozytywnych recenzji nie może być mniejszy od 0.";
            }

            if (fromReviews > toReviews)
            {
                proper = false;
                fromReviews = 0;
                toReviews = 0;
                GameReviewsWeight = 0;

                Status = "Niepoprawnie wprowadzony procent pozytywnych recenzji.";
            }

            GameReviewsMessage();
                       
            if (proper)
            {
                preferencesList.Add(GameReviews.PositiveReviews);
                ReviewsClickable = false;
                Status = "Procent pozytywnych recenzji dodany do listy.";
            }
        }
        #endregion

        #region Dodanie kategorii do listy preferencji
        private ICommand addCategory;
        public ICommand AddCategory
        {
            get
            {
                return addCategory ?? (addCategory = new RelayCommand(CategoryToList, o => true));
            }
        }

        private void CategoryToList(object o)
        {
            bool isAlready = false, proper = true;

            if (category == null || category == "")
            {
                proper = false;
                GameCategory = null;
                GameCategoryWeight = 0;
                Status = "Wybierz kategorię gry z listy.";
            }
            else if (categoryWeight == null)
            {
                proper = false;
                GameCategory = null;
                GameCategoryWeight = 0;
                Status = "Ustaw wagę dla kategorii.";
            }
            else
            {
                GameCategoryMessage(category, categoryWeight);

                foreach (string preference in preferencesList)
                {
                    if (preference.Length < gameCategoryMessage.Remove(gameCategoryMessage.Length - 3).Length) continue;

                    if (IsAlready(preference, gameCategoryMessage))
                    {
                        isAlready = true;
                    }
                }

                if (isAlready)
                {
                    Status = $"Kategoria {category} znajduje się już na liście.";
                }

                if (!isAlready & proper)
                {
                    preferencesList.Add(gameCategoryMessage);
                    categoryClicked = false;
                    Status = $"Kategoria {category} dodana do listy.";
                }
            }
        }
        #endregion

        #region Dodanie dewelopera do listy preferencji
        private ICommand addDeveloper;
        public ICommand AddDeveloper
        {
            get
            {
                return addDeveloper ?? (addDeveloper = new RelayCommand(DeveloperToList, o => true));
            }
        }

        private void DeveloperToList(object o)
        {
            bool isAlready = false, proper = true;

            if (developer == null || developer == "")
            {
                proper = false;
                GameDeveloper = null;
                GameDeveloperWeight = 0;
                Status = "Wybierz dewelopera gry z listy.";
            }
            else if (developerWeight == null)
            {
                proper = false;
                GameDeveloper = null;
                GameDeveloperWeight = 0;
                Status = "Ustaw wagę dla dewelopera.";
            }
            else
            {
                GameDeveloperMessage(developer, developerWeight);

                foreach (string preference in preferencesList)
                {
                    if (preference.Length < gameDeveloperMessage.Remove(gameDeveloperMessage.Length - 3).Length) continue;

                    if (IsAlready(preference, gameDeveloperMessage))
                    {
                        isAlready = true;
                    }
                }

                if (isAlready)
                {
                    Status = $"Deweloper {developer} znajduje się już na liście.";
                }

                if (!isAlready & proper)
                {
                    preferencesList.Add(gameDeveloperMessage);
                    developerClicked = false;
                    Status = $"Deweloper {developer} dodany do listy.";
                }
            }
        }
        #endregion

        #region Dodanie gatunku do listy preferencji
        private ICommand addGenre;
        public ICommand AddGenre
        {
            get
            {
                return addGenre ?? (addGenre = new RelayCommand(GenreToList, o => true));
            }
        }

        private void GenreToList(object o)
        {
            bool isAlready = false, proper = true;

            if (genre == null || genre == "")
            {
                proper = false;
                GameGenres = null;
                GameGenreWeight = 0;
                Status = "Wybierz gatunek gry z listy.";
            }
            else if (genreWeight == null)
            {
                proper = false;
                GameGenres = null;
                GameGenreWeight = 0;
                Status = "Ustaw wagę dla gatunku.";
            }
            else
            {
                GameGenreMessage(genre, genreWeight);

                foreach (string preference in preferencesList)
                {
                    if (preference.Length < gameGenreMessage.Remove(gameGenreMessage.Length - 3).Length) continue;

                    if (IsAlready(preference, gameGenreMessage))
                    {
                        isAlready = true;
                    }
                }

                if (isAlready)
                {
                    Status = $"Gatunek {genre} znajduje się już na liście.";
                }

                if (!isAlready & proper)
                {
                    preferencesList.Add(gameGenreMessage);
                    genreClicked = false;
                    Status = $"Gatunek {genre} dodany do listy.";
                }
            }
        }
        #endregion

        #region Dodanie platformy do listy preferencji
        private ICommand addPlatform;
        public ICommand AddPlatform
        {
            get
            {
                return addPlatform ?? (addPlatform = new RelayCommand(PlatformToList, o => true));
            }
        }

        private void PlatformToList(object o)
        {
            bool isAlready = false, proper = true;

            if (platform == null || platform == "")
            {
                proper = false;
                GamePlatform = null;
                GamePlatformWeight = 0;
                Status = "Wybierz platformę gry z listy.";
            }
            else if (platformWeight == null)
            {
                proper = false;
                GamePlatform = null;
                GamePlatformWeight = 0;
                Status = "Ustaw wagę dla platformy.";
            }
            else
            {
                GamePlatformMessage(platform, platformWeight);

                foreach (string preference in preferencesList)
                {
                    if (preference == null) break;

                    if (preference.Length < gamePlatformMessage.Remove(gamePlatformMessage.Length - 3).Length) continue;

                    if (IsAlready(preference, gamePlatformMessage))
                    {
                        isAlready = true;
                    }
                }

                if (isAlready)
                {
                    Status = $"Platforma {platform} znajduje się już na liście.";
                }

                if (!isAlready & proper)
                {
                    preferencesList.Add(gamePlatformMessage);
                    platformClicked = false;
                    Status = $"Platforma {platform} dodana do listy.";
                }
            }
        }
        #endregion

        #region Dodanie wydawcy do listy preferencji
        private ICommand addPublisher;
        public ICommand AddPublisher
        {
            get
            {
                return addPublisher ?? (addPublisher = new RelayCommand(PublisherToList, o => true));
            }
        }

        private void PublisherToList(object o)
        {
            bool isAlready = false, proper = true;

            if (publisher == null || publisher == "")
            {
                proper = false;
                GamePublisher = null;
                GamePublisherWeight = 0;
                Status = "Wybierz wydawcę gry z listy.";
            }
            else if (publisherWeight == null)
            {
                proper = false;
                GamePublisher = null;
                GamePublisherWeight = 0;
                Status = "Ustaw wagę dla wydawcy.";
            }
            else
            {
                GamePublisherMessage(publisher, publisherWeight);

                foreach (string preference in preferencesList)
                {
                    if (preference.Length < gamePublisherMessage.Remove(gamePublisherMessage.Length - 3).Length) continue;

                    if (IsAlready(preference, gamePublisherMessage))
                    {
                        isAlready = true;
                    }
                }

                if (isAlready)
                {
                    Status = $"Wydawca {publisher} znajduje się już na liście.";
                }

                if (!isAlready & proper)
                {
                    preferencesList.Add(gamePublisherMessage);
                    publisherClicked = false;
                    Status = $"Wydawca {publisher} dodany do listy.";
                }
            }
        }
        #endregion

        #region Dodanie tagu do listy preferencji
        private ICommand addTag;
        public ICommand AddTag
        {
            get
            {
                return addTag ?? (addTag = new RelayCommand(TagToList, o => true));
            }
        }

        private void TagToList(object o)
        {
            bool isAlready = false, proper = true;

            if (tag == null || tag == "")
            {
                proper = false;
                GameTags = null;
                GameTagsWeight = 0;
                Status = "Wybierz tag gry z listy.";
            }
            else if (tagWeight == null)
            {
                proper = false;
                GameTags = null;
                GameTagsWeight = 0;
                Status = "Ustaw wagę dla tagu gry.";
            }
            else
            {
                GameTagMessage(tag, tagWeight);

                foreach (string preference in preferencesList)
                {
                    if (preference.Length < gameTagMessage.Remove(gameTagMessage.Length - 3).Length) continue;

                    if (IsAlready(preference, gameTagMessage))
                    {
                        isAlready = true;
                    }
                }

                if (isAlready)
                {
                    Status = $"Tag {tag} znajduje się już na liście.";
                }

                if (!isAlready & proper)
                {
                    preferencesList.Add(gameTagMessage);
                    tagClicked = false;
                    Status = $"Tag {tag} dodany do listy.";
                }
            }
        }
        #endregion

        // Funkcja pomocnicza sprawdzająca, czy konkretna wartość znajduje się już na liście
        private bool IsAlready(string preference, string newPreference)
        {
            if (preference.Substring(0, preference.Length - 6) == newPreference.Substring(0, newPreference.Length - 6) ||
                preference.Substring(0, preference.Length - 6) == newPreference.Substring(0, newPreference.Length - 7) ||
                preference.Substring(0, preference.Length - 6) == newPreference.Substring(0, newPreference.Length - 8) ||
                preference.Substring(0, preference.Length - 7) == newPreference.Substring(0, newPreference.Length - 6) ||
                preference.Substring(0, preference.Length - 7) == newPreference.Substring(0, newPreference.Length - 7) ||
                preference.Substring(0, preference.Length - 7) == newPreference.Substring(0, newPreference.Length - 8) ||
                preference.Substring(0, preference.Length - 8) == newPreference.Substring(0, newPreference.Length - 6) ||
                preference.Substring(0, preference.Length - 8) == newPreference.Substring(0, newPreference.Length - 7) ||
                preference.Substring(0, preference.Length - 8) == newPreference.Substring(0, newPreference.Length - 8))
            {
                return true;
            }
            else return false;
        }
        #endregion

        #region Kontrolka przygotowania rekomendacji
        #region Rozpoczęcie przygotowania listy preferencji
        private ICommand recommendGames;
        public ICommand RecommendGames
        {
            get
            {
                return recommendGames ?? (recommendGames = new RelayCommand(IRecommend, o => true));
            }
        }

        private void IRecommend(object o)
        {
            if (preferences == null) StatusIRecommendGames = "Nie wybrano preferencji. \nWróć do menu i wciśnij przycisk 'Otwórz formularz'.";
            else
            {
                int userId;
                int inputId;
                int outputId;
                int over;
                int under;

                GetRecommendGames = new Model.IRecommendGames();
                connection = new DBConnection();

                GetRecommendGames.GetRecommendations();

                if (GetRecommendGames.Text.Count > 1)
                {
                    userId = connection.GetUserId(LoginTextBlock);
                    inputId = connection.GetInputId(preferences);
                    outputId = connection.GetOutputId();

                    for (int i = 1; i < GetRecommendGames.Text.Count; i++)
                    {
                        if (int.TryParse(GetRecommendGames.Text[i].Substring(0, 2), out over))
                        {
                            connection.InsertOutput(GetRecommendGames.Text[i].Substring(4).Replace("'", " "));
                        }
                        else if (int.TryParse(GetRecommendGames.Text[i].Substring(0, 1), out under))
                        {
                            connection.InsertOutput(GetRecommendGames.Text[i].Substring(3).Replace("'", " "));
                        }

                        connection.InsertUserSearch(userId, inputId, outputId);
                        outputId += 1;
                    }

                    StatusIRecommendGames = GetRecommendGames.message;
                    RecommendButtonVisibility = Visibility.Hidden;
                }
                else StatusIRecommendGames = "Nie znaleziono gier pasujących do wprowadzonych danych.";
            }
        }
        #endregion

        #region Widoczność przycisku Rozpocznij
        private Visibility recommendButtonVisibility;
        public Visibility RecommendButtonVisibility
        {
            get { return recommendButtonVisibility; }
            set
            {
                recommendButtonVisibility = value;
                onPropertyChange(nameof(RecommendButtonVisibility));
            }
        }
        #endregion
        #endregion

        #region Kontrolka historii wyszukiwań
        #region Historia użytkownika
        private ICommand myHistory;
        public ICommand MyHistory
        {
            get
            {
                return myHistory ?? (myHistory = new RelayCommand(MyHistoryShow, o => true));
            }
        }

        private void MyHistoryShow(object o)
        {
            connection = new DBConnection();

            int userId = connection.GetUserId(LoginTextBlock);
            List<string> myHistory = connection.GetMyHistory(userId);

            LeftColumnList = new ObservableCollection<string>();
            RightColumnList = new ObservableCollection<string>();
            HistoryList = new List<HistoryModel>();

            if (myHistory.Count != 0)
            {
                StatusHistory = $"Historia wyszukiwań użytkownika {LoginTextBlock}.";
                LeftColumnName = "Wybrane preferencje";
                RightColumnName = "Znalezione tytuły";
                LeftColumnWidth = "685";
                RightColumnWidth = "285";

                foreach (string gameSearch in myHistory)
                {
                    LeftColumnList.Add(gameSearch.Split('|')[0]);
                    RightColumnList.Add(gameSearch.Split('|')[1]);
                }

                for (int i = 0; i < LeftColumnList.Count; i++)
                {
                    HistoryList.Add(new HistoryModel(LeftColumnList[i], RightColumnList[i]));
                }
            }
            else StatusHistory = "Brak historii wyszukiwań. \nUruchom formularz preferencji.";
        }
        #endregion

        #region Statystyki wszystkich użytkowników
        private ICommand globalStats;
        public ICommand GlobalStats
        {
            get
            {
                return globalStats ?? (globalStats = new RelayCommand(GlobalStatsShow, o => true));
            }
        }

        private void GlobalStatsShow(object o)
        {
            connection = new DBConnection();

            List<string> globalStats = connection.GetGlobalStats();

            LeftColumnList = new ObservableCollection<string>();
            RightColumnList = new ObservableCollection<string>();
            HistoryList = new List<HistoryModel>();

            if (globalStats.Count != 0)
            {
                StatusHistory = "Ilość powtórzeń tytułów wśród wszystkich użytkowników.";
                LeftColumnName = "Ilość powtórzeń";
                RightColumnName = "Tytuł";
                LeftColumnWidth = "185";
                RightColumnWidth = "785";

                foreach (string gameSearch in globalStats)
                {
                    LeftColumnList.Add(gameSearch.Split('|')[0]);
                    RightColumnList.Add(gameSearch.Split('|')[1]);
                }

                for (int i = 0; i < LeftColumnList.Count; i++)
                {
                    HistoryList.Add(new HistoryModel(LeftColumnList[i], RightColumnList[i]));
                }
            }
            else StatusHistory = "Brak historii wyszukiwań. \nUruchom formularz preferencji.";
        }
        #endregion
        #endregion

        #region Kontrolka ustawień
        #region Zmiana loginu
        private ICommand changeLogin;
        public ICommand ChangeLogin
        {
            get
            {
                return changeLogin ?? (changeLogin = new RelayCommand(DoChangeLogin, o => true));
            }
        }

        private void DoChangeLogin(object o)
        {
            if (LoginChange == null) SettingsStatus = "Pole 'Login' nie może być puste.";
            else if (LoginChange.Length > 20) SettingsStatus = "Login nie może być dłuższy, niż 20 znaków";
            else if (LoginChange == LoginTextBlock) SettingsStatus = "Wprowadź zmianę loginu.";
            else
            {
                connection = new DBConnection();
                int userId = connection.GetUserId(LoginTextBlock);

                connection.ChangeLogin(LoginChange, userId);
                LoginTextBlock = LoginChange;

                SettingsStatus = "Login został zmieniony.";
            }
        }
        #endregion

        #region Zmiana hasła
        private ICommand changePassword;
        public ICommand ChangePassword
        {
            get
            {
                return changePassword ?? (changePassword = new RelayCommand(DoChangePassword, o => true));
            }
        }

        private void DoChangePassword(object o)
        {
            if (PasswordChange == null) SettingsStatus = "Pole 'Hasło' nie może być puste.";
            else if (PasswordChange.Length > 20) SettingsStatus = "Hasło nie może być dłuższe, niż 20 znaków";
            else if (PasswordChange == PasswordBlock) SettingsStatus = "Wprowadź zmianę hasła.";
            else
            {
                connection = new DBConnection();
                int userId = connection.GetUserId(LoginTextBlock);

                connection.ChangePassword(PasswordChange, userId);
                PasswordBlock = PasswordChange;

                SettingsStatus = "Hasło zostało zmienione.";
            }
        }
        #endregion

        #region Usunięcie historii wyszukiwań użytkownika
        private ICommand deleteHistory;
        public ICommand DeleteHistory
        {
            get
            {
                return deleteHistory ?? (deleteHistory = new RelayCommand(DeleteMyHistory, o => true));
            }
        }

        private void DeleteMyHistory(object o)
        {
            connection = new DBConnection();
            int userId = connection.GetUserId(LoginTextBlock);
            List<int> myInputIds = connection.GetInputId(userId);

            if (myInputIds.Count != 0)
            {
                foreach (int inputId in myInputIds)
                {
                    connection.DeleteHistory(inputId);
                }

                SettingsStatus = "Historia wyszukiwań wyczyszczona.";
            }
            else SettingsStatus = "Brak historii wyszukiwań.";
        }
        #endregion

        #region Usunięcie konta użytkownika
        private ICommand deleteAccount;
        public ICommand DeleteAccount
        {
            get
            {
                return deleteAccount ?? (deleteAccount = new RelayCommand(DeleteMyAccount, o => true));
            }
        }

        private void DeleteMyAccount(object o)
        {
            connection = new DBConnection();
            int userId = connection.GetUserId(LoginTextBlock);

            connection.DeleteAccount(userId);
            ButtonsEnabled = false;
            SettingsStatus = "Konto zostało usunięte.";
        }

        private bool buttonsEnabled = true;
        public bool ButtonsEnabled
        {
            get { return buttonsEnabled; }
            set
            {
                buttonsEnabled = value;
                onPropertyChange(nameof(ButtonsEnabled));
            }
        }
        #endregion
        #endregion

        #region Powrót do menu
        private ICommand back;
        public ICommand Back
        {
            get
            {
                return back ?? (back = new RelayCommand(BackToMenu, o => true));
            }
        }

        private void BackToMenu(object o) { Start(); }
        #endregion
        #endregion
    }
}