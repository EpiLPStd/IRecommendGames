namespace IRecommendGames.Model.Range
{
    class Price
    {
        public string GamePrice { get; set; }
        public float FromPrice { get; set; }
        public float ToPrice { get; set; }
        public int PriceWeight { get; set; }

        public Price(float fromPrice, float toPrice, int priceWeight)
        {
            GamePrice = "Cena:x-y;Waga:z";
            FromPrice = fromPrice;
            ToPrice = toPrice;
            PriceWeight = priceWeight;
        }
    }
}