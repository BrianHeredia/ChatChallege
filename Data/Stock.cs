namespace ChatChallenge.Data
{
    public class Stock
    {
        public Stock()
        {
            Symbol = "";
        }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        
    }
}
