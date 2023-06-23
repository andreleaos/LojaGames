CREATE PROCEDURE [dbo].[SP_SEL_VERIFICAR_TABELA_CATEGORIA_SEED]
AS
BEGIN
	SELECT 
		COUNT(*) 
	FROM 
		[dbo].[categoria] 
	WHERE 
		[descricao] not in ('Console', 'Game', 'Acessorio', 'Periferico')
END