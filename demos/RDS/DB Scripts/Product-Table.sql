USE [launch-demo-db]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 8/21/2020 2:47:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](125) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Product] (Name, Description) VALUES ('Widget', 'Standard widget product')
INSERT INTO [dbo].[Product] (Name, Description) VALUES ('Thing-a-ma-Bob', 'Standard thing-a-ma-bob product')
INSERT INTO [dbo].[Product] (Name, Description) VALUES ('Big Ticket Item', 'Standard "big ticket item" product')
