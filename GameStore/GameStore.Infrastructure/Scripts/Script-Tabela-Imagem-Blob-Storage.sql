USE [lojaGamesDB]
GO

/****** Object:  Table [dbo].[imagemProduto]    Script Date: 30/05/2023 23:11:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[imagemProduto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[url_path] [varchar](250) NULL,
	[url_blob_storage] [varchar](300) NULL,
 CONSTRAINT [PK_imagemProduto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


