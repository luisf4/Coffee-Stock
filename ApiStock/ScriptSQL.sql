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

-- Portfolios table
CREATE TABLE portfolios (
    portfolio_id INT PRIMARY KEY,
    user_id INT NOT NULL,
    portfolio_name VARCHAR(100) NOT NULL
);
GO

-- PortfolioStocks table to establish relationships
CREATE TABLE portfolioStocks (
    portfolio_id INT,
    stock_id INT,
    PRIMARY KEY (portfolio_id, stock_id),
    FOREIGN KEY (portfolio_id) REFERENCES portfolios(portfolio_id),
    FOREIGN KEY (stock_id) REFERENCES stocks(stock_id)
);
GO

--- aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa    

-- Create the HistoricalDataPrice table
CREATE TABLE HistoricalDataPrice (
    Date BIGINT PRIMARY KEY,
    [Open] FLOAT,
    High FLOAT,
    Low FLOAT,
    [Close] FLOAT,
    Volume BIGINT,
    AdjustedClose FLOAT
);

-- Create the Stock table
CREATE TABLE Stocks (
    Id INT IDENTITY(1, 1) PRIMARY KEY,

);
