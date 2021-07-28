namespace IRecommendGames.Model.Range
{
    class Reviews
    {
        public string PositiveReviews { get; set; }
        public int FromPercantage { get; set; }
        public int ToPercantage { get; set; }
        public int PositiveReviewsPercentageWeight { get; set; }

        public Reviews(int fromPercentage, int toPercentage, int positiveReviewsPercentageWeight)
        {
            PositiveReviews = "%_Pozytywnych_recenzji:x-y;Waga:z";
            FromPercantage = fromPercentage;
            ToPercantage = toPercentage;
            PositiveReviewsPercentageWeight = positiveReviewsPercentageWeight;
        }
    }
}