USE master  
GO
CREATE DATABASE CompanyDB
GO
USE CompanyDB
GO
CREATE TABLE Company(
	[Id] [int] PRIMARY KEY IDENTITY (1,1),
	[Name] [nvarchar](255) NOT NULL,
	[Exchange] [nvarchar](255) NOT NULL,
	[Ticker] [nvarchar](50) NOT NULL,
	[Isin] [nvarchar](255) NOT NULL,
	[Website] [nvarchar](255) NULL
)
GO
INSERT INTO Company ([Name], Exchange, Ticker, Isin, Website)
VALUES ('Apple Inc', 'NASDAQ', 'AAPL', 'US0378331005', 'http://www.apple.com'),
	('British Airways Plc', 'Pink Sheets', 'BAIRY', 'US1104193065', NULL),
	('Heineken NV', 'Euronext Amsterdam', 'HEIA', 'NL0000009165', NULL),
	('Panasonic Corp', 'Tokyo Stock Exchange', '6752', 'JP3866800000', 'http://panasonic.co.jp'),
	('Porsche Automobil', 'Deutsche Borse', 'PAH3', 'DE000PAH0038', 'https://www.porsche.com')
