USE [MyCoreDB]
GO
/****** Object:  StoredProcedure [dbo].[RetrieveDataByStatus]    Script Date: 5/22/2022 1:20:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:	Thazin Aung
-- Created date:2022-05-22
-- Description:	Retrieve Data By Status
--=================================================
CREATE PROCEDURE [dbo].[RetrieveDataByStatus]
	-- Add the parameters for the stored procedure here
	@p_Status varchar(10)
	
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
	WHERE ([Status]='Approved' and [Status]=@p_Status)
	or ( (@p_Status = 'Failed' or @p_Status ='Rejected') and [Status] in ('Failed' , 'Rejected'))
	or ( (@p_Status = 'Finished' or @p_Status ='Done') and [Status] in ('Finished' , 'Done'))

  

END

