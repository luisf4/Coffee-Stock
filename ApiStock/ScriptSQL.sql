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
    requestedAt Date,
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
    stocks_portfolio INT NOT NULL,
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

