CREATE PROCEDURE [dbo].[SP_SEL_PESQUISAR_IMAGEM_PELA_URL]
	@Url_Blob_Storage varchar(300)
AS
BEGIN
	SELECT 
		id 'Id'
		, url_blob_storage 'Url' 
	FROM 
		imagemProduto WITH(NOLOCK)
	WHERE 
		url_blob_storage = @Url_Blob_Storage
END