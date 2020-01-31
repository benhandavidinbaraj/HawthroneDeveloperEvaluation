--------------------------------------------------------------------------------------------------
--Using the following two temp tables, please generate T-SQL statements that accomplish each item.
--------------------------------------------------------------------------------------------------
DECLARE
  	@TempParts TABLE (
  	PartId VARCHAR(20)
  	, NewDate DATETIME
  	, Price MONEY
);
INSERT INTO @TempParts VALUES ('700450', '08-19-2014', 14.12);
INSERT INTO @TempParts VALUES ('700452', '07-23-2014', 12.12);
INSERT INTO @TempParts VALUES ('700454', '06-19-2014', -10.14);
INSERT INTO @TempParts VALUES ('723160', '08-29-2014', 123.45);
INSERT INTO @TempParts VALUES ('904926', '04-09-2014', 45.36);
INSERT INTO @TempParts VALUES ('904928', '03-03-2014', 54);
INSERT INTO @TempParts VALUES ('700467', '11-07-2014', 1);
INSERT INTO @TempParts VALUES ('700468', '11-11-2014', 23.01);
INSERT INTO @TempParts VALUES ('700469', '11-24-2014', 83.12);
INSERT INTO @TempParts VALUES ('700470', '11-14-2012', 555.44);
INSERT INTO @TempParts VALUES ('700471', '10-07-2013', 143.88);
INSERT INTO @TempParts VALUES ('400450', '08-19-2014', 22.98);
INSERT INTO @TempParts VALUES ('400452', '07-23-2014', 14.44);
INSERT INTO @TempParts VALUES ('400454', '06-12-2014', 89.89);
INSERT INTO @TempParts VALUES ('423160', '08-05-2014', 145.93);
INSERT INTO @TempParts VALUES ('504926', '05-04-2014', 5444444.41);
INSERT INTO @TempParts VALUES ('504928', '08-03-2014', 0.01);
INSERT INTO @TempParts VALUES ('300467', '04-07-2014', 94);
INSERT INTO @TempParts VALUES ('300468', '01-11-2014', 14.54);
INSERT INTO @TempParts VALUES ('300469', '10-13-2014', 12.02);
INSERT INTO @TempParts VALUES ('300470', '11-07-2012', 98.37);
INSERT INTO @TempParts VALUES ('300471', '10-07-2013', 391.7);

DECLARE
  	@TempSaleParts TABLE (
  	PartId VARCHAR(20)
  	, EndDate DATETIME
  	, Price MONEY
);
INSERT INTO @TempSaleParts VALUES ('700450', '11-19-2016', 13.21);
INSERT INTO @TempSaleParts VALUES ('723160', '2-29-2016', 103.45);
INSERT INTO @TempSaleParts VALUES ('904926', '04-09-2017', 44.36);
INSERT INTO @TempSaleParts VALUES ('904928', '12-03-2016', 44);
INSERT INTO @TempSaleParts VALUES ('700467', '11-07-2016', .99);
INSERT INTO @TempSaleParts VALUES ('700471', '10-07-2017', 140.88);
INSERT INTO @TempSaleParts VALUES ('400450', '01-19-2017', 21.98);
INSERT INTO @TempSaleParts VALUES ('504926', '12-04-2016', 5444444.21);
INSERT INTO @TempSaleParts VALUES ('300467', '04-07-2017', 94.01);
INSERT INTO @TempSaleParts VALUES ('300470', '11-07-2018', 90.37);
INSERT INTO @TempSaleParts VALUES ('300470', '11-07-2018', 90.37);

------------------------------------------------------------------------
--Using the @TempParts table above, please show how to do the following:
------------------------------------------------------------------------

--1. Select only the newest 4 parts.
SELECT TOP 4 PartId, 
	NewDate, 
	Price
FROM @TempParts 
ORDER BY NewDate DESC

--2. Select only the cheapest 10 parts.
SELECT TOP 10 PartId, 
	NewDate, 
	Price
FROM @TempParts 
ORDER BY Price

--3. Select 5 parts randomly.
SELECT TOP 5 PartId, 
	NewDate, 
	Price
FROM @TempParts
ORDER BY RAND(CHECKSUM(*) * RAND())

--4. Select all, plus add a “how expensive” identifier column. 
--		up to $10 return $
--		to $25 return $$
--      to $50 return $$$
--      to $100 return $$$$
--      above return $$$$$

SELECT PartId, 
	NewDate, 
	Price,
	CASE WHEN Price <=10 THEN '$' 
	WHEN Price>10 AND Price<=25 THEN '$$'
	WHEN Price>25 AND Price<=50 THEN '$$$'
	WHEN Price>50 AND Price<=100 THEN '$$$$'
	WHEN Price>100 THEN '$$$$$'
	END AS 'How Expensive' 
FROM @TempParts

---------------------------------------------------------------
--Using both tables above, please show how to do the following:
---------------------------------------------------------------

--1. Select all and return PartId, NewDate and lowest available price plus the “how expensive” identifier.
;WITH LowestRateParts(PartId, NewDate, lowestavailableprice)
AS
(
	SELECT DISTINCT Parts.PartId AS PartId, 
		NewDate, 
		CASE WHEN SaleParts.Price IS NULL THEN Parts.Price 
			WHEN SaleParts.Price<Parts.Price THEN SaleParts.Price
			ELSE Parts.Price END AS 'lowest available price'
	FROM @TempParts Parts
	LEFT JOIN @TempSaleParts SaleParts ON SaleParts.PartId = Parts.PartId
)
SELECT  PartId, 
		NewDate, 
		lowestavailableprice AS 'lowest available price',
		CASE WHEN lowestavailableprice <=10 THEN '$' 
			WHEN lowestavailableprice>10 AND lowestavailableprice<=25 THEN '$$'
			WHEN lowestavailableprice>25 AND lowestavailableprice<=50 THEN '$$$'
			WHEN lowestavailableprice>50 AND lowestavailableprice<=100 THEN '$$$$'
			WHEN lowestavailableprice>100 THEN '$$$$$'
			END AS 'How Expensive' 
FROM LowestRateParts

--2. Select only sale parts and return PartId, NewDate and lowest available price plus the “how expensive” identifier.
;WITH LowestRateParts(PartId, NewDate, lowestavailableprice)
AS
(
	SELECT DISTINCT Parts.PartId AS PartId, 
		NewDate, 
		CASE WHEN SaleParts.Price IS NULL THEN Parts.Price 
			WHEN SaleParts.Price<Parts.Price THEN SaleParts.Price
			ELSE Parts.Price END AS 'lowest available price'
	FROM @TempParts Parts
	INNER JOIN @TempSaleParts SaleParts ON SaleParts.PartId = Parts.PartId
)
SELECT  PartId, 
		NewDate, 
		lowestavailableprice AS 'lowest available price',
		CASE WHEN lowestavailableprice <=10 THEN '$' 
			WHEN lowestavailableprice>10 AND lowestavailableprice<=25 THEN '$$'
			WHEN lowestavailableprice>25 AND lowestavailableprice<=50 THEN '$$$'
			WHEN lowestavailableprice>50 AND lowestavailableprice<=100 THEN '$$$$'
			WHEN lowestavailableprice>100 THEN '$$$$$'
			END AS 'How Expensive' 
FROM LowestRateParts

--* Added distinct to above two queries as there were duplicate records in @TempSaleParts table
