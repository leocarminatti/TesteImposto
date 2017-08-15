USE [Teste]
GO
IF OBJECT_ID('dbo.P_CFOP_POR_ESTADOS') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_CFOP_POR_ESTADOS
    IF OBJECT_ID('dbo.P_CFOP_POR_ESTADOS') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_CFOP_POR_ESTADOS >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_CFOP_POR_ESTADOS APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE dbo.P_CFOP_POR_ESTADOS 
(
	@pEstadoDestino varchar(50),
	@pEstadoOrigem varchar(50)
)
AS
BEGIN
	SELECT Cfop
		FROM CfopPorEstado
	WHERE EstadoDestino = @pEstadoDestino
	  AND EstadoOrigem = @pEstadoOrigem
END
GO
GRANT EXECUTE ON dbo.P_CFOP_POR_ESTADOS TO [public]
go
IF OBJECT_ID('dbo.P_CFOP_POR_ESTADOS') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_CFOP_POR_ESTADOS P_CFOP_POR_ESTADOS >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_CFOP_POR_ESTADOS >>>'
go

