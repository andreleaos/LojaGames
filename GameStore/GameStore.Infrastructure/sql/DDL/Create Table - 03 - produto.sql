USE LojaGamesDB


SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

SET NOCOUNT ON

BEGIN
	BEGIN TRY
		BEGIN TRAN		
			IF (not exists (select top 1 * from sys.tables where name = 'produto'))
			BEGIN
				CREATE TABLE [dbo].[produto](
					[id] [int] IDENTITY(1,1) NOT NULL,
					[descricao] [varchar](150) NULL,
					[precoUnitario] [decimal](18, 2) NULL,
					[categoriaId] [int] NULL,
					[imagemId] [int] NULL,
				 CONSTRAINT [PK_produto] PRIMARY KEY CLUSTERED 
				(
					[id] ASC
				)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
				) ON [PRIMARY]

				ALTER TABLE [dbo].[produto]  WITH CHECK ADD  CONSTRAINT [FK_produto_categoria] FOREIGN KEY([categoriaId])
				REFERENCES [dbo].[categoria] ([id])

				ALTER TABLE [dbo].[produto] CHECK CONSTRAINT [FK_produto_categoria]

				ALTER TABLE [dbo].[produto]  WITH CHECK ADD  CONSTRAINT [FK_produto_imagemProduto] FOREIGN KEY([imagemId])
				REFERENCES [dbo].[imagemProduto] ([id])

				ALTER TABLE [dbo].[produto] CHECK CONSTRAINT [FK_produto_imagemProduto]
			END

		COMMIT;

	END TRY
	BEGIN CATCH

		ROLLBACK;
	
		DECLARE      
			@ErrorMessage  NVARCHAR(4000),      
			@ErrorSeverity INT,      
			@ErrorState    INT;

		SELECT 
			@ErrorMessage  = ERROR_MESSAGE(), 
			@ErrorSeverity = ERROR_SEVERITY(), 
			@ErrorState    = ERROR_STATE();

		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);

	END CATCH
END 
