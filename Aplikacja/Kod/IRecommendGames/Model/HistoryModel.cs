namespace IRecommendGames.Model
{
    class HistoryModel
    {
        public string LeftColumn { get; set; }
        public string RightColumn { get; set; }

        public HistoryModel(string leftColumn, string rightColumn) { LeftColumn = leftColumn; RightColumn = rightColumn; }
    }
}