USE LojaGamesDB

BEGIN
	BEGIN TRY
		BEGIN TRAN
			IF NOT EXISTS(SELECT * FROM [dbo].[categoria] WHERE [descricao] = 'Console')
			BEGIN
				insert into categoria (descricao) values ('Console')
			END
			IF NOT EXISTS(SELECT * FROM [dbo].[categoria] WHERE [descricao] = 'Game')
			BEGIN
				insert into categoria (descricao) values ('Game')
			END
			IF NOT EXISTS(SELECT * FROM [dbo].[categoria] WHERE [descricao] = 'Acessorio')
			BEGIN
				insert into categoria (descricao) values ('Acessorio')
			END
			IF NOT EXISTS(SELECT * FROM [dbo].[categoria] WHERE [descricao] = 'Periferico')
			BEGIN
				insert into categoria (descricao) values ('Periferico')
			END

			SELECT SCOPE_IDENTITY();			
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

