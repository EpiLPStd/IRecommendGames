namespace IRecommendGames.Model.Range
{
    class Gametime
    {
        public string AverageGametime { get; set; }
        public int FromHours { get; set; }
        public int ToHours { get; set; }
        public int AverageGametimeWeight { get; set; }

        public Gametime(int fromHours, int toHours, int averageGametimeWeight)
        {
            AverageGametime = "Sredni_czas_gry:x-y;Waga:z";
            FromHours = fromHours;
            ToHours = toHours;
            AverageGametimeWeight = averageGametimeWeight;
        }
    }
}