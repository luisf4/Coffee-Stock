public class ChartDataIn
{
    public long Date { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public int Volume { get; set; }
    public double AdjustedClose { get; set; }
    public string requestedAt { get; set; }



    public ChartDataIn(long Date, double Open, double High, double Low, double Close, int Volume, double AdjustedClose, string requestedAt)
    {
        this.Date = Date;
        this.Open = Open;
        this.High = High;
        this.Low = Low;
        this.Close = Close;
        this.Volume = Volume;
        this.AdjustedClose = AdjustedClose;
        this.requestedAt = requestedAt;
    }
}

