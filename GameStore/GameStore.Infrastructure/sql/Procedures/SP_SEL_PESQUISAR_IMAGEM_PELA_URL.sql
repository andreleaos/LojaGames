USE LojaGamesDB
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SEL_PESQUISAR_IMAGEM_PELA_URL]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[SP_SEL_PESQUISAR_IMAGEM_PELA_URL]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================================================================================
-- Author:		Fernando Augusto
-- Create date: 14/06/2023
-- Description:	Pesquisar uma Imagem pela URL
-- =======================================================================================================================================

-- =======================================================================================================================================
-- Historico
-- =======================================================================================================================================
-- Date			Author			  Description									
-- --------		-------			  ------------------------------------			
-- =======================================================================================================================================

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