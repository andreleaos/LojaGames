USE LojaGamesDB


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DEL_EXCLUIR_IMAGEM]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[SP_DEL_EXCLUIR_IMAGEM]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================================================================================
-- Author:		Fernando Augusto
-- Create date: 14/06/2023
-- Description:	Excluir uma Imagem
-- =======================================================================================================================================

-- =======================================================================================================================================
-- Historico
-- =======================================================================================================================================
-- Date			Author			  Description									
-- --------		-------			  ------------------------------------			
-- =======================================================================================================================================

CREATE PROCEDURE [dbo].[SP_DEL_EXCLUIR_IMAGEM]
	@id   int
AS
BEGIN
	DELETE FROM imagemProduto WHERE id = @id
END