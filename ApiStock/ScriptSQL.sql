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
    jwt NVARCHAR(MAX) NOT NULL
);
GO

CREATE TABLE stocks ( 
    stock_id INT PRIMARY KEY IDENTITY,
    symbol VARCHAR(50) NOT NULL, 
    name VARCHAR(50) NOT NULL,
    price FLOAT NOT NULL, 
    logo VARCHAR(255) NOT NULL,
    requestedAt DATE  ,
);
GO

CREATE TABLE chartData (
    chart_id INT PRIMARY KEY IDENTITY,
    chart_stock_id INT not null,
    [date] DATE,
    [open] FLOAT,
    [high] FLOAT,
    [low] FLOAT,
    [close] FLOAT,
    volume FLOAT,
    adjustedClose FLOAT,
    FOREIGN KEY (chart_stock_id) REFERENCES stocks(stock_id)
)

-- Portfolios table
CREATE TABLE portfolios (
    portfolio_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL,
    [name] VARCHAR(100) NOT NULL,
);
GO

-- PortfolioStocks table to establish relationships
CREATE TABLE stocks_portfolio (
    portfolio_stock_id INT PRIMARY KEY IDENTITY,
    portfolio_id INT NOT NULL,
    symbol VARCHAR(50) NOT NULL,
    [name] VARCHAR(50) NOT NULL,
    qtd int NOT NULL,
    price FLOAT NOT NULL,
    logo VARCHAR(255) NOT NULL,
    [date] Date,
    FOREIGN KEY (portfolio_id) REFERENCES portfolios(portfolio_id),
  --  FOREIGN KEY (symbol) REFERENCES stocks(symbol),
  --  FOREIGN KEY ([name]) REFERENCES stocks(name),
  --  FOREIGN KEY (logo) REFERENCES stocks(logo)
);
GO


-- INSERTS stocks
insert into stocks (symbol,[name],price,logo,requestedAt) values('petr4', 'petobras', 35.00, 'aaaa','2023-11-07');

-- INSERT charData
insert into chartData (chart_stock_id,[date],[open],[high],[low],[close],volume,[adjustedClose]) values (1,'2023-11-07',12.1,13.2,11.25,12.0,3432.1231,32.0)

-- INSERT portfolio
insert into portfolios ([name],user_id) values('portfolio1', 1)

-- INSERT stocks_portfolio
insert into stocks_portfolio (portfolio_id,symbol,[name],qtd,price,logo,[date]) values (1,'aapl', 'apple', 2, 10.00, 'aaaaaaaa', '2023-11-07')

-- SELECTS 
select * from users

select * from stocks where symbol = 'petr4';

select * from chartData

select * from portfolios

select * from stocks_portfolio


-- UPDATES
-- Update all values in the "users" table
UPDATE users SET username = 'NewUserName';

-- Update all values in the "stocks" table
UPDATE stocks SET price = 50.00;

-- Update all values in the "chartData" table
UPDATE chartData SET [open] = 14.0, [high] = 15.0, [low] = 13.0, [close] = 14.5, volume = 5000.0, [adjustedClose] = 35.0;

-- Update all values in the "portfolios" table
UPDATE portfolios SET [name] = 'NewPortfolioName';

-- Update all values in the "stocks_portfolio" table
UPDATE stocks_portfolio SET qtd = 4, price = 20.00, [date] = '2023-11-08';


-- DELETES
-- Delete a user
DELETE FROM users WHERE user_id = 1;

-- Delete a stock
DELETE FROM stocks WHERE symbol = 'petr4';

-- Delete chart data
DELETE FROM chartData WHERE chart_stock_id = 1 AND [date] = '2023-11-07';

-- Delete a portfolio
DELETE FROM portfolios WHERE portfolio_id = 1;

-- Delete a stock from a portfolio
DELETE FROM stocks_portfolio WHERE portfolio_id = 1 AND symbol = 'aapl';
