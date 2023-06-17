USE LojaGamesDB
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UPD_ATUALIZAR_IMAGEM]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[SP_UPD_ATUALIZAR_IMAGEM]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================================================================================
-- Author:		Fernando Augusto
-- Create date: 14/06/2023
-- Description:	Atualizar uma Imagem pelo ID
-- =======================================================================================================================================

-- =======================================================================================================================================
-- Historico
-- =======================================================================================================================================
-- Date			Author			  Description									
-- --------		-------			  ------------------------------------			
-- =======================================================================================================================================

CREATE PROCEDURE [dbo].[SP_UPD_ATUALIZAR_IMAGEM]
	@Url_Blob_Storage varchar(300),
	@id int
AS
BEGIN
	UPDATE imagemProduto SET url_blob_storage = @Url_Blob_Storage WHERE id = @id
END