CREATE PROCEDURE [dbo].[SP_SEL_PESQUISAR_PRODUTO]
	@id int
AS
BEGIN
	SELECT 
		p.id
		, p.descricao
		, p.precoUnitario
		, p.categoriaId
		, c.descricao 'categoria'
		, p.imagemId 'ImagemId'
		, IP.url_blob_storage 'Url' 
	FROM 
		produto p WITH(NOLOCK) 
	INNER JOIN 
		imagemProduto IP WITH(NOLOCK) ON p.imagemId = ip.id 
	INNER JOIN 
		categoria c WITH(NOLOCK) ON p.categoriaId = c.id 
	WHERE 
		p.id = @id
END