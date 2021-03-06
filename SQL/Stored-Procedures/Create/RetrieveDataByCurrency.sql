USE [MyCoreDB]
GO
/****** Object:  StoredProcedure [dbo].[RetrieveDataByCurrency]    Script Date: 5/22/2022 1:19:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:	Thazin Aung
-- Created date:2022-05-22
-- Description:	Retrieve Data By Currency
--=================================================
CREATE PROCEDURE [dbo].[RetrieveDataByCurrency]
	-- Add the parameters for the stored procedure here
	@p_Currency varchar(3)
	
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
	WHERE CurrencyCode = @p_Currency
	
  

END

