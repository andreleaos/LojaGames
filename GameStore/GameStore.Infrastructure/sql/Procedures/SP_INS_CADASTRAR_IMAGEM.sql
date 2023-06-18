USE LojaGamesDB
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_INS_CADASTRAR_IMAGEM]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[SP_INS_CADASTRAR_IMAGEM]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================================================================================
-- Author:		Fernando Augusto
-- Create date: 14/06/2023
-- Description:	Incluir uma Imagem
-- =======================================================================================================================================

-- =======================================================================================================================================
-- Historico
-- =======================================================================================================================================
-- Date			Author			  Description									
-- --------		-------			  ------------------------------------			
-- =======================================================================================================================================

CREATE PROCEDURE [dbo].[SP_INS_CADASTRAR_IMAGEM]
	@Url_Blob_Storage varchar(300)
AS
BEGIN
	INSERT INTO imagemProduto (url_blob_storage) VALUES (@Url_Blob_Storage)
	SELECT SCOPE_IDENTITY();
END