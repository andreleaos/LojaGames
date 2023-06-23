CREATE PROCEDURE [dbo].[SP_SEL_LISTAR_PRODUTO]

AS
BEGIN
	SELECT 
		p.id
		, p.descricao
		, p.precoUnitario
		, p.categoriaId
		, c.descricao 'categoria'
		, p.imagemId 'ImagemId'
		, IP.url_blob_storage 'UrlBlobStorage' 
	FROM 
		produto p WITH(NOLOCK)
	INNER JOIN 
		imagemProduto IP WITH(NOLOCK) ON p.imagemId = IP.id 
	INNER JOIN 
		categoria c WITH(NOLOCK) ON p.categoriaId = c.id
	ORDER BY
		p.descricao
END