CREATE PROCEDURE [dbo].[SP_SEL_VERIFICAR_PROCEDURE_EXISTENTE_POR_NOME]
	@nomeProcedure sysname
AS
BEGIN
	SELECT 
		COUNT(*) 
	FROM 
		sys.procedures 
	WHERE 
		name = @nomeProcedure
END