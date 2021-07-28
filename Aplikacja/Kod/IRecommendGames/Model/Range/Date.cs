using System;

namespace IRecommendGames.Model.Range
{
    class Date
    {
        public string ReleaseDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int DateWeight { get; set; }

        public Date(DateTime fromDate, DateTime toDate, int dateWeight)
        {
            ReleaseDate = "Data:x-y;Waga:z";
            FromDate = fromDate;
            ToDate = toDate;
            DateWeight = dateWeight;
        }
    }
}