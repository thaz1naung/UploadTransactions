USE [MyCoreDB]
GO

/****** Object:  Table [dbo].[DataTransaction]    Script Date: 5/22/2022 1:17:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DataTransaction](
	[TransGUID] [uniqueidentifier] NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CurrencyCode] [nvarchar](3) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_DataTransaction_1] PRIMARY KEY CLUSTERED 
(
	[TransGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DataTransaction] ADD  CONSTRAINT [DF_DataTransaction_Amount]  DEFAULT ((0)) FOR [Amount]
GO


