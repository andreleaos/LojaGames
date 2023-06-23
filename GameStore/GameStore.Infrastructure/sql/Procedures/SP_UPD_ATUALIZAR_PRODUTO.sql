CREATE PROCEDURE [dbo].[SP_UPD_ATUALIZAR_PRODUTO]
	@descricao	   varchar(150) = null,
	@precoUnitario decimal(12,2) = null,
	@categoriaId  int = null,
	@id   int
AS
BEGIN
	UPDATE 
		produto 
	SET 
		descricao = @descricao
		, precoUnitario = @precoUnitario 
		, categoriaId = @categoriaId
	WHERE id = @id
END