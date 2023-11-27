using System;
using System.Collections.Generic;

public class StockIn
{
    public List<Result>? Results { get; set; }
    public DateTime? RequestedAt { get; set; }
    public string? Took { get; set; }
}
public class Result
{
    public string? Symbol { get; set; }
    public string? ShortName { get; set; }
    public double? RegularMarketPrice { get; set; }
    public string? Logourl { get; set; }
    public decimal? MarketCap { get; set; }
    public decimal RegularMarketVolume { get; set; }
    // Include other properties here based on your needs

    public List<HistoricalDataPrice>? HistoricalDataPrice { get; set; }
    public DividendsData? dividendsData { get; set; }
    public BalanceSheetStatements? BalanceSheetHistory { get; set; }
    public SummaryProfile? SummaryProfile { get; set; }
    public FinancialData? FinancialData { get; set; }
}

public class DividendsData { 
    public List<CashDividends>? cashDividends { get; set; }
    public List<StockDividends>? stockDividends { get; set; }
}

public class CashDividends
{
    public string? AssetIssued { get; set; }
    public string? PaymentDate { get; set; }
    public double? Rate { get; set; }
    public string? RelatedTo { get; set; }
}

public class StockDividends {
    public string? assetIssued { get; set; }
    public decimal? factor { get; set; }
    public string? approvedOn { get; set; }
    public string? isinCode { get; set; }
    public string? label { get; set; }
    public string? lastDatePrior { get; set; }
    public string? remarks { get; set; }
}


public class HistoricalDataPrice
{
    public decimal? Date { get; set; }
    public double? Open { get; set; }
    public double? High { get; set; }
    public double? Low { get; set; }
    public double? Close { get; set; }
    public decimal? Volume { get; set; }
    public double? adjustedClose { get; set; }
}

public class BalanceSheetStatements
{
    public string? EndDate { get; set; }
    public decimal? Cash { get; set; }
    public decimal? ShortTermInvestments { get; set; }
    public decimal? NetReceivables { get; set; }
    public decimal? Inventory { get; set; }
    public decimal? OtherCurrentAssets { get; set; }
    public decimal? TotalCurrentAssets { get; set; }
    public decimal? LongTermInvestments { get; set; }
    public decimal? PropertyPlantEquipment { get; set; }
    public decimal? GoodWill { get; set; }
    public decimal? IntangibleAssets { get; set; }
    public decimal? OtherAssets { get; set; }
    public decimal? DeferredLongTermAssetCharges { get; set; }
    public decimal? TotalAssets { get; set; }
    public decimal? AccountsPayable { get; set; }
    public decimal? ShortLongTermDebt { get; set; }
    public decimal? OtherCurrentLiabilities { get; set; }
    public decimal? LongTermDebt { get; set; }
    public decimal? OtherLiabilities { get; set; }
    public decimal? MinorityInterest { get; set; }
    public decimal? TotalCurrentLiabilities { get; set; }
    public decimal? TotalLiabilities { get; set; }
    public decimal? CommonStock { get; set; }
    public decimal? RetainedEarnings { get; set; }
    public decimal? TreasuryStock { get; set; }
    public decimal? OtherStockholderEquity { get; set; }
    public decimal? TotalStockholderEquity { get; set; }
    public decimal? NetTangibleAssets { get; set; }
}

public class SummaryProfile
{
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public string? Industry { get; set; }
    public string? IndustryKey { get; set; }
    public string? IndustryDisp { get; set; }
    public string? Sector { get; set; }
    public string? SectorKey { get; set; }
    public string? SectorDisp { get; set; }
    public string? LongBusinessSummary { get; set; }
    public decimal? FullTimeEmployees { get; set; }
}

public class FinancialData
{
    public double? CurrentPrice { get; set; }
    public double? TargetHighPrice { get; set; }
    public double? TargetLowPrice { get; set; }
    public double? TargetMeanPrice { get; set; }
    public double? TargetMedianPrice { get; set; }
    public double? RecommendationMean { get; set; }
    public string? RecommendationKey { get; set; }
    public decimal? NumberOfAnalystOpinions { get; set; }
    public decimal? TotalCash { get; set; }
    public double? TotalCashPerShare { get; set; }
    public decimal? Ebitda { get; set; }
    public decimal? TotalDebt { get; set; }
    public double? QuickRatio { get; set; }
    public double? CurrentRatio { get; set; }
    public decimal? TotalRevenue { get; set; }
    public double? DebtToEquity { get; set; }
    public double? RevenuePerShare { get; set; }
    public double? ReturnOnAssets { get; set; }
    public double? ReturnOnEquity { get; set; }
    public decimal? GrossProfits { get; set; }
    public decimal? FreeCashflow { get; set; }
    public decimal? OperatingCashflow { get; set; }
    public double? EarningsGrowth { get; set; }
    public double? RevenueGrowth { get; set; }
    public double? GrossMargins { get; set; }
    public double? EbitdaMargins { get; set; }
    public double? OperatingMargins { get; set; }
    public double? ProfitMargins { get; set; }
    public string? FinancialCurrency { get; set; }
}

