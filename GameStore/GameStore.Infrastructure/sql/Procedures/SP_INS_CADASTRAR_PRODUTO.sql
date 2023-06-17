USE LojaGamesDB
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_INS_CADASTRAR_PRODUTO]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[SP_INS_CADASTRAR_PRODUTO]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================================================================================
-- Author:		Fernando Augusto
-- Create date: 14/06/2023
-- Description:	Cadastra um novo produto
-- =======================================================================================================================================

-- =======================================================================================================================================
-- Historico
-- =======================================================================================================================================
-- Date			Author			  Description									
-- --------		-------			  ------------------------------------			
-- =======================================================================================================================================

CREATE PROCEDURE [dbo].[SP_INS_CADASTRAR_PRODUTO]
	@descricao	   varchar(150) = null,
	@precoUnitario decimal(12,2) = null,
	@CategoriaId   int = null,
	@ImagemId	   int = null
AS
BEGIN
	INSERT INTO produto 
		(descricao
		, precoUnitario
		, categoriaId
		, imagemId) 
	values 
		(@descricao
		, @precoUnitario
		, @CategoriaId
		, @ImagemId)
END