namespace AnalyticsAdapter
{
    public class CustomerOverview
    {
        public string Name { get; set; }

        public int TotalProductsPurchased { get; set; }

        // return product name maximum number of purchases for a single product
        public string FavoriteProductName { get; set; }

        // max amount of money spent for a single product
        public decimal MaxAmountSpentPerProducts { get; set; }

        public decimal TotalMoneySpent { get; set; }
    }
}
