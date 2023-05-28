USE [lojaGamesDB]
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 27/05/2023 21:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](50) NULL,
 CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[imagemProduto]    Script Date: 27/05/2023 21:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[imagemProduto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[url_path] [varchar](250) NULL,
 CONSTRAINT [PK_imagemProduto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[produto]    Script Date: 27/05/2023 21:00:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[produto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](150) NULL,
	[precoUnitario] [decimal](12, 3) NULL,
	[categoriaId] [int] NULL,
	[imagemId] [int] NULL,
 CONSTRAINT [PK_produto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[produto]  WITH CHECK ADD  CONSTRAINT [FK_produto_categoria] FOREIGN KEY([categoriaId])
REFERENCES [dbo].[categoria] ([id])
GO
ALTER TABLE [dbo].[produto] CHECK CONSTRAINT [FK_produto_categoria]
GO
ALTER TABLE [dbo].[produto]  WITH CHECK ADD  CONSTRAINT [FK_produto_imagemProduto] FOREIGN KEY([imagemId])
REFERENCES [dbo].[imagemProduto] ([id])
GO
ALTER TABLE [dbo].[produto] CHECK CONSTRAINT [FK_produto_imagemProduto]
GO
