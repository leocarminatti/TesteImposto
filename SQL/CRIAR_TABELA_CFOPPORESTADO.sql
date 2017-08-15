USE [Teste]
GO

CREATE TABLE [dbo].[CfopPorEstado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EstadoDestino] [varchar](50) NULL,
	[EstadoOrigem] [varchar](50) NULL,
	[Cfop] [varchar](5) NULL,
 CONSTRAINT [PK_CfopPorEstado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO