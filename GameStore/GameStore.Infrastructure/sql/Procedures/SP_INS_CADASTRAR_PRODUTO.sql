CREATE PROCEDURE [dbo].[SP_INS_CADASTRAR_PRODUTO]
	@descricao	   varchar(150) = null,
	@precoUnitario decimal(12,2) = null,
	@CategoriaId   int = null,
	@ImagemId	   int = null
AS
BEGIN
	INSERT INTO produto 
		(descricao
		, precoUnitario
		, categoriaId
		, imagemId) 
	values 
		(@descricao
		, @precoUnitario
		, @CategoriaId
		, @ImagemId)
END