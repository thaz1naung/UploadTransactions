USE [MyCoreDB]
GO
/****** Object:  StoredProcedure [dbo].[RetrieveDataByDateRange]    Script Date: 5/22/2022 1:20:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:	Thazin Aung
-- Created date:2022-05-22
-- Description:	Retrieve Data By Date Range
--=================================================
CREATE PROCEDURE [dbo].[RetrieveDataByDateRange]
	-- Add the parameters for the stored procedure here
	@p_FromDate DateTime,
    @p_ToDate DateTime 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	SELECT TransactionId as Id,CONCAT(Amount, CurrencyCode) as Payment,
	CASE
    WHEN [Status] = 'Approved' THEN 'A'
    WHEN [Status] = 'Failed' or [Status] ='Rejected'  THEN 'R'
    WHEN [Status] = 'Finished' or [Status] ='Done' THEN 'D'  
	END as [Status]
    from DataTransaction
	WHERE (CONVERT(char(10), TransactionDate,126)) between @p_FromDate and @p_ToDate

  

END

