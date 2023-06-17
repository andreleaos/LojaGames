USE LojaGamesDB
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UPD_ATUALIZAR_PRODUTO]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[SP_UPD_ATUALIZAR_PRODUTO]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================================================================================
-- Author:		Fernando Augusto
-- Create date: 14/06/2023
-- Description:	Atualizar um produto
-- =======================================================================================================================================

-- =======================================================================================================================================
-- Historico
-- =======================================================================================================================================
-- Date			Author			  Description									
-- --------		-------			  ------------------------------------			
-- =======================================================================================================================================

CREATE PROCEDURE [dbo].[SP_UPD_ATUALIZAR_PRODUTO]
	@descricao	   varchar(150) = null,
	@precoUnitario decimal(12,2) = null,
	@id   int
AS
BEGIN
	UPDATE 
		produto 
	SET 
		descricao = @descricao
		, precoUnitario = @precoUnitario 
	WHERE id = @id
END