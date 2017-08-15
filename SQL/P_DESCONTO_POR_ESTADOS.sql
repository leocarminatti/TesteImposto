USE [Teste]
GO
IF OBJECT_ID('dbo.P_DESCONTO_POR_ESTADOS') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_DESCONTO_POR_ESTADOS
    IF OBJECT_ID('dbo.P_DESCONTO_POR_ESTADOS') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_DESCONTO_POR_ESTADOS >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_DESCONTO_POR_ESTADOS APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE dbo.P_DESCONTO_POR_ESTADOS 
(
	@pEstadoOrigem varchar(50)
)
AS
BEGIN
	SELECT Valor
		FROM Descontos
	WHERE EstadoOrigem = @pEstadoOrigem
END
GO
GRANT EXECUTE ON dbo.P_DESCONTO_POR_ESTADOS TO [public]
go
IF OBJECT_ID('dbo.P_DESCONTO_POR_ESTADOS') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_DESCONTO_POR_ESTADOS CRIADA COM SUCESSO >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_DESCONTO_POR_ESTADOS >>>'
go


