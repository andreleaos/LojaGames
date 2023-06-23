CREATE PROCEDURE [dbo].[SP_UPD_ATUALIZAR_IMAGEM]
	@Url_Blob_Storage varchar(300),
	@id int
AS
BEGIN
	UPDATE imagemProduto SET url_blob_storage = @Url_Blob_Storage WHERE id = @id
END