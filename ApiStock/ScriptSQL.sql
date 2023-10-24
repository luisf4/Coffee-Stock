-- Create the database
CREATE DATABASE coffeeStock;
GO

-- Use the "coffeeStock" database
USE coffeeStock;
GO

-- Users table
CREATE TABLE users (
    user_id INT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL,
    jwt VARCHAR(100) NOT NULL
);
GO

-- Portfolios table
CREATE TABLE portfolios (
    portfolio_id INT PRIMARY KEY,
    user_id INT NOT NULL,
    portfolio_name VARCHAR(100) NOT NULL
);
GO

-- Stocks table
CREATE TABLE stocks (
    stock_id INT PRIMARY KEY,
    stock_symbol VARCHAR(10) NOT NULL,
    stock_name VARCHAR(100) NOT NULL,
    quantity INT NOT NULL,
    purchase_price DECIMAL(10, 2) NOT NULL
);

-- PortfolioStocks table to establish relationships
CREATE TABLE PortfolioStocks (
    portfolio_id INT,
    stock_id INT,
    PRIMARY KEY (portfolio_id, stock_id),
    FOREIGN KEY (portfolio_id) REFERENCES portfolios(portfolio_id),
    FOREIGN KEY (stock_id) REFERENCES stocks(stock_id)
);
GO
