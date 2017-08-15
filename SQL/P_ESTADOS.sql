USE [Teste]
GO
IF OBJECT_ID('dbo.P_ESTADOS') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_ESTADOS
    IF OBJECT_ID('dbo.P_ESTADOS') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_ESTADOS >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_ESTADOS APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE dbo.P_ESTADOS 
AS
BEGIN
	SELECT EstadoOrigem,
		   EstadoDestino
		FROM CfopPorEstado
END
GO
GRANT EXECUTE ON dbo.P_ESTADOS TO [public]
go
IF OBJECT_ID('dbo.P_ESTADOS') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_ESTADOS >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_ESTADOS >>>'
go

