USE LojaGamesDB

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

SET NOCOUNT ON

BEGIN
	BEGIN TRY
		BEGIN TRAN		
			IF (not exists (select top 1 * from sys.tables where name = 'imagemProduto'))
			BEGIN
				CREATE TABLE [dbo].[imagemProduto](
					[id] [int] IDENTITY(1,1) NOT NULL,
					--[url_path] [varchar](250) NULL,
					[url_blob_storage] [varchar](300) NULL,
				 CONSTRAINT [PK_imagemProduto] PRIMARY KEY CLUSTERED 
				(
					[id] ASC
				)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
				) ON [PRIMARY]
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
