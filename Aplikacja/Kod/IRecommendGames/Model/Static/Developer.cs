namespace IRecommendGames.Model.Static
{
    class Developer
    {
        private int id;
        private string gameDeveloper;

        public int Id 
        {
            get { return id; } 
            set { id = value; } 
        }

        public string GameDeveloper 
        { 
            get { return gameDeveloper; } 
            set { gameDeveloper = value; } 
        }
    }
}