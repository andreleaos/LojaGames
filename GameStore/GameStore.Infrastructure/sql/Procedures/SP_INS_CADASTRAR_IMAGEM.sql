CREATE PROCEDURE [dbo].[SP_INS_CADASTRAR_IMAGEM]
	@Url_Blob_Storage varchar(300)
AS
BEGIN
	INSERT INTO imagemProduto (url_blob_storage) VALUES (@Url_Blob_Storage)
	SELECT SCOPE_IDENTITY();
END