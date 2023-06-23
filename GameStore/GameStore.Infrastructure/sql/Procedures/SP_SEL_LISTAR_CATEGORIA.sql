CREATE PROCEDURE [dbo].[SP_SEL_LISTAR_CATEGORIA]
AS
BEGIN
	SELECT 
		id,
		descricao
	FROM 
		[dbo].[categoria]
END