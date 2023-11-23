USE master
DROP DATABASE coffeeStock

-- Create the database
CREATE DATABASE coffeeStock;
GO
-- Use the "coffeeStock" database
USE coffeeStock;
GO

-- Users table
CREATE TABLE users (
    user_id INT PRIMARY KEY IDENTITY,
    username VARCHAR(50) NOT NULL,
    passwordHash VARCHAR(100) NOT NULL,
    jwt NVARCHAR(MAX) NOT NULL,
);
GO

CREATE TABLE stocks ( 
    stock_id INT PRIMARY KEY IDENTITY,
    symbol VARCHAR(50) NOT NULL, 
    [name] VARCHAR(50) NOT NULL,
    price FlOAT NOT NULL, 
    logo VARCHAR(255) NOT NULL,
    requestedAt varchar(50),
	marketcap decimal,
	regularMarketVolume decimal,
	industry varchar(255),
	sector varchar(255),
	longBusinessSummary varchar(max),
	fullTimeEmployees decimal,
	[address] varchar(255),
	city varchar(255),
	country varchar(255)
);
GO
	
select * from stocks

CREATE TABLE chartData (
    chart_id INT PRIMARY KEY IDENTITY,
    symbol VARCHAR(50),
    [date] int,
    [open] FLOAT,
    [high] FLOAT,
    [low] FLOAT,
    [close] FLOAT,
    volume INT,
    adjustedClose FLOAT,
    requestedAt VARCHAR(50),
);
GO

select * from chartData

CREATE TABLE chartDividendsData (
    chart_id INT PRIMARY KEY IDENTITY,
    symbol VARCHAR(50),
	paymentDate VARCHAR(255),
    rate FLOAT,
    relatedTo VARCHAR(255),
    requestedAt VARCHAR(50),
);

select * from chartDividendsData
-- Portfolios table
CREATE TABLE portfolios (
    portfolio_id INT PRIMARY KEY IDENTITY,
    user_name varchar(255) NOT NULL,
    [name] VARCHAR(100) NOT NULL,
);
GO
select * from portfolios where user_name = '1'

-- PortfolioStocks table to establish relationships
CREATE TABLE stocks_portfolio (
    portfolio_stock_id INT PRIMARY KEY IDENTITY,
    portfolio_id INT NOT NULL,
    symbol VARCHAR(50) NOT NULL,
    [name] VARCHAR(50) NOT NULL,
    qtd int NOT NULL,
    price FLOAT NOT NULL,
    logo VARCHAR(255) NOT NULL,
    [date] VARCHAR(50),
    FOREIGN KEY (portfolio_id) REFERENCES portfolios(portfolio_id),
);
GO


-- INSERTS stocks
--insert into stocks (symbol,[name],price,logo,requestedAt) values('petr4', 'petobras', 35.00, 'aaaa','2023-11-07');

-- INSERT charData
--insert into chartData (chart_stock_id,[date],[open],[high],[low],[close],volume,[adjustedClose]) values (1,'2023-11-07',12.1,13.2,11.25,12.0,3432.1231,32.0)

-- INSERT portfolio
--insert into portfolios ([name],user_id) values('portfolio1', 1)

-- INSERT stocks_portfolio
--insert into stocks_portfolio (portfolio_id,symbol,[name],qtd,price,logo,[date]) values (1,'aapl', 'apple', 2, 10.00, 'aaaaaaaa', '2023-11-07')

-- SELECTS 
--select * from users

--select * from stocks where symbol = 'petr4';

--select * from chartData

--select * from portfolios

--select * from stocks_portfolio


-- UPDATES
-- Update all values in the "users" table
--UPDATE users SET username = 'NewUserName';

-- Update all values in the "stocks" table
--UPDATE stocks SET price = 50.00;

-- Update all values in the "chartData" table
--UPDATE chartData SET [open] = 14.0, [high] = 15.0, [low] = 13.0, [close] = 14.5, volume = 5000.0, [adjustedClose] = 35.0;

-- Update all values in the "portfolios" table
--UPDATE portfolios SET [name] = 'NewPortfolioName';

-- Update all values in the "stocks_portfolio" table
--UPDATE stocks_portfolio SET qtd = 4, price = 20.00, [date] = '2023-11-08';


-- DELETES
-- Delete a user
--DELETE FROM users WHERE user_id = 1;

-- Delete a stock
--DELETE FROM stocks WHERE symbol = 'petr4';

-- Delete chart data
--DELETE FROM chartData WHERE chart_stock_id = 1 AND [date] = '2023-11-07';

-- Delete a portfolio
--DELETE FROM portfolios WHERE portfolio_id = 1;

-- Delete a stock from a portfolio
--DELETE FROM stocks_portfolio WHERE portfolio_id = 1 AND symbol = 'aapl';
