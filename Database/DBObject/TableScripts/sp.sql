ALTER PROCEDURE uspTransactionDetails (@OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

SELECT * FROM
(
	 SELECT A.Name [Account], SUM(D.Amount) [Amount], D.DepositDate [Date], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE D.Active = 1 
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)	
	 GROUP BY A.Name,  D.DepositDate
	 

	 UNION	 
	 
	 SELECT A.Name [Account], SUM(E.Amount ) [Amount], E.ExpenseDate [Date], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE E.Active = 1 
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
		GROUP BY A.Name,  E.ExpenseDate

	 UNION

	 SELECT '' [Account], SUM(Amount) [Amount], OfferingDate [Date], 'Offering' FROM OrgOffering	 
	  WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	 GROUP BY OfferingDate 
 ) V
 ORDER BY [DATE]

 END 


ALTER PROCEDURE uspTransactionSummaryByMonthly ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

SELECT [Account], DATENAME(m, str([Mon]) + '/1/2011') [Month], [Amount], [Type] FROM
(
	SELECT A.Name [Account], DATEPART(month, D.DepositDate) [Mon], SUM(D.Amount) [Amount], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE D.Active = 1 
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)	
		GROUP BY DATEPART(month, D.DepositDate), A.Name 

	 UNION

	 SELECT A.Name [Account], DATEPART(month, E.ExpenseDate) [Mon], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE E.Active = 1 
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
		GROUP BY DATEPART(month, E.ExpenseDate), A.Name 

	 UNION

	 SELECT '' [Account], DATEPART(month, OfferingDate) [Mon], SUM(Amount) [Amount], 'Offering' [Type]  FROM OrgOffering WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	  GROUP BY DATEPART(month, OfferingDate)
 ) v
 ORDER BY [Mon]

 END

 uspTransactionSummaryByQuarter 1

 CREATE PROCEDURE uspTransactionSummaryByQuarter ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

--SELECT FYQ.Name, FYQ.StartMonth, FYQ.EndMonth FROM Organization O
--INNER JOIN FiscalYearStartAndEndMonth FYSEM ON O.FiscalYearStartAndEndMonthId = FYSEM.Id
--INNER JOIN FiscalYearQuarter FYQ ON FYQ.FiscalYearStartAndEndMonthId = FYSEM.Id
--WHERE O.Id = 1 AND FYQ.Active = 1 AND FYSEM.Active = 1

SELECT [Account], [Quarter], [Amount], [Type] FROM
(
	SELECT A.Name [Account], CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1)) [Quarter], SUM(D.Amount) [Amount], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE D.Active = 1 
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)	
		GROUP BY CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1)), A.Name 

	 UNION

	 SELECT A.Name [Account], CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1)) [Quarter], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE E.Active = 1 
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
		GROUP BY CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1)), A.Name 

	 UNION

	 SELECT '' [Account], CAST(year(OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,OfferingDate) AS char(1)) [Quarter], SUM(Amount) [Amount], 'Offering' [Type]  FROM OrgOffering WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	  GROUP BY CAST(year(OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,OfferingDate) AS char(1))
 ) v
 ORDER BY [Quarter]

 END



uspBalanceSheet 1
uspBalanceSheetByQuarter 1

ALTER PROCEDURE uspBalanceSheet (@OrgFiscalYearId INT )
as
BEGIN

SELECT 'Offering' AS Type, SUM(Amount) [Amount] FROM Deposit D 
INNER JOIN [Account] A ON D.AccountId = A.Id
INNER JOIN [Bank] B ON A.BankId = B.Id
INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE D.Active = 1 
	AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)	

UNION

SELECT 'Expense' AS Type, SUM(Amount)
 AS [Year] FROM Expense E
	INNER JOIN [Account] A ON E.AccountId = A.Id
INNER JOIN [Bank] B ON A.BankId = B.Id
INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
WHERE E.Active = 1 
AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)	

END

ALTER PROCEDURE uspBalanceSheetByQuarter (@OrgFiscalYearId INT )
as
BEGIN


DECLARE @V1 TABLE ( [Quarter] VARCHAR(10) )

INSERT INTO @V1
SELECT CAST(year(FiscalYearStart) AS char(4)) + '-Q1'  FROM OrgFiscalYear WHERE ID = @OrgFiscalYearId
INSERT INTO @V1
SELECT CAST(year(FiscalYearStart) AS char(4)) + '-Q2'  FROM OrgFiscalYear WHERE ID = @OrgFiscalYearId
INSERT INTO @V1
SELECT CAST(year(FiscalYearStart) AS char(4)) + '-Q3'  FROM OrgFiscalYear WHERE ID = @OrgFiscalYearId
INSERT INTO @V1
SELECT CAST(year(FiscalYearStart) AS char(4)) + '-Q4'  FROM OrgFiscalYear WHERE ID = @OrgFiscalYearId

SELECT 'Offering' AS Type, ISNULL(V2.Amount,0) [Amount], Q.[Quarter] FROM
	(
		SELECT SUM(Amount) [Amount], 
		CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1)) AS [Quarter]  FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
			WHERE D.Active = 1 
			AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
			GROUP BY CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1))
	) V2
	RIGHT OUTER JOIN @V1 AS Q ON Q.[Quarter] = V2.[Quarter]

UNION

SELECT 'Expense' AS Type, ISNULL(V3.Amount,0) [Amount], Q.[Quarter] FROM
(
	SELECT 'Expense' AS Type, SUM(Amount) [Amount], 
	CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1)) AS [Quarter] FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE E.Active = 1 
	AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)	
	GROUP BY CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1))
) V3 
RIGHT OUTER JOIN @V1 AS Q ON Q.[Quarter] = V3.[Quarter]

END

ALTER PROCEDURE [dbo].[uspGetCurrentYesarMonthlyVisitorCount] 
as
BEGIN

DECLARE @V1 TABLE ( [Month] VARCHAR(10) )

INSERT INTO @V1
SELECT '1'
INSERT INTO @V1
SELECT '2'
INSERT INTO @V1
SELECT '3'
INSERT INTO @V1
SELECT '4'

INSERT INTO @V1
SELECT '5'
INSERT INTO @V1
SELECT '6'
INSERT INTO @V1
SELECT '7'
INSERT INTO @V1
SELECT '8'

INSERT INTO @V1
SELECT '9'
INSERT INTO @V1
SELECT '10'
INSERT INTO @V1
SELECT '11'
INSERT INTO @V1
SELECT '12'

SELECT V1.[Month], ISNULL( [VisitoCount], 0) FROM
(
	SELECT ( DATEPART(MONTH, F.[FirstVisitedDate] ) ) [Month], COUNT(*) [VisitoCount] FROM [dbo].[Family] F
	WHERE [FirstVisitedDate] >= '01/01/'+ CAST(YEAR(getdate()) AS VARCHAR(5)) AND [FirstVisitedDate] <= '12/31/'+ CAST(YEAR(getdate()) AS VARCHAR(5))
	AND  F.[MembershipStartDate] IS NULL
	GROUP BY DATEPART(MONTH, F.[FirstVisitedDate])	
) V2

RIGHT OUTER JOIN @V1 AS V1 ON V1.[Month] = V2.[Month]
ORDER BY CAST(V1.[Month] AS INT) 

END

ALTER PROCEDURE [dbo].[uspGetCurrentYesarMonthlyMemberCount]
as
BEGIN
DECLARE @V1 TABLE ( [Month] VARCHAR(10) )

INSERT INTO @V1
SELECT '1'
INSERT INTO @V1
SELECT '2'
INSERT INTO @V1
SELECT '3'
INSERT INTO @V1
SELECT '4'

INSERT INTO @V1
SELECT '5'
INSERT INTO @V1
SELECT '6'
INSERT INTO @V1
SELECT '7'
INSERT INTO @V1
SELECT '8'

INSERT INTO @V1
SELECT '9'
INSERT INTO @V1
SELECT '10'
INSERT INTO @V1
SELECT '11'
INSERT INTO @V1
SELECT '12'

SELECT V1.[Month], ISNULL( [MemberCount], 0) FROM
(
	SELECT ( DATEPART(MONTH, F.[MembershipStartDate]) ) [Month], COUNT(*) [MemberCount] FROM [dbo].[Family] F
	WHERE [MembershipStartDate] >= '01/01/'+ CAST(YEAR(getdate()) AS VARCHAR(5)) AND [MembershipStartDate] <= '12/31/'+ CAST(YEAR(getdate()) AS VARCHAR(5))
	GROUP BY DATEPART(MONTH, F.[MembershipStartDate])	
) V2

RIGHT OUTER JOIN @V1 AS V1 ON V1.[Month] = V2.[Month]
ORDER BY CAST(V1.[Month] AS INT) 


END


[uspGetCurrentYesarMonthlyMemberCount]


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[uspBalanceSheetByQuarter] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

DECLARE @V1 TABLE ( [Quarter] VARCHAR(10) )

INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q1'
INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q2'
INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q3'
INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q4'


SELECT [Quarter], [Amount], [Type] FROM
(
	SELECT V1.[Quarter], ISNULL(V2.[Amount],0) [Amount], 'Deposit' [Type] FROM
	(
		SELECT CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1)) [Quarter], SUM(D.Amount) [Amount], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE D.Active = 1 
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)	
		GROUP BY CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1))
	) V2 RIGHT OUTER JOIN @V1 AS V1 ON V1.[Quarter] = V2.[Quarter]

	

	 UNION

	 SELECT V1.[Quarter], ISNULL(V2.[Amount],0) [Amount], 'Expense' [Type] FROM
	(
	 SELECT CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1)) [Quarter], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id
		INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
		WHERE E.Active = 1 
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
		GROUP BY CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1))
		) V2 RIGHT OUTER JOIN @V1 AS V1 ON V1.[Quarter] = V2.[Quarter]
	
 ) V3
 ORDER BY [Quarter], [Type]

 END


 SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[uspBalanceSheet] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

DECLARE @totalOffering DECIMAL(18,2);
DECLARE @totalExpene DECIMAL(18,2);
DECLARE @OpenningBalance DECIMAL(18,2);

SELECT @totalOffering = SUM(D.Amount)  FROM Deposit D 
	INNER JOIN [Account] A ON D.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE D.Active = 1 
	AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)		

	 
	
	SELECT @totalExpene = SUM(E.Amount) FROM Expense E
	INNER JOIN [Account] A ON E.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE E.Active = 1 
	AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
	
	SELECT @OpenningBalance = SUM(Amount) FROM [OpeningBalance] OP  	
	WHERE [OrgFiscalYearId] = @OrgFiscalYearId AND Active = 1 

SELECT [Amount], [Type] FROM
(

	

	SELECT 1 [Id], SUM(Amount) [Amount], 'Openning Balance' [Type] FROM [OpeningBalance] OP  	
	WHERE [OrgFiscalYearId] = @OrgFiscalYearId AND Active = 1 
	
	 UNION
	
	SELECT 2 [Id], SUM(D.Amount) [Amount], 'Amount Received' [Type] FROM Deposit D 
	INNER JOIN [Account] A ON D.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE D.Active = 1 
	AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)		

	 UNION
	
	SELECT 3 [Id], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
	INNER JOIN [Account] A ON E.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE E.Active = 1 
	AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
		
	 UNION
	
	SELECT 4 [Id], ( @totalOffering - @totalExpene ) + @OpenningBalance, 'Available Balance' [Type]  FROM Expense E
	INNER JOIN [Account] A ON E.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE E.Active = 1 
	AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
	
 ) V3
 ORDER BY [Id]

 END


 ALTER PROCEDURE [dbo].[uspBalanceSheetByAccount] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

SELECT [Account Name], [Amount], [Type] FROM
(
	
	SELECT 1 [Id], NAME [Account Name], Amount, 'Openning Balance' [Type] FROM Account A
	LEFT OUTER JOIN [OpeningBalance] OP ON A.Id= OP.AccountId
	WHERE [OrgFiscalYearId] = @OrgFiscalYearId AND A.Active = 1 AND OP.Active = 1
	
	 UNION
	
	SELECT 2 [Id],  A.Name [Account Name], SUM(ISNULL(D.Amount,0)) [Amount], 'Amount Received' [Type] FROM Deposit D 
	LEFT OUTER JOIN [Account] A ON D.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE D.Active = 1 
	AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)		
	GROUP BY A.Name

	 UNION
	
	SELECT 3 [Id], A.Name [Account Name], SUM(ISNULL(E.Amount,0)) [Amount], 'Expense' [Type]  FROM Expense E
	LEFT OUTER JOIN [Account] A ON E.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id
	INNER JOIN OrgFiscalYear OFY ON OFY.OrganizationId = B.OrganizationId
	WHERE E.Active = 1 
	AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),OFY.FiscalYearStart, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),OFY.FiscalYearEnd, 120)
	GROUP BY A.Name
		
		
	
 ) V3
 ORDER BY [Id]

 END

ALTER PROCEDURE [dbo].[uspDailyRemainder]
AS
BEGIN
	
	INSERT INTO DBO.[BirthdayRemainder] (OrganizationId, MemberId, BirthDate,CreateUser, LastUpdateUser)
	
	SELECT O.Id, FM.Id, CONVERT(date, GETDATE()), 'SYSTEM', 'SYSTEM' FROM DBO.FamilyMember FM 
	INNER JOIN DBO.Family F ON FM.FamilyId = F.Id
	INNER JOIN DBO.Organization B ON F.BranchId = B.Id
	INNER JOIN DBO.Organization O ON B.ParentId = O.Id
	WHERE datepart(day, GETDATE()) = datepart(day,DOB)
		AND datepart(month, GETDATE()) = datepart(month,DOB)   
		AND NOT EXISTS (SELECT * FROM dbo.BirthdayRemainder BR WHERE BR.MemberId = FM.Id AND DATEDIFF(DAY, BR.BirthDate, GETDATE() ) = 0)

INSERT INTO DBO.[MarriageAnniversaryRemainder] (OrganizationId, FamilyId, MarriageDate,CreateUser, LastUpdateUser)

SELECT O.Id, F.Id, CONVERT(date, GETDATE()), 'SYSTEM', 'SYSTEM' FROM DBO.Family F	
	INNER JOIN DBO.Organization B ON F.BranchId = B.Id
	INNER JOIN DBO.Organization O ON B.ParentId = O.Id
	WHERE datepart(day, GETDATE()) = datepart(day,MariageDate)
		AND datepart(month, GETDATE()) = datepart(month,MariageDate)   
		AND NOT EXISTS (SELECT * FROM dbo.MarriageAnniversaryRemainder MAR WHERE MAR.FamilyId = F.Id AND DATEDIFF(DAY, MAR.	, GETDATE() ) = 0)


END
