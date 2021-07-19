using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Histogram;

namespace OnlineShopWebApp
{
    public static class MeasureOptions
    {
        public static CounterOptions MainPageRequestCounterOptions
        {
            get
            {
                return new CounterOptions
                {
                    Name = "Main_Page_Requests",
                    MeasurementUnit = Unit.Requests
                };
            }
        }

        public static HistogramOptions ProductsInFavoritesHistogramOptions
        {
            get
            {
                return new HistogramOptions
                {
                    Name = "Products_in_favorites",
                    MeasurementUnit = Unit.Items
                };
            }
        }
    }
}
