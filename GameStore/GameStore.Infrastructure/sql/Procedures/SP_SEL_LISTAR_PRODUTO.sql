USE LojaGamesDB
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SEL_LISTAR_PRODUTO]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[SP_SEL_LISTAR_PRODUTO]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================================================================================
-- Author:		Fernando Augusto
-- Create date: 14/06/2023
-- Description:	Retorna todos produtos com descrição de categoria e a url do blob storage
-- =======================================================================================================================================

-- =======================================================================================================================================
-- Historico
-- =======================================================================================================================================
-- Date			Author			  Description									
-- --------		-------			  ------------------------------------			
-- =======================================================================================================================================

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