// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Sharts
    {
        public double v { get; set; }
        public double vw { get; set; }
        public double o { get; set; }
        public double c { get; set; }
        public double h { get; set; }
        public double l { get; set; }
        public object t { get; set; }
        public int n { get; set; }
    }

    public class StockSharts
    {
        public string ticker { get; set; }
        public int queryCount { get; set; }
        public int resultsCount { get; set; }
        public bool adjusted { get; set; }
        public List<Sharts> sharts { get; set; }
        public string status { get; set; }
        public string request_id { get; set; }
        public int count { get; set; }
        public string next_url { get; set; }
    }